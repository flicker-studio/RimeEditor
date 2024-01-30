using Frame.StateMachine;
using LevelEditor.Command;

namespace LevelEditor
{
    public class ActionPanelShowState : AdditiveState
    {
        private ControlHandleAction GetControlHandleAction => m_information.UIManager.GetControlHandlePanel.GetControlHandleAction;

        private bool GetUndoButtonDown => m_information.UIManager.GetActionPanel.GetUndoInputDown;

        private bool GetRedoButtonDown => m_information.UIManager.GetActionPanel.GetRedoInputDown;

        private bool GetViewButtonDown => m_information.UIManager.GetActionPanel.GetViewInputDown;

        private bool GetPositionButtonDown => m_information.UIManager.GetActionPanel.GetPositionInputDown;

        private bool GetRotationButtonDown => m_information.UIManager.GetActionPanel.GetRotationInputDown;

        private bool GetScaleButtonDown => m_information.UIManager.GetActionPanel.GetScaleInputDown;

        private bool GetRectButtonDown => m_information.UIManager.GetActionPanel.GetRectInputDown;

        private bool GetShiftButton => m_information.InputManager.GetShiftButton;
        public bool GetPButtonDown => m_information.InputManager.GetPButtonDown;

        public bool GetRButtonDown => m_information.InputManager.GetRButtonDown;

        public bool GetSButtonDown => m_information.InputManager.GetSButtonDown;

        public ActionPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            if (GetPositionButtonDown || GetPButtonDown)
            {
                CommandInvoker.Execute(new ActionChangeCommand(GetControlHandleAction, CONTROLHANDLEACTIONTYPE.PositionAxisButton));
            }
            else if (GetRectButtonDown || (GetShiftButton && GetRButtonDown))
            {
                CommandInvoker.Execute(new ActionChangeCommand(GetControlHandleAction, CONTROLHANDLEACTIONTYPE.RectButton));
            }
            else if (GetRotationButtonDown || GetRButtonDown)
            {
                CommandInvoker.Execute(new ActionChangeCommand(GetControlHandleAction, CONTROLHANDLEACTIONTYPE.RotationAxisButton));
            }
            else if (GetViewButtonDown)
            {
                CommandInvoker.Execute(new ActionChangeCommand(GetControlHandleAction, CONTROLHANDLEACTIONTYPE.ViewButton));
            }
            else if (GetScaleButtonDown || GetSButtonDown)
            {
                CommandInvoker.Execute(new ActionChangeCommand(GetControlHandleAction, CONTROLHANDLEACTIONTYPE.ScaleAxisButton));
            }
            else if (GetUndoButtonDown)
            {
                CommandInvoker.Undo();
            }
            else if (GetRedoButtonDown)
            {
                CommandInvoker.Redo();
            }
        }
    }
}