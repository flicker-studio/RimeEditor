using Frame.StateMachine;

namespace LevelEditor
{
    public class ActionPanelShowState : AdditiveState
    {
        private CommandExcute GetExcute => m_information.GetCommandSet.GetExcute;

        private UndoExcute GetUndo => m_information.GetCommandSet.GetUndo;
        
        private RedoExcute GetRedo => m_information.GetCommandSet.GetRedo;

        private ControlHandleAction GetControlHandleAction => m_information.GetUI.GetControlHandlePanel.GetControlHandleAction;

        private bool GetUndoButtonDown => m_information.GetUI.GetActionPanel.GetUndoInputDown;
    
        private bool GetRedoButtonDown => m_information.GetUI.GetActionPanel.GetRedoInputDown;

        private bool GetViewButtonDown => m_information.GetUI.GetActionPanel.GetViewInputDown;

        private bool GetPositionButtonDown => m_information.GetUI.GetActionPanel.GetPositionInputDown;

        private bool GetRotationButtonDown => m_information.GetUI.GetActionPanel.GetRotationInputDown;

        private bool GetScaleButtonDown => m_information.GetUI.GetActionPanel.GetScaleInputDown;

        private bool GetRectButtonDown => m_information.GetUI.GetActionPanel.GetRectInputDown;

        private bool GetShiftButton => m_information.GetInput.GetShiftButton;
        public bool GetPButtonDown => m_information.GetInput.GetPButtonDown;
        
        public bool GetRButtonDown => m_information.GetInput.GetRButtonDown;
        
        public bool GetSButtonDown => m_information.GetInput.GetSButtonDown;
    
        public ActionPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
        
        }

        public override void Motion(BaseInformation information)
        {
            if (GetPositionButtonDown || GetPButtonDown)
            {
                GetExcute?.Invoke(new ActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.PositionAxisButton));
            }
            else if (GetRectButtonDown || GetShiftButton && GetRButtonDown)
            {
                GetExcute?.Invoke(new ActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.RectButton));
            }
            else if (GetRotationButtonDown || GetRButtonDown)
            {
                GetExcute?.Invoke(new ActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.RotationAxisButton));
            }
            else if (GetViewButtonDown)
            {
                GetExcute?.Invoke(new ActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.ViewButton));
            }
            else if (GetScaleButtonDown || GetSButtonDown)
            {
                GetExcute?.Invoke(new ActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.ScaleAxisButton));
            }
            else if (GetUndoButtonDown)
            {
                GetUndo?.Invoke();
            }
            else if (GetRedoButtonDown)
            {
                GetRedo?.Invoke();
            }
        }
    
    }
}
