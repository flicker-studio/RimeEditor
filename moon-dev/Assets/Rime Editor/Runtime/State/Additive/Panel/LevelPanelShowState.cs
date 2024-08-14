using System;
using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using LevelEditor.Manager;
using RimeEditor.Runtime;
using UnityEngine;

namespace LevelEditor
{
    public class LevelPanelShowState : AdditiveState
    {
        public LevelPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitEvents();
        }

        private LevelAction GetLevelAction => m_information.LevelAction;
        private LevelPanel  GetLevelPanel  => m_information.UIManager.GetLevelPanel;

        private BrowseController Get => m_information.Controller;

        private OutlineManager OutlineManager => m_information.OutlineManager;

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
            Get.SetActiveEditors(false);
            OutlineManager.SetRenderObjects(null);
            LevelPlay.Instance.Play(Get.ShowSubLevels(), Get.CurrentSubLevelIndex);
        }

        private void SaveLevel()
        {
            if (string.IsNullOrEmpty(Get.CurrentCustomLevel.Name))
            {
                LaunchPopover(GetLevelPanel.GetPopoverProperty.POPOVER_TEXT_LEVEL_NAME_MISSING,
                              GetLevelPanel.GetPopoverProperty.POPOVER_ERROR_COLOR);

                return;
            }

            LaunchPopover(GetLevelPanel.GetPopoverProperty.POPOVER_TEXT_SAVE_SUCCESS,
                          GetLevelPanel.GetPopoverProperty.POPOVER_SUCCESS_COLOR);

            Get.ToJson();
        }

        private void ToLevelSetting()
        {
            if (!CheckStates.Contains(typeof(LevelSettingPanelShowState))) ChangeMotionState(typeof(LevelSettingPanelShowState));
        }

        public override void Motion(Information information)
        {
        }

        private void LaunchPopover(string text, Color color)
        {
            throw new NotImplementedException();

            // PopoverLauncher.Instance
            //     .LaunchTip(GetLevelPanel.GetSaveButton.transform,
            //         GetLevelPanel.GetPopoverProperty.POPOVER_LOCATION,
            //         GetLevelPanel.GetPopoverProperty.SIZE,
            //         color,
            //         text,
            //         GetLevelPanel.GetPopoverProperty.DURATION);
        }

        private void ExitCurrentLevelPopover()
        {
            throw new NotImplementedException();
            // PopoverLauncher.Instance.LaunchSelector(GetLevelPanel.GetExitButton.transform,
            //     GetLevelPanel.GetPopoverProperty.POPOVER_TEXT_EXIT,
            //     ExitCurrentLevel);
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