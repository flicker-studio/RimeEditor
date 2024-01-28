using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public class EditorController
    {
        private readonly Information m_information = new();

        private MotionController m_motionController;

        public async UniTask Init(RectTransform levelEditorTransform, CommandSet commandSet)
        {
            await m_information.Init(levelEditorTransform, commandSet);
            m_motionController = new MotionController(m_information);
            MotionInit();
        }

        private void MotionInit()
        {
            m_motionController.ChangeMotionState(typeof(CameraDefultState));
            m_motionController.ChangeMotionState(typeof(LevelManagerPanelShowState));
        }

        public void LateUpdate()
        {
            m_motionController.Motion(m_information);
        }
    }
}