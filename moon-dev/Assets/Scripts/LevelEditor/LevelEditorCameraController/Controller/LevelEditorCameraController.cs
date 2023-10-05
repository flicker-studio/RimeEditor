using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public class LevelEditorCameraController
{
    private MotionController m_motionController;

    private LevelEditorCameraInformation m_cameraInformation;

    public LevelEditorCameraController(RectTransform selectionUI)
    {
        ControllerInit(selectionUI);
        MotionInit();
    }
    
    void ControllerInit(RectTransform selectionUI)
    {
        m_cameraInformation = new LevelEditorCameraInformation(selectionUI);
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
