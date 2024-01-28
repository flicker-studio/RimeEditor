namespace LevelEditor
{
    public class ActionChangeCommand : ICommand
    {
        private ControlHandleAction m_controlHandleAction;

        private CONTROLHANDLEACTIONTYPE m_lastActionType;

        private CONTROLHANDLEACTIONTYPE m_nextActionType;

        private bool m_lastGrid;

        private bool m_nextGrid;

        public ActionChangeCommand(ControlHandleAction controlHandleAction, CONTROLHANDLEACTIONTYPE nextActionType)
        {
            m_controlHandleAction = controlHandleAction;
            m_lastActionType = m_controlHandleAction.ControlHandleActionType;
            m_nextActionType = nextActionType;
            m_lastGrid = m_controlHandleAction.UseGrid;
            m_nextGrid = m_controlHandleAction.UseGrid;
        }

        public ActionChangeCommand(ControlHandleAction controlHandleAction, bool nextUseGrid)
        {
            m_controlHandleAction = controlHandleAction;
            m_lastGrid = m_controlHandleAction.UseGrid;
            m_nextGrid = nextUseGrid;
            m_lastActionType = m_controlHandleAction.ControlHandleActionType;
            m_nextActionType = m_controlHandleAction.ControlHandleActionType;
        }

        public   void Execute()
        {
            m_controlHandleAction.ControlHandleActionType = m_nextActionType;
            m_controlHandleAction.UseGrid = m_nextGrid;
        }

        public   void Undo()
        {
            m_controlHandleAction.ControlHandleActionType = m_lastActionType;
            m_controlHandleAction.UseGrid = m_lastGrid;
        }
    }
}