using Frame.Tool;

namespace LevelEditor
{
    public class InputController
    {
        public bool GetMouseLeftButton => InputManager.Instance.GetMouseLeftButton;
    
        public bool GetMouseLeftButtonDown => InputManager.Instance.GetMouseLeftButtonDown;
    
        public bool GetMouseLeftButtonUp => InputManager.Instance.GetMouseLeftButtonUp;
    
        public bool GetMouseRightButton => InputManager.Instance.GetMouseRightButton;
    
        public bool GetMouseRightButtonDown => InputManager.Instance.GetMouseRightButtonDown;
    
        public bool GetMouseRightButtonUp => InputManager.Instance.GetMouseRightButtonUp;
    
        public bool GetMouseMiddleButton => InputManager.Instance.GetMouseMiddleButton;

        public float GetMouseSroll => InputManager.Instance.GetMouseScroll;
    
        public bool GetMouseSrollDown => InputManager.Instance.GetMouseScrollDown;
    
        public bool GetMouseSrollUp => InputManager.Instance.GetMouseScrollUp;
    
        public bool GetMouseMiddleButtonDown => InputManager.Instance.GetMouseMiddleButtonDown;
    
        public bool GetMouseMiddleButtonUp => InputManager.Instance.GetMouseMiddleButtonUp;

        public bool GetShiftButton => InputManager.Instance.GetShiftButton;

        public bool GetCtrlButton => InputManager.Instance.GetCtrlButton;

        public bool GetDeleteButtonDown => InputManager.Instance.GetDeleteButtonDown;
    }

}