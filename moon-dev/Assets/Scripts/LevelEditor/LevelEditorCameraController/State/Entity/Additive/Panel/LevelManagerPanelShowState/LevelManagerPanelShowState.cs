using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Frame.CompnentExtensions;
using Frame.StateMachine;
using Frame.Tool.Popover;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelManagerPanelShowState : AdditiveState
    {
        private InputController GetInput => m_information.GetInput;
        private DataManager GetData => m_information.GetData;

        private GameObject GetLevelDataButtonPrefab => m_information.GetPrefab.GetLevelItem;
        private LevelManagerPanel GetLevelManagerPanel => m_information.GetUI.GetLevelManagerPanel;
        
        public string GetLevelTextName => GetLevelManagerPanel.GetLevelTextName;

        public string GetLevelPathTextName => GetLevelManagerPanel.GetLevelPathTextName;

        public string GetLevelImageName => GetLevelManagerPanel.GetLevelImageName;

        private UIProperty.PopoverProperty GetPopoverProperty => GetLevelManagerPanel.GetPopoverProperty;
        
        public RawImage GetLevelCoverImage => GetLevelManagerPanel.GetLevelCoverImage;

        private ScrollRect GetScrollRect => GetLevelManagerPanel.GetLevelScrollRect;

        private Button GetOpenLocalFileButton => GetLevelManagerPanel.GetOpenLocalDirectoryButton;
        
        private RectTransform GetLevelManagerRoot => GetLevelManagerPanel.GetLevelManagerRootRect;
        
        private RectTransform GetLevelListContent => GetLevelManagerPanel.GetLevelListContentRect;
        
        private RectTransform GetFullPanel => GetLevelManagerPanel.GetFullPanelRect;

        private Button GetOpenButton => GetLevelManagerPanel.GetOpenButton;

        private Button GetCreateButton => GetLevelManagerPanel.GetCreateButton;

        private Button GetDeclarationButton => GetLevelManagerPanel.GetDeclarationButton;

        private Button GetExitButton => GetLevelManagerPanel.GetExitButton;

        private Button GetRefreshButton => GetLevelManagerPanel.GetRefreshButton;

        private Button GetDeleteButton => GetLevelManagerPanel.GetDeleteButton;
        
        public TextMeshProUGUI GetSubLevelNumber => GetLevelManagerPanel.GetSubLevelNumber;

        private TextMeshProUGUI GetLevelName => GetLevelManagerPanel.GetLevelName;

        private TextMeshProUGUI GetAnthorName => GetLevelManagerPanel.GetAnthorName;

        private TextMeshProUGUI GetDateTime => GetLevelManagerPanel.GetDateTime;

        private TextMeshProUGUI GetInstroduction => GetLevelManagerPanel.GetInstroduction;
        private TextMeshProUGUI GetVersion => GetLevelManagerPanel.GetVersion;

        private LevelDataButton m_currentChooseLevelButton;

        private List<LevelDataButton> m_levelDataButtons = new List<LevelDataButton>();
        
        public LevelManagerPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitState();
            InitEvent();
        }

        private void InitState()
        {
            GetInput.SetCanInput(false);
            GetFullPanel.gameObject.SetActive(true);
            GetLevelManagerRoot.gameObject.SetActive(true);
            ReloadLevels();
        }
        
        private void InitEvent()
        {
            GetCreateButton.onClick.AddListener(CreateLevel);
            GetOpenButton.onClick.AddListener(OpenLevel);
            GetRefreshButton.onClick.AddListener(ReloadLevels);
            GetDeleteButton.onClick.AddListener(DeleteLevel);
            GetOpenLocalFileButton.onClick.AddListener(OpenLevelFile);
        }

        private void RemoveEvent()
        {
            GetCreateButton.onClick.RemoveAllListeners();
            GetOpenButton.onClick.RemoveAllListeners();
            GetRefreshButton.onClick.RemoveAllListeners();
            GetDeleteButton.onClick.RemoveAllListeners();
            GetOpenLocalFileButton.onClick.RemoveAllListeners();
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
            GetData.CreateLevel();
            JumpToEditorViewState();
        }
        
        private void OpenLevel()
        {
            if (m_currentChooseLevelButton == null)
            {
                PopoverLauncher.Instance.Launch(GetLevelManagerRoot, GetPopoverProperty.POPOVER_LOCATION,
                    GetPopoverProperty.SIZE, GetPopoverProperty.POPOVER_ERROR_COLOR,
                    GetPopoverProperty.POPOVER_TEXT_CHOOSE_LEVEL_ERROR, GetPopoverProperty.DURATION);
                return;
            }
            GetData.OpenLevel(m_currentChooseLevelButton.GetLevelData);
            JumpToEditorViewState();
        }

        private void DeleteLevel()
        {
            if (GetData.DeleteLevel(m_currentChooseLevelButton.GetLevelData))
            {
                PopoverLauncher.Instance.Launch(GetLevelManagerRoot, GetPopoverProperty.POPOVER_LOCATION,
                    GetPopoverProperty.SIZE, GetPopoverProperty.POPOVER_SUCCESS_COLOR,
                    GetPopoverProperty.POPOVER_DELETE_SUCCESS, GetPopoverProperty.DURATION);
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

        private void ReloadLevels()
        {
            ClearLevelDataButtons();
            UpdateChooseLevelUI();
            UniTask.Void(ReloadLevelsAsync);
        }
        
        private async UniTaskVoid ReloadLevelsAsync()
        {
            await GetData.LoadLevelFiles();
            List<LevelData> levelDatas = GetData.GetAllLevels;
            foreach (var levelData in levelDatas)
            {
                m_levelDataButtons.Add(
                    new LevelDataButton(GetLevelDataButtonPrefab,ChooseLevelDataButton,
                        GetLevelListContent,GetScrollRect,levelData,GetLevelTextName,GetLevelPathTextName,GetLevelImageName));
            }
        }
        
        private void ChooseLevelDataButton(GridItemButton gridItemButton)
        {
            LevelDataButton itemProductButton = gridItemButton as LevelDataButton;
            m_currentChooseLevelButton = itemProductButton;
            UpdateChooseLevelUI();
            GetCreateButton.interactable = true;
            foreach (var m_levelDataButton in m_levelDataButtons)
            {
                if (m_levelDataButton != gridItemButton)
                {
                    m_levelDataButton.SetSelected = false;
                }
            }
            gridItemButton.SetSelected = true;
        }

        private void UpdateChooseLevelUI()
        {
            if (m_currentChooseLevelButton == null)
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
            GetLevelName.text = $"{m_currentChooseLevelButton.GetLevelData.GetName}";
            GetDateTime.text = $"At {m_currentChooseLevelButton.GetLevelData.GetTime}";
            GetAnthorName.text = $"By {m_currentChooseLevelButton.GetLevelData.GetAuthorName}";
            GetInstroduction.text = $"{m_currentChooseLevelButton.GetLevelData.GetIntroduction}";
            GetVersion.text = $"{m_currentChooseLevelButton.GetLevelData.GetVersion}";
            GetSubLevelNumber.text = $"{m_currentChooseLevelButton.GetLevelData.GetSubLevelDatas.Count}";
            GetLevelCoverImage.texture = m_currentChooseLevelButton.GetLevelData.GetLevelCoverImage;
        }

        private void ClearLevelDataButtons()
        {
            foreach (var levelDataButton in m_levelDataButtons)
            {
                levelDataButton.Remove();
            }
            m_levelDataButtons.Clear();
            m_currentChooseLevelButton = null;
        }

        private void OpenLevelFile()
        {
            string path = EditorUtility.OpenFolderPanel("Open a level directory", "", "");
            if (!GetData.OpenLocalLevelDirectory(path))
            {
                PopoverLauncher.Instance.Launch(GetLevelManagerRoot, GetPopoverProperty.POPOVER_LOCATION,
                    GetPopoverProperty.SIZE, GetPopoverProperty.POPOVER_ERROR_COLOR,
                    GetPopoverProperty.CHECK_ERROR_LEVEL_DIRECTORY, GetPopoverProperty.DURATION);
                return;
            }
            ReloadLevels();
        }
    }
}