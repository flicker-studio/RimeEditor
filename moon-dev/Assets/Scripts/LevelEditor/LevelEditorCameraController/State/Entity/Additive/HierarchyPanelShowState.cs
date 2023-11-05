using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public delegate void OnSelect(ItemNode selectNode);

public class HierarchyPanelShowState : AdditiveState
{
    private HierarchyPanel GetHierarchyPanel => m_information.GetUI.GetHierarchyPanel;
    
    private GameObject GetItemNodeGameObject => m_information.GetItemNodeGameObject;
    
    public Color GetHighLightedColor => GetHierarchyPanel.GetHighLightedColor;

    public Color GetSelectedColor => GetHierarchyPanel.GetSelectedColor;

    public Button GetAddButton => GetHierarchyPanel.GetAddButton;

    public Button GetDeleteButton => GetHierarchyPanel.GetDeleteButton;

    private OnSelect m_onSelect;

    private List<ItemNode> m_itemNodeProperties = new List<ItemNode>();

    private List<ItemNode> m_selectItemNode = new List<ItemNode>();
    
    public HierarchyPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        InitButton();
    }

    public override void Motion(BaseInformation information)
    {
        
    }

    private void InitButton()
    {
        GetAddButton.onClick.AddListener(() =>
        {
            ChangeMotionState(typeof(ItemWarehousePanelShowState));
        });
    }

    private void GetSelectedNode(ItemNode selectNode)
    {
        m_selectItemNode.Clear();
        
        foreach (var itemNodeProperty in m_itemNodeProperties)
        {
            itemNodeProperty.IsSelected = false;
        }

        selectNode.IsSelected = true;
        
        if (selectNode is ItemNodeChild)
        {
            m_selectItemNode.Add(selectNode);
            return;
        }

        if (selectNode is ItemNodeParent selectNodeParent)
        {
            List<ItemNodeChild> targetChilds = selectNodeParent.GetTargetChilds();
            foreach (var itemNodeChild in targetChilds)
            {
                itemNodeChild.IsSelected = true;
                m_selectItemNode.Add(itemNodeChild);
            }
        }
    }
}
