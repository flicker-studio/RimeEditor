using Cysharp.Threading.Tasks;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    //TODO: rename
    public class LevelEditorBehaviour : MonoBehaviour
    {
        private readonly EditorController m_editorController = new();

        private readonly CommandInvoker m_commandInvoker = new();

        private readonly UniTaskCompletionSource m_completionSource = new();

        
        
        
        
        private async void Start()
        {
            await Explorer.BootCompletionTask;
            await m_editorController.Init(transform as RectTransform, m_commandInvoker.CommandSet);
            m_completionSource.TrySetResult();
        }


        private void LateUpdate()
        {
            if (m_completionSource.Task.Status == UniTaskStatus.Succeeded)
            {
                m_editorController.LateUpdate();
            }
        }

        private void Update()
        {
            if (m_completionSource.Task.Status != UniTaskStatus.Succeeded)
            {
                return;
            }

            //TODO:目前与输入框互动时Redo和Undo会有BUG，出于架构考虑，暂时在想解决办法，在想用不用全局事件
            var zButtonDown = Frame.Tool.InputManager.Instance.GetZButtonDown;

            if (Frame.Tool.InputManager.Instance.GetCtrlButton && Frame.Tool.InputManager.Instance.GetShiftButton && zButtonDown)

                // if(InputManager.Instance.GetDebuggerNum2Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandInvoker.CommandSet.GetRedo?.Invoke();
            }
            else if (Frame.Tool.InputManager.Instance.GetCtrlButton && zButtonDown)

                // }else if(InputManager.Instance.GetDebuggerNum1Up)
            {
                // EventCenterManager.Instance.EventTrigger(GameEvent.UNDO_AND_REDO);
                m_commandInvoker.CommandSet.GetUndo?.Invoke();
            }
        }

        private void OnEnable()
        {
            if (m_commandInvoker != null)
            {
                m_commandInvoker.CommandSet.EnableExcute?.Invoke();
            }
        }
    }
}