using Frame.StateMachine;
using UnityEngine;

public class LevelEditorCameraController
{
    private MotionController m_motionController;

    private LevelEditorCameraInformation m_cameraInformation;

    public LevelEditorCameraController(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        ControllerInit(levelEditorTransform,levelEditorCommandExcute);
        MotionInit();
    }
    
    void ControllerInit(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        m_cameraInformation = new LevelEditorCameraInformation(levelEditorTransform,levelEditorCommandExcute);
        m_motionController = new MotionController(m_cameraInformation);
    }
    
    private void MotionInit()
    {
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraAdditiveDefultState);
    }
    
    public void LateUpdate()
    {
        m_motionController.Motion(m_cameraInformation);
    }
}
