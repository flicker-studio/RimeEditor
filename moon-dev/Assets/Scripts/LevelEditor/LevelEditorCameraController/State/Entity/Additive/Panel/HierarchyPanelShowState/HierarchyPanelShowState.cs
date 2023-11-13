using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public class HierarchyPanelShowState : AdditiveState
{
    private HierarchyPanel GetHierarchyPanel => m_information.GetUI.GetHierarchyPanel;

    private Transform GetScrollViewContent => GetHierarchyPanel.GetHierarchyContent;

    private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

    private ObservableList<ItemData> TargetItems => m_information.TargetItems;

    private ObservableList<ItemData> ItemAssets => m_information.ItemAssets;

    private OutlinePainter GetOutlinePainter => m_information.GetOutlinePainter;

    private Button GetAddButton => GetHierarchyPanel.GetAddButton;

    private Button GetDeleteButton => GetHierarchyPanel.GetDeleteButton;

    private ScrollRect GetScrollView => GetHierarchyPanel.GetScrollView;

    private bool GetShiftInput => m_information.GetInput.GetShiftButton;

    private bool GetCtrlInput => m_information.GetInput.GetCtrlButton;

    private bool GetDeleteInputDown => m_information.GetInput.GetDeleteButtonDown;

    private List<ItemNode> m_itemNodeProperties = new List<ItemNode>();

    private List<ItemData> m_selectTargetItem = new List<ItemData>();
    
    public HierarchyPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitEvent();
        InitButton();
    }

    public override void Motion(BaseInformation information)
    {
        if (GetDeleteInputDown)
        {
            GetExcute?.Invoke(new ItemDeleteCommand(TargetItems,ItemAssets,GetOutlinePainter));
        }
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
        
        GetDeleteButton.onClick.AddListener(() =>
        {
            GetExcute?.Invoke(new ItemDeleteCommand(TargetItems,ItemAssets,GetOutlinePainter));
        });
    }

    private void InitEvent()
    {
        ItemAssets.OnAdd += CreateNode;
        ItemAssets.OnAdd += SyncNodePanel;
        ItemAssets.OnAddRange += CreateNode;
        ItemAssets.OnRemove += DeleteNode;
        ItemAssets.OnRemove += SyncNodePanel;
        ItemAssets.OnRemoveAll += DeleteNode;
        TargetItems.OnAddRange += SyncNodePanel;
    }

    private void CreateNode(List<ItemData> targetItems)
    {
        foreach (var targetItem in targetItems)
        {
            CreateNode(targetItem);
        }
    }
    
    private void CreateNode(ItemData targetItem)
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

    private void DeleteNode(List<ItemData> itemDatas)
    {
        m_selectTargetItem.Clear();
        foreach (var itemData in itemDatas)
        {
            DeleteNode(itemData);
        }
    }
    
    private void DeleteNode(ItemData itemData)
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
        ItemNodeParent itemNodeParent = new ItemNodeParent(itemProduct,GetScrollViewContent, GetSelectedNode,GetScrollView);
        m_itemNodeProperties.Add(itemNodeParent);
        itemNodeParent.ItemNodeTransform.SetSiblingIndex(GetScrollViewContent.childCount);
        return itemNodeParent;
    }

    private ItemNodeChild CreateChild(ItemData targetItem)
    {
        ItemNodeChild itemNodeChild = new ItemNodeChild(targetItem.GetItemProduct, GetScrollViewContent,GetSelectedNode, targetItem,GetScrollView);
        m_itemNodeProperties.Add(itemNodeChild);
        return itemNodeChild;
    }
    
    private void SyncNodePanel(List<ItemData> itemData)
    {
        SyncNodePanel();
    }

    private void SyncNodePanel(ItemData itemData)
    {
        SyncNodePanel();
    }
    
    private void SyncNodePanel()
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
        if(GetCtrlInput)
        {
            SelectOppositeItem(selectNode);
            return;
        }
        SelectSingleItem(selectNode);
    }

    private void SelectSingleItem(ItemNode selectNode)
    {
        if(selectNode.IsSelected && m_selectTargetItem.Count == 1) return;
        
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
        
        ItemData lastSelectData = m_selectTargetItem.Last();

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
        if(selectNode.IsSelected) return;
        if (selectNode is ItemNodeChild child)
        {
            selectNode.IsSelected = true;
            m_selectTargetItem.Add(child.ItemData);
            m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
            GetExcute?.Invoke(new ItemSelectCommand(TargetItems,m_selectTargetItem,GetOutlinePainter));
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
            GetExcute?.Invoke(new ItemSelectCommand(TargetItems,m_selectTargetItem,GetOutlinePainter));
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
            GetExcute?.Invoke(new ItemSelectCommand(TargetItems,m_selectTargetItem,GetOutlinePainter));
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
            GetExcute?.Invoke(new ItemSelectCommand(TargetItems,m_selectTargetItem,GetOutlinePainter));
            selectNode.IsSelected = isSelectParent;
        }
    }
}
