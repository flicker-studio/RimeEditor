using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnSelect(ItemNode selectNode);

public class HierarchyPanelShowState : AdditiveState
{
    private HierarchyPanel GetHierarchyPanel => m_information.GetUI.GetHierarchyPanel;

    private Transform GetScrollViewContent => GetHierarchyPanel.GetScrollViewContent;

    private CommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

    private ObservableList<ItemData> TargetItems => m_information.TargetItems;

    private ObservableList<ItemData> ItemAssets => m_information.ItemAssets;

    private OutlinePainter GetOutlinePainter => m_information.GetOutlinePainter;

    private Button GetAddButton => GetHierarchyPanel.GetAddButton;

    private Button GetDeleteButton => GetHierarchyPanel.GetDeleteButton;

    private bool GetShiftInput => m_information.GetInput.GetShiftButton;

    private bool GetCtrlInput => m_information.GetInput.GetCtrlButton;

    private OnSelect m_onSelect;

    private List<ItemNode> m_itemNodeProperties = new List<ItemNode>();

    private List<ItemData> m_selectTargetItem = new List<ItemData>();
    
    public HierarchyPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitEvent();
        InitButton();
    }

    public override void Motion(BaseInformation information)
    {
        
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
        m_onSelect = GetSelectedNode;
        ItemAssets.OnAdd += CreateNode;
        ItemAssets.OnAdd += SynchronousNodePanel;
        ItemAssets.OnAddRange += CreateNode;
        ItemAssets.OnRemove += DeleteNode;
        ItemAssets.OnRemove += SynchronousNodePanel;
        ItemAssets.OnRemoveAll += DeleteNode;
        TargetItems.OnAddRange += SynchronousNodePanel;
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
            if (itemNodeProperty as ItemNodeParent != null && itemNodeProperty.Itemtype == targetItem.GetItemProduct.ItemType)
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
        ItemNodeParent itemNodeParent = new ItemNodeParent(itemProduct,GetScrollViewContent, m_onSelect);
        m_itemNodeProperties.Add(itemNodeParent);
        itemNodeParent.ItemNodeTransform.SetSiblingIndex(GetScrollViewContent.childCount);
        return itemNodeParent;
    }

    private ItemNodeChild CreateChild(ItemData targetItem)
    {
        ItemNodeChild itemNodeChild = new ItemNodeChild(targetItem.GetItemProduct, GetScrollViewContent,m_onSelect, targetItem);
        m_itemNodeProperties.Add(itemNodeChild);
        return itemNodeChild;
    }
    
    private void SynchronousNodePanel(List<ItemData> itemData)
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

    private void SynchronousNodePanel(ItemData itemData)
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
            SelectAddItem(selectNode);
        }
        else if(GetCtrlInput)
        {
            SelectOppositeItem(selectNode);
        }
        else
        {
            SelectOneItem(selectNode);
        }
    }

    private void SelectOneItem(ItemNode selectNode)
    {
        m_selectTargetItem.Clear();
        
        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            itemNodeProperty.IsSelected = false;
        }

        SelectAddItem(selectNode);
    }

    private void SelectAddItem(ItemNode selectNode)
    {
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
        }
        selectNode.IsSelected = true;
        m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
        GetExcute?.Invoke(new ItemSelectCommand(TargetItems,m_selectTargetItem,GetOutlinePainter));
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
            selectNode.IsSelected = !selectNode.IsSelected;
            List<ItemNodeChild> targetChilds = selectNodeParent.GetChilds();
            foreach (var itemNodeChild in targetChilds)
            {
                itemNodeChild.IsSelected = selectNode.IsSelected;
                m_selectTargetItem.Add(itemNodeChild.ItemData);
                if (itemNodeChild.IsSelected)
                {
                    m_selectTargetItem.Add(itemNodeChild.ItemData);
                }
                else
                {
                    m_selectTargetItem.Remove(itemNodeChild.ItemData);
                }
            }
        }
        m_selectTargetItem = m_selectTargetItem.Distinct().ToList();
        GetExcute?.Invoke(new ItemSelectCommand(TargetItems,m_selectTargetItem,GetOutlinePainter));
    }
}
