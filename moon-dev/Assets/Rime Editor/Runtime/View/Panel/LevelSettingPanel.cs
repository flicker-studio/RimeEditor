using LevelEditor.Extension;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelSettingPanel
    {
        private TMP_InputField m_authorNameInputField;
        private Button         m_closeButton;
        private RawImage       m_coverImage;
        private Button         m_coverImageButton;
        private TMP_InputField m_introductionInputField;
        private TMP_InputField m_levelNameInputField;
        private Transform      m_levelSettingPanelTransform;

        private Transform      m_popoverPanelTransform;
        private Button         m_saveButton;
        private TMP_InputField m_versionInputField;

        public LevelSettingPanel(Transform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
        }

        public UISetting.PopoverProperty GetPopoverProperty        { get; private set; }
        public GameObject                GetPopoverPanelObj        => m_popoverPanelTransform.gameObject;
        public GameObject                GetLevelSettingPanelObj   => m_levelSettingPanelTransform.gameObject;
        public RawImage                  GetCoverImage             => m_coverImage;
        public Button                    GetCloseButton            => m_closeButton;
        public Button                    GetSaveButton             => m_saveButton;
        public Button                    GetCoverImageButton       => m_coverImageButton;
        public TMP_InputField            GetLevelNameInputField    => m_levelNameInputField;
        public TMP_InputField            GetAuthorNameInputField   => m_authorNameInputField;
        public TMP_InputField            GetVersionInputField      => m_versionInputField;
        public TMP_InputField            GetIntroductionInputField => m_introductionInputField;

        private void InitComponent(Transform levelEditor, UISetting levelEditorUISetting)
        {
            var property = levelEditorUISetting.GetLevelSettingPanelUI.GetLevelSettingPanelUIName;
            GetPopoverProperty           = levelEditorUISetting.GetPopoverProperty;
            m_popoverPanelTransform      = levelEditor.FindPath(property.POPOVER_PANEL);
            m_levelSettingPanelTransform = levelEditor.FindPath(property.SETTING_PANEL);
            m_coverImage                 = levelEditor.FindPath(property.COVER_IMAGE_NAME).GetComponent<RawImage>();
            m_closeButton                = levelEditor.FindPath(property.CLOSE_BUTTON_NAME).GetComponent<Button>();
            m_saveButton                 = levelEditor.FindPath(property.SAVE_BUTTON_NAME).GetComponent<Button>();
            m_coverImageButton           = levelEditor.FindPath(property.COVER_IMAGE_NAME).GetComponent<Button>();
            m_levelNameInputField        = levelEditor.FindPath(property.LEVEL_NAME_INPUTFIELD).GetComponent<TMP_InputField>();
            m_authorNameInputField       = levelEditor.FindPath(property.AUTHOR_NAME_INPUTFIELD).GetComponent<TMP_InputField>();
            m_versionInputField          = levelEditor.FindPath(property.VERSION_INPUTFIELD).GetComponent<TMP_InputField>();
            m_introductionInputField     = levelEditor.FindPath(property.INTRODUCTION_INPUTFIELD).GetComponent<TMP_InputField>();
        }
    }
}