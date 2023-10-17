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
            m_motionController = new MotionController(m_information,new AdditiveStateFactory());
        }
    
        private void MotionInit()
        {
            m_motionController.ChangeMotionState(MOTIONSTATEENUM.CameraDefultState);
            m_motionController.ChangeMotionState(MOTIONSTATEENUM.ItemTransformPanelShowState);
            m_motionController.ChangeMotionState(MOTIONSTATEENUM.ActionPanelShowState);
            m_motionController.ChangeMotionState(MOTIONSTATEENUM.ControlHandlePanelShowState);
        }
    
        public void LateUpdate()
        {
            m_motionController.Motion(m_information);
        }
    }
}
