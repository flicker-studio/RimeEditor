using Frame.StateMachine;

namespace LevelEditor
{
    public class ActionPanelShowState : AdditiveState
    {
        public ActionPanelShowState(Information baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
        }

        private ControlHandleAction GetControlHandleAction => m_information.UIManager.GetControlHandlePanel.GetControlHandleAction;

        public override void Motion(Information information)
        {
            // var panel          = m_information.UIManager.GetActionPanel;
            // var GetShiftButton = m_information.InputManager.GetShiftButton;
            // var GetPButtonDown = m_information.InputManager.GetPButtonDown;
            // var GetRButtonDown = m_information.InputManager.GetRButtonDown;
            // var GetSButtonDown = m_information.InputManager.GetSButtonDown;
            // */
            // if (GetPButtonDown)
            //     CommandInvoker.Execute(new Action(GetControlHandleAction, Controlhandleactiontype.PositionAxisButton));
            // else if (GetShiftButton && GetRButtonDown)
            //     CommandInvoker.Execute(new Action(GetControlHandleAction, Controlhandleactiontype.RectButton));
            // else if (GetRButtonDown)
            //     CommandInvoker.Execute(new Action(GetControlHandleAction, Controlhandleactiontype.RotationAxisButton));
            // else if ( )
            // {
            //     CommandInvoker.Execute(new ActionChangeCommand(GetControlHandleAction, CONTROLHANDLEACTIONTYPE.ViewButton));
            // }
            // else if (GetSButtonDown) CommandInvoker.Execute(new Action(GetControlHandleAction, Controlhandleactiontype.ScaleAxisButton));
            // else if (GetUndoButtonDown)
            // {
            //     CommandInvoker.Undo();
            // }
            // else if (GetRedoButtonDown)
            // {
            //     CommandInvoker.Redo();
            // }
        }
    }
}