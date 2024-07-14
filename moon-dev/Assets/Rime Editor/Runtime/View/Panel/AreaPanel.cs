using LevelEditor.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class AreaPanel
    {
        private Button m_addButton;

        private TMP_Dropdown m_areaDropdown;

        private Button m_areaSettingButton;

        private Button m_deleteButton;

        private TextMeshProUGUI m_describeText;

        private Button m_environmentSettingButton;

        private Button m_manageButton;

        public AreaPanel(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
        }

        public TextMeshProUGUI GetDescribeText => m_describeText;

        public TMP_Dropdown GetAreaDropdown => m_areaDropdown;

        public Button GetAddButton => m_addButton;

        public Button GetDeleteButton => m_deleteButton;

        public Button GetManageButton => m_manageButton;

        public Button GetAreaSettingButton => m_areaSettingButton;

        public Button GetEnvironmentSettingButton => m_environmentSettingButton;

        private void InitComponent(Transform canvasRect, UISetting levelEditorUISetting)
        {
            var property = levelEditorUISetting.GetAreaPanelUI.GetAreaPanelUIName;
            m_describeText             = canvasRect.FindPath(property.DESCRIBE_TEST).GetComponent<TextMeshProUGUI>();
            m_areaDropdown             = canvasRect.FindPath(property.AREA_DROP_DOWN).GetComponent<TMP_Dropdown>();
            m_addButton                = canvasRect.FindPath(property.ADD_BUTTON).GetComponent<Button>();
            m_deleteButton             = canvasRect.FindPath(property.DELETE_BUTTON).GetComponent<Button>();
            m_manageButton             = canvasRect.FindPath(property.MANAGE_BUTTON).GetComponent<Button>();
            m_areaSettingButton        = canvasRect.FindPath(property.AREA_SETTING_BUTTON).GetComponent<Button>();
            m_environmentSettingButton = canvasRect.FindPath(property.ENVIRONMENT_SETTING_BUTTON).GetComponent<Button>();
        }
    }
}