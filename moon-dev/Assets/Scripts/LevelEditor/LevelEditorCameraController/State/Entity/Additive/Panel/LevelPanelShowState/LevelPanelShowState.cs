using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using Frame.Static.Global;
using Frame.Tool;
using Frame.Tool.Popover;
using UnityEngine;

namespace LevelEditor
{
    public class LevelPanelShowState : AdditiveState
    {
        private LevelAction GetLevelAction => m_information.GetLevelAction;
        private LevelPanel GetLevelPanel => m_information.GetUI.GetLevelPanel;

        private DataManager GetData => m_information.GetData;

        private CameraManager GetCamera => m_information.GetCamera;
        
        public LevelPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitEvents();
        }

        private void InitEvents()
        {
            GetLevelPanel.GetPlayButton.onClick.AddListener(PlayLevel);
            GetLevelPanel.GetSaveButton.onClick.AddListener(SaveLevel);
            GetLevelPanel.GetSettingButton.onClick.AddListener(ToLevelSetting);
            GetLevelPanel.GetExitButton.onClick.AddListener(ExitCurrentLevelPopover);
        }

        protected override void RemoveState()
        {
            GetLevelPanel.GetPlayButton.onClick.RemoveAllListeners();
            GetLevelPanel.GetSaveButton.onClick.RemoveAllListeners();
            GetLevelPanel.GetSettingButton.onClick.RemoveAllListeners();
            GetLevelPanel.GetExitButton.onClick.RemoveAllListeners();
            base.RemoveState();
        }

        private void PlayLevel()
        {
            GetData.SetActiveEditors(false);
            GetCamera.SetTargetObject = null;
            LevelPlay.Instance.Play(GetData.ShowSubLevels(),GetData.GetCurrentSubLevelIndex);
        }

        private void SaveLevel()
        {
            if (GetData.GetCurrentLevel.GetName == "" || GetData.GetCurrentLevel.GetName == null)
            {
                LaunchPopover(GetLevelPanel.GetPopoverProperty.POPOVER_TEXT_LEVEL_NAME_MISSING,
                    GetLevelPanel.GetPopoverProperty.POPOVER_ERROR_COLOR);
                return;
            }
            LaunchPopover(GetLevelPanel.GetPopoverProperty.POPOVER_TEXT_SAVE_SUCCESS,
                GetLevelPanel.GetPopoverProperty.POPOVER_SUCCESS_COLOR);
            GetData.ToJson();
        }

        private void ToLevelSetting()
        {
            if (!CheckStates.Contains(typeof(LevelSettingPanelShowState)))
            {
                ChangeMotionState(typeof(LevelSettingPanelShowState));
            }
        }
        
        public override void Motion(BaseInformation information)
        {
            
        }

        private void LaunchPopover(string text,Color color)
        {
            PopoverLauncher.Instance
                .LaunchTip(GetLevelPanel.GetSaveButton.transform,
                    GetLevelPanel.GetPopoverProperty.POPOVER_LOCATION,
                    GetLevelPanel.GetPopoverProperty.SIZE,
                    color,
                    text,
                    GetLevelPanel.GetPopoverProperty.DURATION);
        }

        private void ExitCurrentLevelPopover()
        {
            PopoverLauncher.Instance.LaunchSelector(GetLevelPanel.GetExitButton.transform,
                GetLevelPanel.GetPopoverProperty.POPOVER_TEXT_EXIT,
                ExitCurrentLevel);
        }

        private void ExitCurrentLevel()
        {
            UniTask.Void(ExitCurrentLevelAsync);
        }
        
        private async UniTaskVoid ExitCurrentLevelAsync()
        {
            GetData.SetActiveEditors(false);
            await SceneLoader.Instance.RemoveTargetScene(GlobalSetting.Scenes.LEVEL_PLAY);
            await SceneLoader.Instance.EnterScene(GlobalSetting.Scenes.LEVEL_EDITOR);
            GetLevelAction.InvokeExitEditor();
        }
    }
}
