using Frame.StateMachine;

namespace LevelEditor
{
    public class LevelPanelShowState : AdditiveState
    {
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
        }

        private void PlayLevel()
        {
            GetData.SetActiveEditors(false);
            GetCamera.GetOutlinePainter.SetTargetObj = null;
            LevelPlay.Instance.Play(GetData.ShowLevels(),GetData.GetCurrentIndex);
        }
        
        public override void Motion(BaseInformation information)
        {
            
        }
    }
}
