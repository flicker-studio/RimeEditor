using Frame.Tool;

namespace LevelEditor
{
    public class InputManager : IManager
    {
        public bool GetCanInput => Frame.Tool.InputManager.Instance.CanInput;

        public void SetCanInput(bool canInput)
        {
            Frame.Tool.InputManager.Instance.CanInput = canInput;
        }

        public bool GetMouseLeftButton => Frame.Tool.InputManager.Instance.GetMouseLeftButton;

        public bool GetMouseLeftButtonDown => Frame.Tool.InputManager.Instance.GetMouseLeftButtonDown;

        public bool GetMouseLeftButtonUp => Frame.Tool.InputManager.Instance.GetMouseLeftButtonUp;

        public bool GetMouseRightButton => Frame.Tool.InputManager.Instance.GetMouseRightButton;

        public bool GetMouseRightButtonDown => Frame.Tool.InputManager.Instance.GetMouseRightButtonDown;

        public bool GetMouseRightButtonUp => Frame.Tool.InputManager.Instance.GetMouseRightButtonUp;

        public bool GetMouseMiddleButton => Frame.Tool.InputManager.Instance.GetMouseMiddleButton;

        public float GetMouseSroll => Frame.Tool.InputManager.Instance.GetMouseScroll;

        public bool GetMouseSrollDown => Frame.Tool.InputManager.Instance.GetMouseScrollDown;

        public bool GetMouseSrollUp => Frame.Tool.InputManager.Instance.GetMouseScrollUp;

        public bool GetMouseMiddleButtonDown => Frame.Tool.InputManager.Instance.GetMouseMiddleButtonDown;

        public bool GetMouseMiddleButtonUp => Frame.Tool.InputManager.Instance.GetMouseMiddleButtonUp;

        public bool GetShiftButton => Frame.Tool.InputManager.Instance.GetShiftButton;

        public bool GetCtrlButton => Frame.Tool.InputManager.Instance.GetCtrlButton;

        public bool GetDeleteButtonDown => Frame.Tool.InputManager.Instance.GetDeleteButtonDown;

        public bool GetCButtonDown => Frame.Tool.InputManager.Instance.GetCButtonDown;

        public bool GetVButtonDown => Frame.Tool.InputManager.Instance.GetVButtonDown;

        public bool GetGButtonDown => Frame.Tool.InputManager.Instance.GetGButtonDown;

        public bool GetPButtonDown => Frame.Tool.InputManager.Instance.GetPButtonDown;

        public bool GetRButtonDown => Frame.Tool.InputManager.Instance.GetRButtonDown;

        public bool GetSButtonDown => Frame.Tool.InputManager.Instance.GetSButtonDown;
    }
}