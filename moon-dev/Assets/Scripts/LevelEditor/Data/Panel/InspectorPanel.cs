using Moon.Kernel.Extension;
using TMPro;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class InspectorPanel
    {
        public RectTransform GetInspectorRootRect => m_inspectorRootRect;

        public RectTransform GetInspectorContentRect => m_inspectorContentRect;

        public TextMeshProUGUI GetInspectorDescribeText => m_inspectorDescribeText;

        public UISetting.InspectorItemProperty GetInspectorItemProperty { get; private set; }

        private RectTransform m_inspectorRootRect;

        private RectTransform m_inspectorContentRect;

        private TextMeshProUGUI m_inspectorDescribeText;

        public InspectorPanel(RectTransform rect, UISetting levelEditorUISetting)
        {
            InitComponent(rect, levelEditorUISetting);
        }

        private void InitComponent(RectTransform rect, UISetting levelEditorUISetting)
        {
            var property = levelEditorUISetting.GetInspectorPanelUI.GetInspectorPanelUIName;
            GetInspectorItemProperty = levelEditorUISetting.GetInspectorPanelUI.GetInspectorItemProperty;
            m_inspectorRootRect = rect.FindPath(property.INSPECTOR_ROOT) as RectTransform;
            m_inspectorContentRect = rect.FindPath(property.INSPECTOR_CONTENT) as RectTransform;
            m_inspectorDescribeText = rect.FindPath(property.DESCRIBE_TEXT).GetComponent<TextMeshProUGUI>();
        }
    }
}