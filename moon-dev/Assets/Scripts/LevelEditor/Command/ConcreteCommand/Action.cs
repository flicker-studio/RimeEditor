namespace LevelEditor.Command
{
    /// <summary>
    ///     显示不同的
    /// </summary>
    internal class Action : ICommand
    {
        private readonly ControlHandleAction m_controlHandleAction;
        
        private readonly Controlhandleactiontype m_lastActionType;
        
        private readonly Controlhandleactiontype m_nextActionType;

        private readonly bool m_lastGrid;

        private readonly bool m_nextGrid;
        
        public Action(ControlHandleAction controlHandleAction, Controlhandleactiontype nextActionType)
        {
            m_controlHandleAction = controlHandleAction;
            m_lastActionType      = m_controlHandleAction.ControlHandleActionType;
            m_nextActionType      = nextActionType;
            m_lastGrid            = m_controlHandleAction.UseGrid;
            m_nextGrid            = m_controlHandleAction.UseGrid;
        }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Action(ControlHandleAction controlHandleAction, bool nextUseGrid)
        {
            m_controlHandleAction = controlHandleAction;
            m_lastGrid            = m_controlHandleAction.UseGrid;
            m_nextGrid            = nextUseGrid;
            m_lastActionType      = m_controlHandleAction.ControlHandleActionType;
            m_nextActionType      = m_controlHandleAction.ControlHandleActionType;
        }

        /// <inheritdoc />
        public void Execute()
        {
            m_controlHandleAction.ControlHandleActionType = m_nextActionType;
            m_controlHandleAction.UseGrid                 = m_nextGrid;
        }

        /// <inheritdoc />
        public void Undo()
        {
            m_controlHandleAction.ControlHandleActionType = m_lastActionType;
            m_controlHandleAction.UseGrid                 = m_lastGrid;
        }
    }
}