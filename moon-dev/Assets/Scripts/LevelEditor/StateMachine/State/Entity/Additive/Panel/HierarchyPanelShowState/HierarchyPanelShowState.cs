using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using LevelEditor;
using LevelEditor.Command;
using Moon.Kernel.Utils;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyPanelShowState : AdditiveState
{
    private LevelDataManager GetData => m_information.DataManager;
    private HierarchyPanel GetHierarchyPanel => m_information.UIManager.GetHierarchyPanel;

    private Transform GetScrollViewContent => GetHierarchyPanel.GetHierarchyContent;


    private ObservableList<ItemDataBase> TargetItems => GetData.TargetItems;

    private ObservableList<ItemDataBase> ItemAssets => GetData.ItemAssets;

    private OutlineManager GetOutlinePainter => m_information.OutlineManager;

    private Button GetAddButton => GetHierarchyPanel.GetAddButton;

    private Button GetDeleteButton => GetHierarchyPanel.GetDeleteButton;

    private ScrollRect GetScrollView => GetHierarchyPanel.GetScrollView;

    private bool GetShiftInput => m_information.InputManager.GetShiftButton;

    private bool GetCtrlInput => m_information.InputManager.GetCtrlButton;

    private bool GetDeleteInputDown => m_information.InputManager.GetDeleteButtonDown;

    private List<ItemNode> m_itemNodeProperties = new List<ItemNode>();

    private List<ItemDataBase> m_selectTargetItem = new();

    public HierarchyPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitSyncEvent();
        InitEvent();
        InitButton();
        InitState();
    }

    public override void Motion(BaseInformation information)
    {
        if (GetDeleteInputDown)
        {
            CommandInvoker.Execute(new ItemDeleteCommand(TargetItems, ItemAssets, GetOutlinePainter));
        }
    }

    private void InitState()
    {
        if (GetData.CurrentSubLevel is null)
        {
            return;
        }

        SyncNodeByLevelData((SubLevelData)GetData.CurrentSubLevel);
    }

    private void InitButton()
    {
        GetAddButton.onClick.AddListener(() =>
        {
            if (!CheckStates.Contains(typeof(ItemWarehousePanelShowState)))
            {
                ChangeMotionState(typeof(ItemWarehousePanelShowState));
            }
        });

        GetDeleteButton.onClick.AddListener(() => { CommandInvoker.Execute(new ItemDeleteCommand(TargetItems, ItemAssets, GetOutlinePainter)); });
    }

    private void InitSyncEvent()
    {
        GetData.SyncLevelData += SyncNodeByLevelData;
    }

    private void InitEvent()
    {
        ItemAssets.OnAdd -= CreateNode;
        ItemAssets.OnAdd -= SyncNodePanelSelete;
        ItemAssets.OnAddRange -= CreateNode;
        ItemAssets.OnRemove -= DeleteNode;
        ItemAssets.OnRemove -= SyncNodePanelSelete;
        ItemAssets.OnRemoveAll -= DeleteNode;
        TargetItems.OnAddRange -= SyncNodePanelSelete;

        ItemAssets.OnAdd += CreateNode;
        ItemAssets.OnAdd += SyncNodePanelSelete;
        ItemAssets.OnAddRange += CreateNode;
        ItemAssets.OnRemove += DeleteNode;
        ItemAssets.OnRemove += SyncNodePanelSelete;
        ItemAssets.OnRemoveAll += DeleteNode;
        TargetItems.OnAddRange += SyncNodePanelSelete;
    }

    private void CreateNode(List<ItemDataBase> targetItems)
    {
        foreach (var targetItem in targetItems)
        {
            CreateNode(targetItem);
        }
    }

    private void CreateNode(ItemDataBase targetItem)
    {
        ItemNodeChild itemNodeChild = CreateChild(targetItem);
        ItemNodeParent itemNodeParent;
        Transform itemNodeChildTransform = itemNodeChild.ItemNodeTransform;

        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            if (itemNodeProperty as ItemNodeParent != null && itemNodeProperty.Itemtypeenum == targetItem.GetItemProduct.ItemType)
            {
                itemNodeParent = itemNodeProperty as ItemNodeParent;
                List<ItemNodeChild> parentChilds = itemNodeParent.GetChilds();
                int lastIndex;

                if (parentChilds.Count > 0)
                {
                    lastIndex = parentChilds.Last().ItemNodeTransform.GetSiblingIndex();
                }
                else
                {
                    lastIndex = itemNodeParent.ItemNodeTransform.GetSiblingIndex();
                }

                itemNodeChildTransform.SetSiblingIndex(lastIndex + 1);
                itemNodeParent.AddChild(itemNodeChild);
                itemNodeParent.ShowChilds();
                return;
            }
        }

        itemNodeParent = CreateParent(targetItem.GetItemProduct);
        itemNodeChildTransform.SetSiblingIndex(itemNodeParent.ItemNodeTransform.GetSiblingIndex() + 1);
        itemNodeParent.AddChild(itemNodeChild);
        itemNodeParent.ShowChilds();
    }

    private void DeleteNode(List<ItemDataBase> itemDatas)
    {
        m_selectTargetItem.Clear();

        foreach (var itemData in itemDatas)
        {
            DeleteNode(itemData);
        }
    }

    private void DeleteNode(ItemDataBase itemData)
    {
        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            if (itemNodeProperty is ItemNodeChild child && child.ItemData == itemData)
            {
                DeleteNode(child);
                return;
            }
        }
    }

    private void DeleteNode(ItemNode itemNode)
    {
        if (itemNode is ItemNodeChild itemNodeChild)
        {
            foreach (var itemNodeProperty in m_itemNodeProperties)
            {
                if (itemNodeProperty is ItemNodeParent parent && parent.GetChilds().Contains(itemNodeChild))
                {
                    parent.RemoveChild(itemNodeChild);
                }
            }
        }

        if (itemNode is ItemNodeParent itemNodeParent)
        {
            List<ItemNodeChild> childs = itemNodeParent.GetChilds();

            foreach (var nodeChild in childs)
            {
                m_itemNodeProperties.Remove(nodeChild);
                nodeChild.RemoveNode();
            }

            childs.Clear();
        }

        m_itemNodeProperties.Remove(itemNode);
        itemNode.RemoveNode();
    }

    private ItemNodeParent CreateParent(ItemProduct itemProduct)
    {
        var itemNodeParent = new ItemNodeParent(itemProduct, GetScrollViewContent, GetSelectedNode, GetScrollView);
        m_itemNodeProperties.Add(itemNodeParent);
        itemNodeParent.ItemNodeTransform.SetSiblingIndex(GetScrollViewContent.childCount);
        return itemNodeParent;
    }

    private ItemNodeChild CreateChild(ItemDataBase targetItem)
    {
        var itemNodeChild = new ItemNodeChild(targetItem.GetItemProduct, GetScrollViewContent, GetSelectedNode, targetItem, GetScrollView);
        m_itemNodeProperties.Add(itemNodeChild);
        return itemNodeChild;
    }

    private void SyncNodeByLevelData(SubLevelData subLevelData)
    {
        InitEvent();
        ClearNode();
        var itemDatas = subLevelData.ItemAssets;

        foreach (var itemData in itemDatas)
        {
            CreateNode(itemData);
        }
    }

    private void ClearNode()
    {
        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            itemNodeProperty.RemoveNode();
        }

        m_itemNodeProperties.Clear();
    }

    private void SyncNodePanelSelete(List<ItemDataBase> itemData)
    {
        SyncNodePanelSelete();
    }

    private void SyncNodePanelSelete(ItemDataBase itemData)
    {
        SyncNodePanelSelete();
    }

    private void SyncNodePanelSelete()
    {
        m_selectTargetItem.Clear();
        m_selectTargetItem.AddRange(TargetItems);

        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            itemNodeProperty.IsSelected = false;
        }

        foreach (var targetItem in TargetItems)
        {
            foreach (var itemNodeProperty in m_itemNodeProperties)
            {
                if (itemNodeProperty is ItemNodeChild itemNodeChild && itemNodeChild.ItemData == targetItem)
                {
                    itemNodeChild.IsSelected = true;
                    break;
                }
            }
        }
    }

    private void GetSelectedNode(ItemNode selectNode)
    {
        if (GetShiftInput)
        {
            SelectMultiItem(selectNode);
            return;
        }

        if (GetCtrlInput)
        {
            SelectOppositeItem(selectNode);
            return;
        }

        SelectSingleItem(selectNode);
    }

    private void SelectSingleItem(ItemNode selectNode)
    {
        if (selectNode.IsSelected && m_selectTargetItem.Count == 1)
        {
            return;
        }

        m_selectTargetItem.Clear();

        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            itemNodeProperty.IsSelected = false;
        }

        SelectAddItem(selectNode);
    }

    private void SelectMultiItem(ItemNode selecteNode)
    {
        if (m_selectTargetItem.Count == 0)
        {
            SelectAddItem(selecteNode);
            return;
        }

        int startSelect = selecteNode.ItemNodeTransform.GetSiblingIndex();

        int endSelect = startSelect;

        var lastSelectData = m_selectTargetItem.Last();

        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            if (itemNodeProperty is ItemNodeChild lastSelectNode && lastSelectNode.ItemData == lastSelectData)
            {
                endSelect = lastSelectNode.ItemNodeTransform.GetSiblingIndex();
                break;
            }
        }

        int lower = Mathf.Min(startSelect, endSelect);
        int upper = Mathf.Max(startSelect, endSelect);

        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            if (itemNodeProperty is ItemNodeChild child && child.ItemNodeTransform.GetSiblingIndex() >= lower
                                                        && child.ItemNodeTransform.GetSiblingIndex() <= upper)
            {
                SelectAddItem(child);
            }
        }
    }

    private void SelectAddItem(ItemNode selectNode)
    {
        if (selectNode.IsSelected)
        {
            return;
        }

        if (selectNode is ItemNodeChild child)
        {
            selectNode.IsSelected = true;
            m_selectTargetItem.Add(child.ItemData);
            m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
            CommandInvoker.Execute(new ItemSelectCommand(TargetItems, m_selectTargetItem, GetOutlinePainter));
            return;
        }

        if (selectNode is ItemNodeParent selectNodeParent)
        {
            List<ItemNodeChild> targetChilds = selectNodeParent.GetChilds();

            foreach (var itemNodeChild in targetChilds)
            {
                itemNodeChild.IsSelected = true;
                m_selectTargetItem.Add(itemNodeChild.ItemData);
            }

            m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
            CommandInvoker.Execute(new ItemSelectCommand(TargetItems, m_selectTargetItem, GetOutlinePainter));
            selectNode.IsSelected = true;
        }
    }

    private void SelectOppositeItem(ItemNode selectNode)
    {
        if (selectNode is ItemNodeChild child)
        {
            selectNode.IsSelected = !selectNode.IsSelected;

            if (selectNode.IsSelected)
            {
                m_selectTargetItem.Add(child.ItemData);
            }
            else
            {
                m_selectTargetItem.Remove(child.ItemData);
            }

            m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
            CommandInvoker.Execute(new ItemSelectCommand(TargetItems, m_selectTargetItem, GetOutlinePainter));
            return;
        }

        if (selectNode is ItemNodeParent selectNodeParent)
        {
            List<ItemNodeChild> targetChilds = selectNodeParent.GetChilds();
            bool isSelectParent = !selectNode.IsSelected;

            foreach (var itemNodeChild in targetChilds)
            {
                itemNodeChild.IsSelected = isSelectParent;

                if (itemNodeChild.IsSelected)
                {
                    m_selectTargetItem.Add(itemNodeChild.ItemData);
                }
                else
                {
                    m_selectTargetItem.Remove(itemNodeChild.ItemData);
                }
            }

            m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
            CommandInvoker.Execute(new ItemSelectCommand(TargetItems, m_selectTargetItem, GetOutlinePainter));
            selectNode.IsSelected = isSelectParent;
        }
    }
}