using Frame.Static.Extensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelPanel
    {
        public TextMeshProUGUI GetLevelName => m_levelName;

        public Button GetSaveButton => m_saveButton;
        
        public Button GetReleaseButton => m_releaseButton;
        
        public Button GetPlayButton => m_playButton;
        
        private TextMeshProUGUI m_levelName;

        private Button m_saveButton;

        private Button m_releaseButton;

        private Button m_playButton;
        
        public LevelPanel(Transform levelEditorCanvasRect, UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
        }

        private void InitComponent(Transform levelEditor, UIProperty levelEditorUIProperty)
        {
            UIProperty.LevelPanelUIName property = levelEditorUIProperty.GetLevelPanelUI.GetLevelPanelUIName;
            m_levelName = levelEditor.FindPath(property.LEVEL_NAME).GetComponent<TextMeshProUGUI>();
            m_saveButton = levelEditor.FindPath(property.SAVE_BUTTON).GetComponent<Button>();
            m_releaseButton = levelEditor.FindPath(property.RELEASE_BUTTON).GetComponent<Button>();
            m_playButton = levelEditor.FindPath(property.PLAY_BUTTON).GetComponent<Button>();
        }
    }
}
