using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Frame.CompnentExtensions;
using Frame.StateMachine;
using Frame.Tool.Popover;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelManagerPanelShowState : AdditiveState
    {
        private          InputManager              GetInput                 => m_information.InputManager;
        private          LevelDataManager          DataManager              => m_information.DataManager;
        private          GameObject                GetLevelDataButtonPrefab => m_information.PrefabManager.GetLevelItem;
        private          LevelManagerPanel         GetLevelManagerPanel     => m_information.UIManager.GetLevelManagerPanel;
        private          string                    GetLevelTextName         => GetLevelManagerPanel.GetLevelTextName;
        private          string                    GetLevelPathTextName     => GetLevelManagerPanel.GetLevelPathTextName;
        private          string                    GetLevelImageName        => GetLevelManagerPanel.GetLevelImageName;
        private          UISetting.PopoverProperty GetPopoverProperty       => GetLevelManagerPanel.GetPopoverProperty;
        private          RawImage                  GetLevelCoverImage       => GetLevelManagerPanel.GetLevelCoverImage;
        private          ScrollRect                GetScrollRect            => GetLevelManagerPanel.GetLevelScrollRect;
        private          Button                    GetOpenLocalFileButton   => GetLevelManagerPanel.GetOpenLocalDirectoryButton;
        private          RectTransform             GetLevelManagerRoot      => GetLevelManagerPanel.GetLevelManagerRootRect;
        private          RectTransform             GetLevelListContent      => GetLevelManagerPanel.GetLevelListContentRect;
        private          RectTransform             GetFullPanel             => GetLevelManagerPanel.GetFullPanelRect;
        private          Button                    GetOpenButton            => GetLevelManagerPanel.GetOpenButton;
        private          Button                    GetCreateButton          => GetLevelManagerPanel.GetCreateButton;
        private          Button                    GetDeclarationButton     => GetLevelManagerPanel.GetDeclarationButton;
        private          Button                    GetExitButton            => GetLevelManagerPanel.GetExitButton;
        private          Button                    GetRefreshButton         => GetLevelManagerPanel.GetRefreshButton;
        private          Button                    GetDeleteButton          => GetLevelManagerPanel.GetDeleteButton;
        private          TextMeshProUGUI           GetSubLevelNumber        => GetLevelManagerPanel.GetSubLevelNumber;
        private          TextMeshProUGUI           GetLevelName             => GetLevelManagerPanel.GetLevelName;
        private          TextMeshProUGUI           GetAnthorName            => GetLevelManagerPanel.GetAnthorName;
        private          TextMeshProUGUI           GetDateTime              => GetLevelManagerPanel.GetDateTime;
        private          TextMeshProUGUI           GetInstroduction         => GetLevelManagerPanel.GetInstroduction;
        private          TextMeshProUGUI           GetVersion               => GetLevelManagerPanel.GetVersion;
        private          LevelDataButton           _currentChooseLevelButton;
        private readonly List<LevelDataButton>     _levelDataButtons = new();

        public LevelManagerPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            GetInput.SetCanInput(false);
            GetFullPanel.gameObject.SetActive(true);
            GetLevelManagerRoot.gameObject.SetActive(true);
            ReloadLevels();

            GetCreateButton.onClick.AddListener(CreateLevel);
            GetOpenButton.onClick.AddListener(OpenLevel);
            GetRefreshButton.onClick.AddListener(ReloadLevels);
            GetDeleteButton.onClick.AddListener(DeleteLevelPopover);
            GetOpenLocalFileButton.onClick.AddListener(OpenLevelFile);
            GetExitButton.onClick.AddListener(ExitGamePopover);
        }

        private void RemoveEvent()
        {
            GetCreateButton.onClick.RemoveAllListeners();
            GetOpenButton.onClick.RemoveAllListeners();
            GetRefreshButton.onClick.RemoveAllListeners();
            GetDeleteButton.onClick.RemoveAllListeners();
            GetOpenLocalFileButton.onClick.RemoveAllListeners();
            GetExitButton.onClick.RemoveAllListeners();
        }

        protected override void RemoveState()
        {
            base.RemoveState();
            RemoveEvent();
            GetInput.SetCanInput(true);
            GetLevelManagerRoot.gameObject.SetActive(false);
            GetFullPanel.gameObject.SetActive(false);
        }

        private void CreateLevel()
        {
            DataManager.CreateLevel();
            JumpToEditorViewState();
        }

        private void OpenLevel()
        {
            if (_currentChooseLevelButton == null)
            {
                PopoverLauncher.Instance.LaunchTip
                    (
                     GetLevelManagerRoot, GetPopoverProperty.POPOVER_LOCATION,
                     GetPopoverProperty.SIZE, GetPopoverProperty.POPOVER_ERROR_COLOR,
                     GetPopoverProperty.POPOVER_TEXT_CHOOSE_LEVEL_ERROR, GetPopoverProperty.DURATION
                    );
                return;
            }

            DataManager.OpenLevel(_currentChooseLevelButton.GetLevelData);
            JumpToEditorViewState();
        }

        private void DeleteLevelPopover()
        {
            PopoverLauncher.Instance.LaunchSelector(GetLevelManagerRoot, GetPopoverProperty.POPOVER_TEXT_DELETE, DeleteLevel);
        }

        private void DeleteLevel()
        {
            if (DataManager.DeleteLevel(_currentChooseLevelButton.GetLevelData))
            {
                PopoverLauncher.Instance.LaunchTip
                    (
                     GetLevelManagerRoot,
                     GetPopoverProperty.POPOVER_LOCATION,
                     GetPopoverProperty.SIZE,
                     GetPopoverProperty.POPOVER_SUCCESS_COLOR,
                     GetPopoverProperty.POPOVER_DELETE_SUCCESS,
                     GetPopoverProperty.DURATION
                    );
            }

            ReloadLevels();
        }

        private void JumpToEditorViewState()
        {
            ChangeMotionState(typeof(EditorViewState));
            RemoveState();
        }

        public override void Motion(BaseInformation information)
        {
        }

        private async void ReloadLevels()
        {
            ClearLevelDataButtons();
            UpdateChooseLevelUI();
            await ReloadLevelsAsync();
        }

        private async UniTask ReloadLevelsAsync()
        {
            await DataManager.LoadLevelFiles();

            foreach (var levelData in DataManager.LevelDatas)
            {
                var buttons = new LevelDataButton
                    (
                     GetLevelDataButtonPrefab,
                     ChooseLevelDataButton,
                     GetLevelListContent,
                     GetScrollRect,
                     levelData,
                     GetLevelTextName,
                     GetLevelPathTextName,
                     GetLevelImageName
                    );

                _levelDataButtons.Add(buttons);
            }
        }

        #region View

        /// <summary>
        ///     在UI中选择对应的项目
        /// </summary>
        /// <param name="gridItemButton"></param>
        private void ChooseLevelDataButton(GridItemButton gridItemButton)
        {
            var itemProductButton = gridItemButton as LevelDataButton;
            _currentChooseLevelButton = itemProductButton;
            UpdateChooseLevelUI();
            GetCreateButton.interactable = true;

            foreach (var button in _levelDataButtons.Where(button => button != gridItemButton)) button.SetSelected(false);

            gridItemButton.SetSelected(true);
        }

        #endregion


        private void UpdateChooseLevelUI()
        {
            if (_currentChooseLevelButton == null)
            {
                GetLevelName.gameObject.SetActive(false);
                GetDateTime.gameObject.SetActive(false);
                GetAnthorName.gameObject.SetActive(false);
                GetInstroduction.gameObject.SetActive(false);
                GetVersion.gameObject.SetActive(false);
                GetLevelCoverImage.gameObject.SetActive(false);
                GetSubLevelNumber.gameObject.SetActive(false);
                GetDeleteButton.gameObject.SetActive(false);
                return;
            }

            GetLevelName.gameObject.SetActive(true);
            GetDateTime.gameObject.SetActive(true);
            GetAnthorName.gameObject.SetActive(true);
            GetInstroduction.gameObject.SetActive(true);
            GetVersion.gameObject.SetActive(true);
            GetLevelCoverImage.gameObject.SetActive(true);
            GetSubLevelNumber.gameObject.SetActive(true);
            GetDeleteButton.gameObject.SetActive(true);

            GetLevelName.text          = $"{_currentChooseLevelButton.GetLevelData.LevelName}";
            GetDateTime.text           = $"At {_currentChooseLevelButton.GetLevelData.CreateTime}";
            GetAnthorName.text         = $"By {_currentChooseLevelButton.GetLevelData.AuthorName}";
            GetInstroduction.text      = $"{_currentChooseLevelButton.GetLevelData.Introduction}";
            GetVersion.text            = $"{_currentChooseLevelButton.GetLevelData.Version}";
            GetSubLevelNumber.text     = $"{_currentChooseLevelButton.GetLevelData.SubLevelDatas.Count}";
            GetLevelCoverImage.texture = _currentChooseLevelButton.GetLevelData.Cover;
        }

        private void ExitGamePopover()
        {
            PopoverLauncher.Instance.LaunchSelector(GetLevelManagerRoot, GetPopoverProperty.POPOVER_TEXT_EXIT, ExitGame);
        }

        private void ExitGame()
        {
            Application.Quit();
        }

        private void ClearLevelDataButtons()
        {
            foreach (var levelDataButton in _levelDataButtons) levelDataButton.Remove();

            _levelDataButtons.Clear();
            _currentChooseLevelButton = null;
        }

        private void OpenLevelFile()
        {
            OpenLevelFileAsync().Forget();
        }

        private async UniTaskVoid OpenLevelFileAsync()
        {
            await FileBrowser.WaitForLoadDialog(FileBrowser.PickMode.Folders, false, null, null, "Open a level directory", "Load");

            if (FileBrowser.Success)
            {
                var path = FileBrowser.Result[0].Replace("\\", "/");

                if (!DataManager.OpenLocalLevelDirectory(path))
                {
                    PopoverLauncher.Instance.LaunchTip
                        (
                         GetLevelManagerRoot,
                         GetPopoverProperty.POPOVER_LOCATION,
                         GetPopoverProperty.SIZE,
                         GetPopoverProperty.POPOVER_ERROR_COLOR,
                         GetPopoverProperty.CHECK_ERROR_LEVEL_DIRECTORY,
                         GetPopoverProperty.DURATION
                        );

                    return;
                }

                ReloadLevels();
            }
        }
    }
}