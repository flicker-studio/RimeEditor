using Moon.Kernel.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelPanel
    {
        public UISetting.PopoverProperty GetPopoverProperty { get; private set; }

        public TextMeshProUGUI GetLevelName => m_levelName;

        public Button GetSaveButton => m_saveButton;

        public Button GetReleaseButton => m_releaseButton;
        public Button GetSettingButton => m_settingButton;

        public Button GetPlayButton => m_playButton;

        public Button GetExitButton => m_exitButton;

        private TextMeshProUGUI m_levelName;

        private Button m_saveButton;

        private Button m_releaseButton;

        private Button m_playButton;

        private Button m_settingButton;

        private Button m_exitButton;

        public LevelPanel(Transform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
        }

        private void InitComponent(Transform levelEditor, UISetting levelEditorUISetting)
        {
            var property = levelEditorUISetting.GetLevelPanelUI.GetLevelPanelUIName;
            GetPopoverProperty = levelEditorUISetting.GetPopoverProperty;
            m_levelName = levelEditor.FindPath(property.LEVEL_NAME).GetComponent<TextMeshProUGUI>();
            m_saveButton = levelEditor.FindPath(property.SAVE_BUTTON).GetComponent<Button>();
            m_releaseButton = levelEditor.FindPath(property.RELEASE_BUTTON).GetComponent<Button>();
            m_playButton = levelEditor.FindPath(property.PLAY_BUTTON).GetComponent<Button>();
            m_settingButton = levelEditor.FindPath(property.SETTING_BUTTON).GetComponent<Button>();
            m_exitButton = levelEditor.FindPath(property.EXIT_BUTTON).GetComponent<Button>();
        }
    }
}