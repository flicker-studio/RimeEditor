using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

public class LevelEditorCameraController : MonoBehaviour
{
    private MotionController m_motionController;

    private LevelEditorCameraInformation m_cameraInformation;
    
    void ControllerInit()
    {
        m_cameraInformation = new LevelEditorCameraInformation(transform);
        m_motionController = new MotionController(m_cameraInformation);
    }
    
    private void MotionInit()
    {
        m_motionController.ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraAdditiveDefultState);
    }
        
    private void Start()
    {
        ControllerInit();
        MotionInit();
    }
    
    private void LateUpdate()
    {
        m_motionController.Motion(m_cameraInformation);
    }
}
