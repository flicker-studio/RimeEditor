using System;
using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using Frame.Tool.Popover;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelSettingPanelShowState : AdditiveState
    {
        private InputManager GetInput => m_information.InputManager;
        private DataManager GetData => m_information.DataManager;
        private LevelData GetCurrentLevel => GetData.GetCurrentLevel;
        private LevelSettingPanel GetLevelSettingPanel => m_information.UIManager.GetLevelSettingPanel;

        private UIProperty.PopoverProperty GetPopoverProperty => GetLevelSettingPanel.GetPopoverProperty;

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
            GetInput.SetCanInput(false);
            GetCloseButton.onClick.AddListener(RemoveState);
            GetSaveButton.onClick.AddListener(SaveLevel);
            GetCoverImageButton.onClick.AddListener(UploadCoverImage);
            InitInputField();
        }

        protected override void RemoveState()
        {
            GetLevelSettingPanelObj.SetActive(false);
            GetPopoverPanelObj.SetActive(false);
            GetInput.SetCanInput(true);
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
            UniTask.Void(UploadCoverImageAsync);
        }
        
        protected async UniTaskVoid UploadCoverImageAsync()
        {
            FileBrowser.SetFilters( false, new FileBrowser.Filter( "Image", ".jpg", ".png" ));
            await FileBrowser.WaitForLoadDialog( FileBrowser.PickMode.Files, false, null, null, "Choose level cover image", "Load" );
            if (FileBrowser.Success)
            {
                m_coverImagePath = FileBrowser.Result[0].Replace("\\","/");
                CheckImage();
            }
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
            try
            {
                await uwr.SendWebRequest();
            }
            catch (Exception e)
            {
                PopoverLauncher.Instance.LaunchTip(GetLevelSettingPanelObj.transform, GetPopoverProperty.POPOVER_LOCATION,
                    GetPopoverProperty.SIZE, GetPopoverProperty.POPOVER_ERROR_COLOR,
                    GetPopoverProperty.CANT_LOAD_IMAGE_ERROR, GetPopoverProperty.DURATION);
                return;
            }
            GetCoverImage.texture = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}
