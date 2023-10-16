using Frame.StateMachine;

public class ActionPanelShowState : LevelEditorAdditiveState
{
    private LevelEditorCommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

    private ControlHandleAction GetControlHandleAction => m_information.GetUI.GetControlHandlePanel.GetControlHandleAction;

    private bool GetUndoButtonDown => m_information.GetUI.GetActionPanel.GetUndoInputDown;
    
    private bool GetRedoButtonDown => m_information.GetUI.GetActionPanel.GetRedoInputDown;

    private bool GetViewButtonDown => m_information.GetUI.GetActionPanel.GetViewInputDown;

    private bool GetPositionButtonDown => m_information.GetUI.GetActionPanel.GetPositionInputDown;

    private bool GetRotationButtonDown => m_information.GetUI.GetActionPanel.GetRotationInputDown;

    private bool GetScaleButtonDown => m_information.GetUI.GetActionPanel.GetScaleInputDown;
    
    private bool GetScaleChange => m_information.GetUI.GetItemTransformPanel.GetScaleChange;
    
    public ActionPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
        
    }

    public override void Motion(BaseInformation information)
    {
        if (GetPositionButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.PositionAxisButton));
        }else if (GetRotationButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.RotationAxisButton));
        }else if (GetViewButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.ViewButton));
        }
    }
    
}
