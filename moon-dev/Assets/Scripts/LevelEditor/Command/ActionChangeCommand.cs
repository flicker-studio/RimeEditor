namespace LevelEditor
{
    public class ActionChangeCommand : Command
    {
        private ControlHandleAction m_controlHandleAction;
    
        private CONTROLHANDLEACTIONTYPE m_lastActionType;

        private CONTROLHANDLEACTIONTYPE m_nextActionType;

        public ActionChangeCommand(ControlHandleAction controlHandleAction,CONTROLHANDLEACTIONTYPE nextActionType)
        {
            m_controlHandleAction = controlHandleAction;
            m_lastActionType = m_controlHandleAction.ControlHandleActionType;
            m_nextActionType = nextActionType;
        }
        public override void Execute()
        {
            m_controlHandleAction.ControlHandleActionType = m_nextActionType;
        }

        public override void Undo()
        {
            m_controlHandleAction.ControlHandleActionType = m_lastActionType;
        }
    }

}