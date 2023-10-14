public class LevelEditorActionChangeCommand : LevelEditorCommand
{
    private ControlHandleAction _mControlHandleAction;
    
    private CONTROLHANDLEACTIONTYPE m_lastActionType;

    private CONTROLHANDLEACTIONTYPE m_nextActionType;

    public LevelEditorActionChangeCommand(ControlHandleAction controlHandleAction,CONTROLHANDLEACTIONTYPE nextActionType)
    {
        _mControlHandleAction = controlHandleAction;
        m_lastActionType = _mControlHandleAction.ControlHandleActionType;
        m_nextActionType = nextActionType;
    }
    public override void Execute()
    {
        _mControlHandleAction.ControlHandleActionType = m_nextActionType;
    }

    public override void Undo()
    {
        _mControlHandleAction.ControlHandleActionType = m_lastActionType;
    }
}
