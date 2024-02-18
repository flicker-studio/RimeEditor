using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using Frame.CompnentExtensions;
using Frame.StateMachine;
using Frame.Tool.Popover;
using LevelEditor.View;
using SimpleFileBrowser;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor.State
{
    /// <summary>
    ///     The level browsing state of the state machine
    /// </summary>
    public sealed class BrowseState : AdditiveState, IState
    {
        private InputManager     GetInput                 => m_information.InputManager;
        private LevelDataManager DataManager              => m_information.DataManager;
        private GameObject       GetLevelDataButtonPrefab => m_information.PrefabManager.GetLevelItem;

        private string                    GetLevelTextName       => GetBrowseCanvas.GetLevelTextName;
        private string                    GetLevelPathTextName   => GetBrowseCanvas.GetLevelPathTextName;
        private string                    GetLevelImageName      => GetBrowseCanvas.GetLevelImageName;
        private UISetting.PopoverProperty GetPopoverProperty     => GetBrowseCanvas.GetPopoverProperty;
        private RawImage                  GetLevelCoverImage     => GetBrowseCanvas.GetLevelCoverImage;
        private ScrollRect                GetScrollRect          => GetBrowseCanvas.GetLevelScrollRect;
        private Button                    GetOpenLocalFileButton => GetBrowseCanvas.GetImportButton;
        private RectTransform             GetLevelManagerRoot    => GetBrowseCanvas.GetLevelManagerRootRect;
        private RectTransform             GetLevelListContent    => GetBrowseCanvas.GetLevelListContentRect;
        private RectTransform             GetFullPanel           => GetBrowseCanvas.GetFullPanelRect;

        private Button GetOpenButton        => GetBrowseCanvas.GetOpenButton;
        private Button GetCreateButton      => GetBrowseCanvas.GetCreateButton;
        private Button GetDeclarationButton => GetBrowseCanvas.GetDeclarationButton;
        private Button GetExitButton        => GetBrowseCanvas.GetExitButton;
        private Button GetRefreshButton     => GetBrowseCanvas.GetRefreshButton;
        private Button GetDeleteButton      => GetBrowseCanvas.GetDeleteButton;

        private          TextMeshProUGUI       GetSubLevelNumber => GetBrowseCanvas.GetSubLevelNumber;
        private          TextMeshProUGUI       GetLevelName      => GetBrowseCanvas.GetLevelName;
        private          TextMeshProUGUI       GetAnthorName     => GetBrowseCanvas.GetAnthorName;
        private          TextMeshProUGUI       GetDateTime       => GetBrowseCanvas.GetDateTime;
        private          TextMeshProUGUI       GetInstroduction  => GetBrowseCanvas.GetInstroduction;
        private          TextMeshProUGUI       GetVersion        => GetBrowseCanvas.GetVersion;
        private          LevelDataButton       _currentChooseLevelButton;
        private readonly List<LevelDataButton> _levelDataButtons = new();

        private BrowseCanvas GetBrowseCanvas => m_information.UIManager.GetBrowseCanvas;
        private BrowseCanvas _panel;

        public BrowseState(BaseInformation baseInformation, MotionCallBack motionCallBack)
            : base(baseInformation, motionCallBack)
        {
            _panel = new BrowseCanvas();
            // GetInput.SetCanInput(false);
            // GetFullPanel.gameObject.SetActive(true);
            // GetLevelManagerRoot.gameObject.SetActive(true);
            // ReloadLevels();

            // GetCreateButton.onClick.AddListener(CreateLevel);
            // GetOpenButton.onClick.AddListener(OpenLevel);
            // GetRefreshButton.onClick.AddListener(ReloadLevels);
            // GetDeleteButton.onClick.AddListener(DeleteLevelPopover);
            // GetOpenLocalFileButton.onClick.AddListener(OpenLevelFile);
            // GetExitButton.onClick.AddListener(ExitGamePopover);
        }

        /// <inheritdoc />
        public void OnEnter()
        {
            _panel.Active();
            GetInput.SetCanInput(false);
            GetFullPanel.gameObject.SetActive(true);
            GetLevelManagerRoot.gameObject.SetActive(true);
            ReloadLevels();
        }

        /// <inheritdoc />
        public void OnExit()
        {
            _panel.Inactive();
        }

        private void RemoveEvent()
        {
            //  GetCreateButton.onClick.RemoveAllListeners();
            // GetOpenButton.onClick.RemoveAllListeners();
            // GetRefreshButton.onClick.RemoveAllListeners();
            // GetDeleteButton.onClick.RemoveAllListeners();
            // GetOpenLocalFileButton.onClick.RemoveAllListeners();
            // GetExitButton.onClick.RemoveAllListeners();
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
                PopoverLauncher.Instance.LaunchTip
                    (
                     GetLevelManagerRoot,
                     GetPopoverProperty.POPOVER_LOCATION,
                     GetPopoverProperty.SIZE,
                     GetPopoverProperty.POPOVER_SUCCESS_COLOR,
                     GetPopoverProperty.POPOVER_DELETE_SUCCESS,
                     GetPopoverProperty.DURATION
                    );

            ReloadLevels();
        }

        private void JumpToEditorViewState()
        {
            ChangeMotionState(typeof(EditorState));
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

        /// <summary>
        ///     Select the corresponding item in the UI
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