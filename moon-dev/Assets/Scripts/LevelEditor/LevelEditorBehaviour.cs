using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    //TODO: rename
    public class LevelEditorBehaviour : MonoBehaviour
    {
        private readonly CommandInvoker m_commandInvoker = new();

        private readonly UniTaskCompletionSource m_completionSource = new();

        private readonly Information m_information = new();

        private MotionController m_motionController;

        public async UniTask Init(RectTransform levelEditorTransform, CommandSet commandSet)
        {
            await m_information.Init(levelEditorTransform, commandSet);
            m_motionController = new MotionController(m_information);
            m_motionController.ChangeMotionState(typeof(CameraDefultState));
            m_motionController.ChangeMotionState(typeof(LevelManagerPanelShowState));
        }


        private async void Start()
        {
            await Explorer.BootCompletionTask;
            await Init(transform as RectTransform, m_commandInvoker.CommandSet);
            m_completionSource.TrySetResult();
        }


        private void LateUpdate()
        {
            if (m_completionSource.Task.Status == UniTaskStatus.Succeeded)
            {
                m_motionController.Motion(m_information);
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