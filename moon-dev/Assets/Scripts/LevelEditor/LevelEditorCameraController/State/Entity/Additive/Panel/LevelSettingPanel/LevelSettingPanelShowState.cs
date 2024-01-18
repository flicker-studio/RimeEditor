using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelSettingPanelShowState : AdditiveState
    {
        private DataManager GetData => m_information.GetData;
        private LevelData GetCurrentLevel => GetData.GetCurrentLevel;
        private LevelSettingPanel GetLevelSettingPanel => m_information.GetUI.GetLevelSettingPanel;

        private GameObject GetPopoverPanelObj => GetLevelSettingPanel.GetPopoverPanelObj;

        private GameObject GetLevelSettingPanelObj => GetLevelSettingPanel.GetLevelSettingPanelObj;
        
        private Button GetCloseButton => GetLevelSettingPanel.GetCloseButton;
        
        private Button GetSaveButton => GetLevelSettingPanel.GetSaveButton;

        private Button GetCoverImageButton => GetLevelSettingPanel.GetCoverImageButton;

        private RawImage GetCoverImage => GetLevelSettingPanel.GetCoverImage;
        
        private TMP_InputField GetLevelNameInputField => GetLevelSettingPanel.GetLevelNameInputField;
        
        private TMP_InputField GetAuthorNameInputField => GetLevelSettingPanel.GetAuthorNameInputField;
        
        private TMP_InputField GetVersionInputField => GetLevelSettingPanel.GetVersionInputField;
        
        private TMP_InputField GetIntroductionInputField => GetLevelSettingPanel.GetIntroductionInputField;

        private string m_coverImagePath;
        
        public LevelSettingPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
        }

        private void InitState()
        {
            GetPopoverPanelObj.SetActive(true);
            GetLevelSettingPanelObj.SetActive(true);
            GetCloseButton.onClick.AddListener(RemoveState);
            GetSaveButton.onClick.AddListener(SaveLevel);
            GetCoverImageButton.onClick.AddListener(UploadCoverImage);
            InitInputField();
        }

        protected override void RemoveState()
        {
            GetLevelSettingPanelObj.SetActive(false);
            GetPopoverPanelObj.SetActive(false);
            GetCloseButton.onClick.RemoveAllListeners();
            GetSaveButton.onClick.RemoveAllListeners();
            GetCoverImageButton.onClick.RemoveAllListeners();
            base.RemoveState();
        }

        private void SaveLevel()
        {
            GetCurrentLevel.SetName = GetLevelNameInputField.text;
            GetCurrentLevel.SetAuthorName = GetAuthorNameInputField.text;
            GetCurrentLevel.SetIntroduction = GetIntroductionInputField.text;
            GetCurrentLevel.SetVersion = GetVersionInputField.text;
            GetCurrentLevel.SetLevelCoverImage = GetCoverImage.texture as Texture2D;
            RemoveState();
        }

        protected void UploadCoverImage()
        {
            m_coverImagePath = EditorUtility.OpenFilePanel("Overwrite with png", "", "png,jpg");
            CheckImage();
        }
        
        public override void Motion(BaseInformation information)
        {
            
        }

        private void InitInputField()
        {
            GetLevelNameInputField.text = GetCurrentLevel.GetName;
            GetAuthorNameInputField.text = GetCurrentLevel.GetAuthorName;
            GetIntroductionInputField.text = GetCurrentLevel.GetIntroduction;
            GetVersionInputField.text = GetCurrentLevel.GetVersion;
            GetCoverImage.texture = GetCurrentLevel.GetLevelCoverImage;
        }
        
        private void CheckImage()
        {
            if (m_coverImagePath != null && m_coverImagePath != "")
            {
                UniTask.Void(UpdateImage);
            }
        }

        private async UniTaskVoid UpdateImage()
        {
            UnityWebRequest uwr = UnityWebRequestTexture.GetTexture("file:///" + m_coverImagePath);
            await uwr.SendWebRequest();;
            GetCoverImage.texture = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}
