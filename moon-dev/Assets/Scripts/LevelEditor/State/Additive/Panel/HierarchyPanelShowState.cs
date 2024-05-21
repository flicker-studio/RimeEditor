using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using LevelEditor;
using LevelEditor.Command;
using LevelEditor.Data;
using Moon.Kernel.Utils;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyPanelShowState : AdditiveState
{
    private          LevelDataManager       DataManager       => m_information.DataManager;
    private          HierarchyPanel         HierarchyPanel    => m_information.UIManager.GetHierarchyPanel;
    private          Transform              ScrollViewContent => HierarchyPanel.GetHierarchyContent;
    private          List<LevelEditor.Item> TargetItems       => DataManager.TargetItems;
    private          List<LevelEditor.Item> ItemAssets        => DataManager.ItemAssets;
    private          OutlineManager         Outline           => m_information.OutlineManager;
    private          Button                 AddButton         => HierarchyPanel.GetAddButton;
    private          Button                 DeleteButton      => HierarchyPanel.GetDeleteButton;
    private          ScrollRect             ScrollView        => HierarchyPanel.GetScrollView;
    private          bool                   ShiftInput        => m_information.InputManager.GetShiftButton;
    private          bool                   CtrlInput         => m_information.InputManager.GetCtrlButton;
    private          bool                   DeleteInputDown   => m_information.InputManager.GetDeleteButtonDown;
    private readonly List<ItemView>         _itemViewList     = new();
    private          List<LevelEditor.Item> _selectedItemList = new();

    public HierarchyPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitSyncEvent();
        InitEvent();
        InitButton();
        InitState();
    }

    /// <inheritdoc />
    public override void Motion(BaseInformation information)
    {
        if (DeleteInputDown) CommandInvoker.Execute(new Delete(TargetItems.ToList()));
    }

    private void InitState()
    {
        if (DataManager.CurrentSubLevel is null) return;
        
        SyncNodeByLevelData((SubLevel)DataManager.CurrentSubLevel);
    }

    private void InitButton()
    {
        AddButton.onClick.AddListener(() =>
        {
            if (!CheckStates.Contains(typeof(ItemWarehousePanelShowState))) ChangeMotionState(typeof(ItemWarehousePanelShowState));
        });

        DeleteButton.onClick.AddListener(() => { CommandInvoker.Execute(new Delete(TargetItems.ToList())); });
    }

    private void InitSyncEvent()
    {
        DataManager.SyncLevelData += SyncNodeByLevelData;
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
    
    private void CreateNode(List<LevelEditor.Item> targetItems)
    {
        foreach (var targetItem in targetItems) CreateNode(targetItem);
    }
    
    private void CreateNode(LevelEditor.Item targetItem)
    {
        var ItemView               = CreateChild(targetItem);
        var itemNodeChildTransform = ItemView.Transform;

        foreach (var itemNodeProperty in _itemViewList)
            if (itemNodeProperty is ItemView && itemNodeProperty.Type == targetItem.Type)
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
        
        ItemView = CreateParent(targetItem);
        itemNodeChildTransform.SetSiblingIndex(ItemView.Transform.GetSiblingIndex() + 1);
        ItemView.AddChild(ItemView);
        ItemView.ShowChild();
    }
    
    private void Delete(List<LevelEditor.Item> item)
    {
        _selectedItemList.Clear();

        foreach (var itemData in item) Delete(itemData);
    }
    
    private void Delete(LevelEditor.Item item)
    {
        Delete(item.View);
    }

    private void Delete(ItemView itemView)
    {
        itemView.Remove();
    }
    
    private ItemView CreateParent(LevelEditor.Item item)
    {
        var itemNodeParent = new ItemView(item, GetSelectedNode, ScrollView);
        _itemViewList.Add(itemNodeParent);
        itemNodeParent.Transform.SetSiblingIndex(ScrollViewContent.childCount);
        return itemNodeParent;
    }
    
    private ItemView CreateChild(LevelEditor.Item targetItem)
    {
        var itemNodeChild = new ItemView(targetItem, GetSelectedNode, ScrollView);
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
    
    private void SyncNodePanelSelect(List<LevelEditor.Item> itemData)
    {
        SyncNodePanelSelect();
    }
    
    private void SyncNodePanelSelect(LevelEditor.Item item)
    {
        SyncNodePanelSelect();
    }

    private void SyncNodePanelSelect()
    {
        _selectedItemList.Clear();
        _selectedItemList.AddRange(TargetItems);

        foreach (var itemNodeProperty in _itemViewList) itemNodeProperty.IsSelected = false;

        foreach (var targetItem in TargetItems)
        foreach (var itemNodeProperty in _itemViewList)
            if (itemNodeProperty is ItemView itemNodeChild && itemNodeChild.Item == targetItem)
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
            if (itemNodeProperty.Item != lastSelectData) continue;
            endSelect = itemNodeProperty.Transform.GetSiblingIndex();
            break;
        }

        var lower = Mathf.Min(startSelect, endSelect);
        var upper = Mathf.Max(startSelect, endSelect);

        foreach (var view in _itemViewList)
        {
            if
            (
                view.Transform.GetSiblingIndex() >= lower
             && view.Transform.GetSiblingIndex() <= upper
            )
            {
                SelectAddItem(view);
            }
        }
    }

    private void SelectAddItem(ItemView selectView)
    {
        if (selectView.IsSelected) return;

        if (selectView is ItemView child)
        {
            selectView.IsSelected = true;
            _selectedItemList.Add(child.Item);
            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(TargetItems, _selectedItemList, Outline));
            return;
        }

        if (selectView is ItemView selectNodeParent)
        {
            var tarGetAllChild = selectNodeParent.GetAllChild();

            foreach (var itemNodeChild in tarGetAllChild)
            {
                itemNodeChild.IsSelected = true;
                _selectedItemList.Add(itemNodeChild.Item);
            }

            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(TargetItems, _selectedItemList, Outline));
            selectView.IsSelected = true;
        }
    }

    private void SelectOppositeItem(ItemView selectView)
    {
        if (selectView is ItemView child)
        {
            selectView.IsSelected = !selectView.IsSelected;

            if (selectView.IsSelected)
                _selectedItemList.Add(child.Item);
            else
                _selectedItemList.Remove(child.Item);

            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(TargetItems, _selectedItemList, Outline));
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
                    _selectedItemList.Add(itemNodeChild.Item);
                else
                    _selectedItemList.Remove(itemNodeChild.Item);
            }

            _selectedItemList = _selectedItemList.Distinct().ToList();
            CommandInvoker.Execute(new Select(TargetItems, _selectedItemList, Outline));
            selectView.IsSelected = isSelectParent;
        }
    }
}