using Frame.StateMachine;

public class LevelEditorCameraDefultState : LevelEditorAdditiveState
{
    private bool GetMouseMiddleButtonDown => m_information.GetInput.GetMouseMiddleButtonDown;

    private bool GetMouseSrollDown => m_information.GetInput.GetMouseSrollDown;
    public LevelEditorCameraDefultState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        CheckCameraInput();
    }
    
    private void CheckCameraInput()
    {
        if (GetMouseMiddleButtonDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorMoveState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraMoveState);
            }
        }

        if (GetMouseSrollDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorChangeZState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraChangeZState);
            }
        }
    }
}
