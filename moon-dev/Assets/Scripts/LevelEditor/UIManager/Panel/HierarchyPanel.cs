using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class HierarchyPanel
    {
        public Transform GetScrollViewContent => m_scrollViewContent;

        public Color GetHighLightedColor => m_highLightedColor;

        public Color GetSelectedColor => m_selectedColor;

        public Button GetAddButton => m_addButton;

        public Button GetDeleteButton => m_deleteButton;
        
        private Button m_addButton;

        private Button m_deleteButton;

        public Color m_highLightedColor;

        public Color m_selectedColor;

        private Transform m_scrollViewContent;

        private UIProperty.HierarchyPanelUI m_property;
        
        public HierarchyPanel(RectTransform levelEditorCanvasRect, UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
            InitEvent();
        }

        private void InitComponent(RectTransform levelEditorCanvasRect, UIProperty levelEditorUIProperty)
        {
            m_property = levelEditorUIProperty.GetHierarchyPanelUI;
            UIProperty.HierarchyPanelUIName hierarchyPanelUIName = m_property.GetHierarchyPanelUIName;
            m_addButton = levelEditorCanvasRect.FindPath(hierarchyPanelUIName.ADD_BUTTON).GetComponent<Button>();
            m_deleteButton = levelEditorCanvasRect.FindPath(hierarchyPanelUIName.DELETE_BUTTON).GetComponent<Button>();
            m_scrollViewContent = levelEditorCanvasRect.FindPath(hierarchyPanelUIName.SCROLL_VIEW_CONTENT);
            UIProperty.ItemNodeProperty itemNodeProperty = m_property.GetItemNodeProperty;
            m_highLightedColor = itemNodeProperty.HIGH_LIGHTED_COLOR;
            m_selectedColor = itemNodeProperty.SELECTED_COLOR;
        }

        private void InitEvent()
        {
        }
    }
}
