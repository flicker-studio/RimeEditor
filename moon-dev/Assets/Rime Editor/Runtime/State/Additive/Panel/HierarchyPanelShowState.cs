using System;
using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using LevelEditor;
using LevelEditor.Command;
using LevelEditor.Item;
using LevelEditor.Manager;
using LevelEditor.View.Element;
using RimeEditor.Runtime;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyPanelShowState : AdditiveState
{
    private readonly List<ItemView> _itemViewList     = new();
    private          List<ItemBase> _selectedItemList = new();

    public HierarchyPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitSyncEvent();
        InitEvent();
        InitButton();
        InitState();
    }

    private BrowseController Controller        => m_information.Controller;
    private HierarchyPanel   HierarchyPanel    => m_information.UIManager.GetHierarchyPanel;
    private Transform        ScrollViewContent => HierarchyPanel.GetHierarchyContent;
    private List<ItemBase>   SelectedItems     => Controller.SelectedItems;
    private List<ItemBase>   ItemAssets        => Controller.ItemAssets;
    private OutlineManager   Outline           => m_information.OutlineManager;
    private Button           AddButton         => HierarchyPanel.GetAddButton;
    private Button           DeleteButton      => HierarchyPanel.GetDeleteButton;
    private ScrollRect       ScrollView        => HierarchyPanel.GetScrollView;
    private bool             ShiftInput        => throw new InvalidOperationException(); // m_information.InputManager.GetShiftButton;
    private bool             CtrlInput         => throw new InvalidOperationException(); // m_information.InputManager.GetCtrlButton;
    private bool             DeleteInputDown   => throw new InvalidOperationException(); // m_information.InputManager.GetDeleteButtonDown;

    /// <inheritdoc />
    public override void Motion(Information information)
    {
        if (DeleteInputDown) CommandInvoker.Execute(new Delete(SelectedItems.ToList()));
    }

    private void InitState()
    {
        if (Controller.CurrentSubLevel is null) return;

        SyncNodeByLevelData(Controller.CurrentSubLevel);
    }

    private void InitButton()
    {
        AddButton.onClick.AddListener(() =>
        {
            if (!CheckStates.Contains(typeof(ItemWarehousePanelShowState))) ChangeMotionState(typeof(ItemWarehousePanelShowState));
        });

        DeleteButton.onClick.AddListener(() => { CommandInvoker.Execute(new Delete(SelectedItems.ToList())); });
    }

    private void InitSyncEvent()
    {
        Controller.SyncLevelData += SyncNodeByLevelData;
    }

    private void InitEvent()
    {
        /*
            ItemAssets.OnAdd       -= CreateNode;
            ItemAssets.OnAdd       -= SyncNodePanelSelect;
            ItemAssets.OnAddRange  -= CreateNode;
            ItemAssets.OnRemove    -= Delete;
            ItemAssets.OnRemove    -= SyncNodePanelSelect;
            ItemAssets.OnRemoveAll -= Delete;
            TargetItems.OnAddRange -= SyncNodePanelSelect;

            ItemAssets.OnAdd       += CreateNode;
            ItemAssets.OnAdd       += SyncNodePanelSelect;
            ItemAssets.OnAddRange  += CreateNode;
            ItemAssets.OnRemove    += Delete;
            ItemAssets.OnRemove    += SyncNodePanelSelect;
            ItemAssets.OnRemoveAll += Delete;
            TargetItems.OnAddRange += SyncNodePanelSelect;
            */
    }

    private void CreateNode(List<ItemBase> targetItems)
    {
        foreach (var targetItem in targetItems) CreateNode(targetItem);
    }

    private void CreateNode(ItemBase targetItemBase)
    {
        var ItemView               = CreateChild(targetItemBase);
        var itemNodeChildTransform = ItemView.Transform;

        foreach (var itemNodeProperty in _itemViewList)
            if (itemNodeProperty is ItemView && itemNodeProperty.Type == targetItemBase.Type)
            {
                ItemView = itemNodeProperty;
                var parentChilds = ItemView.GetAllChild();
                int lastIndex;

                if (parentChilds.Count > 0)
                    lastIndex = parentChilds.Last().Transform.GetSiblingIndex();
                else
                    lastIndex = ItemView.Transform.GetSiblingIndex();

                itemNodeChildTransform.SetSiblingIndex(lastIndex + 1);
                ItemView.AddChild(ItemView);
                ItemView.ShowChild();
                return;
            }

        ItemView = CreateParent(targetItemBase);
        itemNodeChildTransform.SetSiblingIndex(ItemView.Transform.GetSiblingIndex() + 1);
        ItemView.AddChild(ItemView);
        ItemView.ShowChild();
    }

    private void Delete(List<ItemBase> item)
    {
        _selectedItemList.Clear();

        foreach (var itemData in item) Delete(itemData);
    }

    private void Delete(ItemBase itemBase)
    {
        Delete(itemBase.View);
    }

    private void Delete(ItemView itemView)
    {
        itemView.Remove();
    }

    private ItemView CreateParent(ItemBase itemBase)
    {
        var itemNodeParent = new ItemView(itemBase, GetSelectedNode, ScrollView);
        _itemViewList.Add(itemNodeParent);
        itemNodeParent.Transform.SetSiblingIndex(ScrollViewContent.childCount);
        return itemNodeParent;
    }

    private ItemView CreateChild(ItemBase targetItemBase)
    {
        var itemNodeChild = new ItemView(targetItemBase, GetSelectedNode, ScrollView);
        _itemViewList.Add(itemNodeChild);
        return itemNodeChild;
    }

    private void SyncNodeByLevelData(SubLevel subLevel)
    {
        InitEvent();
        ClearNode();
        var itemDatas = subLevel.ItemAssets;

        foreach (var itemData in itemDatas) CreateNode(itemData);
    }

    private void ClearNode()
    {
        foreach (var itemNodeProperty in _itemViewList) itemNodeProperty.Remove();

        _itemViewList.Clear();
    }

    private void SyncNodePanelSelect(List<ItemBase> itemData)
    {
        SyncNodePanelSelect();
    }

    private void SyncNodePanelSelect(ItemBase itemBase)
    {
        SyncNodePanelSelect();
    }

    private void SyncNodePanelSelect()
    {
        _selectedItemList.Clear();
        _selectedItemList.AddRange(SelectedItems);

        foreach (var itemNodeProperty in _itemViewList) itemNodeProperty.IsSelected = false;

        foreach (var targetItem in SelectedItems)
        foreach (var itemNodeProperty in _itemViewList)
            if (itemNodeProperty is ItemView itemNodeChild && itemNodeChild.ItemBase == targetItem)
            {
                itemNodeChild.IsSelected = true;
                break;
            }
    }

    private void GetSelectedNode()
    {
        /*ItemView selectView
        if (ShiftInput)
        {
            SelectMultiItem(selectView);
            return;
        }

        if (CtrlInput)
        {
            SelectOppositeItem(selectView);
            return;
        }

        SelectSingleItem(selectView);*/
    }

    private void SelectSingleItem(ItemView selectView)
    {
        if (selectView.IsSelected && _selectedItemList.Count == 1) return;

        _selectedItemList.Clear();

        foreach (var itemNodeProperty in _itemViewList) itemNodeProperty.IsSelected = false;

        SelectAddItem(selectView);
    }

    private void SelectMultiItem(ItemView selectedView)
    {
        if (_selectedItemList.Count == 0)
        {
            SelectAddItem(selectedView);
            return;
        }

        var startSelect = selectedView.Transform.GetSiblingIndex();

        var endSelect = startSelect;

        var lastSelectData = _selectedItemList.Last();

        foreach (var itemNodeProperty in _itemViewList)
        {
            if (itemNodeProperty.ItemBase != lastSelectData) continue;
            endSelect = itemNodeProperty.Transform.GetSiblingIndex();
            break;
        }

        var lower = Mathf.Min(startSelect, endSelect);
        var upper = Mathf.Max(startSelect, endSelect);

        foreach (var view in _itemViewList)
            if
            (
                view.Transform.GetSiblingIndex() >= lower
             && view.Transform.GetSiblingIndex() <= upper
            )
                SelectAddItem(view);
    }

    private void SelectAddItem(ItemView selectView)
    {
        if (selectView.IsSelected) return;

        if (selectView is ItemView child)
        {
            selectView.IsSelected = true;
            _selectedItemList.Add(child.ItemBase);
            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(SelectedItems, _selectedItemList, Outline));
            return;
        }

        if (selectView is ItemView selectNodeParent)
        {
            var tarGetAllChild = selectNodeParent.GetAllChild();

            foreach (var itemNodeChild in tarGetAllChild)
            {
                itemNodeChild.IsSelected = true;
                _selectedItemList.Add(itemNodeChild.ItemBase);
            }

            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(SelectedItems, _selectedItemList, Outline));
            selectView.IsSelected = true;
        }
    }

    private void SelectOppositeItem(ItemView selectView)
    {
        if (selectView is ItemView child)
        {
            selectView.IsSelected = !selectView.IsSelected;

            if (selectView.IsSelected)
                _selectedItemList.Add(child.ItemBase);
            else
                _selectedItemList.Remove(child.ItemBase);

            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(SelectedItems, _selectedItemList, Outline));
            return;
        }

        if (selectView is ItemView selectNodeParent)
        {
            var tarGetAllChild = selectNodeParent.GetAllChild();
            var isSelectParent = !selectView.IsSelected;

            foreach (var itemNodeChild in tarGetAllChild)
            {
                itemNodeChild.IsSelected = isSelectParent;

                if (itemNodeChild.IsSelected)
                    _selectedItemList.Add(itemNodeChild.ItemBase);
                else
                    _selectedItemList.Remove(itemNodeChild.ItemBase);
            }

            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(SelectedItems, _selectedItemList, Outline));
            selectView.IsSelected = isSelectParent;
        }
    }
}