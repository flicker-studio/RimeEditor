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

        private UISetting.HierarchyPanelUI m_property;

        public HierarchyPanel(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
            InitEvent();
        }

        private void InitComponent(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            m_property = levelEditorUISetting.GetHierarchyPanelUI;
            var hierarchyPanelUIName = m_property.GetHierarchyPanelUIName;
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