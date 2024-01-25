using Moon.Kernel.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelPanel
    {
        public UIProperty.PopoverProperty GetPopoverProperty => m_popoverProperty;
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

        private UIProperty.PopoverProperty m_popoverProperty;

        public LevelPanel(Transform levelEditorCanvasRect, UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
        }

        private void InitComponent(Transform levelEditor, UIProperty levelEditorUIProperty)
        {
            UIProperty.LevelPanelUIName property = levelEditorUIProperty.GetLevelPanelUI.GetLevelPanelUIName;
            m_popoverProperty = levelEditorUIProperty.GetPopoverProperty;
            m_levelName = levelEditor.FindPath(property.LEVEL_NAME).GetComponent<TextMeshProUGUI>();
            m_saveButton = levelEditor.FindPath(property.SAVE_BUTTON).GetComponent<Button>();
            m_releaseButton = levelEditor.FindPath(property.RELEASE_BUTTON).GetComponent<Button>();
            m_playButton = levelEditor.FindPath(property.PLAY_BUTTON).GetComponent<Button>();
            m_settingButton = levelEditor.FindPath(property.SETTING_BUTTON).GetComponent<Button>();
            m_exitButton = levelEditor.FindPath(property.EXIT_BUTTON).GetComponent<Button>();
        }
    }
}