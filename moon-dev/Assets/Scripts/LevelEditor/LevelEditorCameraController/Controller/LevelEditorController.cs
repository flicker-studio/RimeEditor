using Frame.StateMachine;
using UnityEngine;

public class LevelEditorController
{
    private MotionController m_motionController;

    private LevelEditorInformation m_information;

    public LevelEditorController(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        ControllerInit(levelEditorTransform,levelEditorCommandExcute);
        MotionInit();
    }
    
    void ControllerInit(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        m_information = new LevelEditorInformation(levelEditorTransform,levelEditorCommandExcute);
        m_motionController = new MotionController(m_information);
    }
    
    private void MotionInit()
    {
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraDefultState);
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.ItemTransformPanelShowState);
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.ActionPanelShowState);
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.ControlHandlePanelShowState);
    }
    
    public void LateUpdate()
    {
        m_motionController.Motion(m_information);
    }
}
