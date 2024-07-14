using System;
using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelSettingPanelShowState : AdditiveState
    {
        private string m_coverImagePath;

        public LevelSettingPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
        }

        private InputManager      GetInput             => m_information.InputManager;
        private LevelDataManager  GetData              => m_information.DataManager;
        private LevelData         GetCurrentLevel      => GetData.CurrentLevel;
        private LevelSettingPanel GetLevelSettingPanel => m_information.UIManager.GetLevelSettingPanel;

        private UISetting.PopoverProperty GetPopoverProperty => GetLevelSettingPanel.GetPopoverProperty;

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

        private void InitState()
        {
            GetPopoverPanelObj.SetActive(true);
            GetLevelSettingPanelObj.SetActive(true);
            throw new InvalidOperationException(); // GetInput.SetCanInput(false);
            GetCloseButton.onClick.AddListener(RemoveState);
            GetSaveButton.onClick.AddListener(SaveLevel);
            GetCoverImageButton.onClick.AddListener(UploadCoverImage);
            InitInputField();
        }

        protected override void RemoveState()
        {
            GetLevelSettingPanelObj.SetActive(false);
            GetPopoverPanelObj.SetActive(false);
            throw new InvalidOperationException(); // GetInput.SetCanInput(true);
            GetCloseButton.onClick.RemoveAllListeners();
            GetSaveButton.onClick.RemoveAllListeners();
            GetCoverImageButton.onClick.RemoveAllListeners();
            base.RemoveState();
        }

        private void SaveLevel()
        {
            GetData.Save
                (
                 GetLevelNameInputField.text,
                 GetAuthorNameInputField.text,
                 GetIntroductionInputField.text,
                 GetVersionInputField.text,
                 GetCoverImage.texture as Texture2D
                );

            RemoveState();
        }

        protected void UploadCoverImage()
        {
            UniTask.Void(UploadCoverImageAsync);
        }

        protected async UniTaskVoid UploadCoverImageAsync()
        {
            FileBrowser.SetFilters(false, new FileBrowser.Filter("Image", ".jpg", ".png"));
            await FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Files, false, null, null, "Choose level cover image", "Load");

            if (FileBrowser.Success)
            {
                m_coverImagePath = FileBrowser.Result[0].Replace("\\", "/");
                CheckImage();
            }
        }

        public override void Motion(Information information)
        {
        }

        private void InitInputField()
        {
            GetLevelNameInputField.text    = GetCurrentLevel.LevelName;
            GetAuthorNameInputField.text   = GetCurrentLevel.AuthorName;
            GetIntroductionInputField.text = GetCurrentLevel.Introduction;
            GetVersionInputField.text      = GetCurrentLevel.Version;
            GetCoverImage.texture          = GetCurrentLevel.Cover;
        }

        private void CheckImage()
        {
            if (!string.IsNullOrEmpty(m_coverImagePath)) UniTask.Void(UpdateImage);
        }

        private async UniTaskVoid UpdateImage()
        {
            var uwr = UnityWebRequestTexture.GetTexture("file:///" + m_coverImagePath);

            try
            {
                await uwr.SendWebRequest();
            }
            catch (Exception e)
            {
                throw new NotImplementedException();

                // PopoverLauncher.Instance.LaunchTip(GetLevelSettingPanelObj.transform, GetPopoverProperty.POPOVER_LOCATION,
                //     GetPopoverProperty.SIZE, GetPopoverProperty.POPOVER_ERROR_COLOR,
                //     GetPopoverProperty.CANT_LOAD_IMAGE_ERROR, GetPopoverProperty.DURATION);

                return;
            }

            GetCoverImage.texture = DownloadHandlerTexture.GetContent(uwr);
        }
    }
}