using Cysharp.Threading.Tasks;

namespace LevelEditor
{
    public class InputManager : IManager
    {
        /*
           public bool GetCanInput => Moon.Runtime.InputManager.Instance.CanInput;

           public void SetCanInput(bool canInput)
           {
               Moon.Runtime.InputManager.Instance.CanInput = canInput;
           }

           public bool GetMouseLeftButton => Moon.Runtime.InputManager.Instance.GetMouseLeftButton;

           public bool GetMouseLeftButtonDown => Moon.Runtime.InputManager.Instance.GetMouseLeftButtonDown;

           public bool GetMouseLeftButtonUp => Moon.Runtime.InputManager.Instance.GetMouseLeftButtonUp;

           public bool GetMouseRightButton => Moon.Runtime.InputManager.Instance.GetMouseRightButton;

           public bool GetMouseRightButtonDown => Moon.Runtime.InputManager.Instance.GetMouseRightButtonDown;

           public bool GetMouseRightButtonUp => Moon.Runtime.InputManager.Instance.GetMouseRightButtonUp;

           public bool GetMouseMiddleButton => Moon.Runtime.InputManager.Instance.GetMouseMiddleButton;

           public float GetMouseSroll => Moon.Runtime.InputManager.Instance.GetMouseScroll;

           public bool GetMouseSrollDown => Moon.Runtime.InputManager.Instance.GetMouseScrollDown;

           public bool GetMouseSrollUp => Moon.Runtime.InputManager.Instance.GetMouseScrollUp;

           public bool GetMouseMiddleButtonDown => Moon.Runtime.InputManager.Instance.GetMouseMiddleButtonDown;

           public bool GetMouseMiddleButtonUp => Moon.Runtime.InputManager.Instance.GetMouseMiddleButtonUp;

           public bool GetShiftButton => Moon.Runtime.InputManager.Instance.GetShiftButton;

           public bool GetCtrlButton => Moon.Runtime.InputManager.Instance.GetCtrlButton;

           public bool GetDeleteButtonDown => Moon.Runtime.InputManager.Instance.GetDeleteButtonDown;

           public bool GetCButtonDown => Moon.Runtime.InputManager.Instance.GetCButtonDown;

           public bool GetVButtonDown => Moon.Runtime.InputManager.Instance.GetVButtonDown;

           public bool GetGButtonDown => Moon.Runtime.InputManager.Instance.GetGButtonDown;

           public bool GetPButtonDown => Moon.Runtime.InputManager.Instance.GetPButtonDown;

           public bool GetRButtonDown => Moon.Runtime.InputManager.Instance.GetRButtonDown;

           public bool GetSButtonDown => Moon.Runtime.InputManager.Instance.GetSButtonDown;
   */
        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }
    }
}