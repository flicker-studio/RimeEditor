using System;
using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using Moon.Kernel.Utils;
using Moon.Runtime;
using UnityEngine;

namespace LevelEditor
{
    public class LevelPanelShowState : AdditiveState
    {
        private LevelAction GetLevelAction => m_information.LevelAction;
        private LevelPanel GetLevelPanel => m_information.UIManager.GetLevelPanel;

        private LevelDataManager GetData => m_information.DataManager;

        private OutlineManager OutlineManager => m_information.OutlineManager;

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
            OutlineManager.SetRenderObjects(null);
            LevelPlay.Instance.Play(GetData.ShowSubLevels(), GetData.CurrentSubLevelIndex);
        }

        private void SaveLevel()
        {
            if (string.IsNullOrEmpty(GetData.CurrentLevel.LevelName))
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

        private void LaunchPopover(string text, Color color)
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
            //TODO:需加载SO
            throw new Exception("需加载SO");

            // GetData.SetActiveEditors(false);
            // await SceneLoader.Instance.RemoveTargetScene(GlobalSetting.Scenes.LEVEL_PLAY);
            // await SceneLoader.Instance.EnterScene(GlobalSetting.Scenes.LEVEL_EDITOR);
            // GetLevelAction.InvokeExitEditor();
        }
    }
}