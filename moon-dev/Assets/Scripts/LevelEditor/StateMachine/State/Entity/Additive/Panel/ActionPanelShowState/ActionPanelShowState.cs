using Frame.StateMachine;
using LevelEditor.Command;

namespace LevelEditor
{
    public class ActionPanelShowState : AdditiveState
    {
        private ControlHandleAction GetControlHandleAction => m_information.UIManager.GetControlHandlePanel.GetControlHandleAction;


        public ActionPanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack)
            : base(baseInformation, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            var panel = m_information.UIManager.GetActionPanel;

            var GetUndoButtonDown = panel.GetUndoInputDown;

            var GetRedoButtonDown = panel.GetRedoInputDown;

            var GetViewButtonDown = panel.GetViewInputDown;

            var GetPositionButtonDown = panel.GetPositionInputDown;

            var GetRotationButtonDown = panel.GetRotationInputDown;

            var GetScaleButtonDown = panel.GetScaleInputDown;

            var GetRectButtonDown = panel.GetRectInputDown;

            var GetShiftButton = m_information.InputManager.GetShiftButton;

            var GetPButtonDown = m_information.InputManager.GetPButtonDown;

            var GetRButtonDown = m_information.InputManager.GetRButtonDown;

            var GetSButtonDown = m_information.InputManager.GetSButtonDown;

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