using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public class EditorController
    {
        private MotionController m_motionController;

        private Information m_information;

        public EditorController(RectTransform levelEditorTransform,CommandExcute levelEditorCommandExcute)
        {
            ControllerInit(levelEditorTransform,levelEditorCommandExcute);
            MotionInit();
        }
    
        void ControllerInit(RectTransform levelEditorTransform,CommandExcute levelEditorCommandExcute)
        {
            m_information = new Information(levelEditorTransform,levelEditorCommandExcute);
            m_motionController = new MotionController(m_information);
        }
    
        private void MotionInit()
        {
            m_motionController.ChangeMotionState(typeof(CameraDefultState));
            m_motionController.ChangeMotionState(typeof(PanelDefultState));
        }
    
        public void LateUpdate()
        {
            m_motionController.Motion(m_information);
        }
    }
}
