using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class HierarchyPanel
    {
        public Transform GetHierarchyContent => m_hierarchyContent;

        public Button GetAddButton => m_addButton;

        public Button GetDeleteButton => m_deleteButton;

        public ScrollRect GetScrollView => m_scrollView;

        private Button m_addButton;

        private Button m_deleteButton;

        private ScrollRect m_scrollView;

        private Transform m_hierarchyContent;

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
            m_hierarchyContent = levelEditorCanvasRect.FindPath(hierarchyPanelUIName.HIERARCHY_VIEW_CONTENT);
            m_scrollView = levelEditorCanvasRect.FindPath(hierarchyPanelUIName.SCROLL_VIEW).GetComponent<ScrollRect>();
        }

        private void InitEvent()
        {
        }
    }
}