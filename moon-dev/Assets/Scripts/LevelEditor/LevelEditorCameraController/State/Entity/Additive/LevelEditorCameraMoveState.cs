using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditorCameraMoveState : LevelEditorCameraAdditiveState
{
    private Vector3 m_originMousePosition;

    private Transform GetTransform => Camera.main.transform;

    private Vector3 MouseWorldPoint => m_information.GetMouseWorldPoint;
    
    public LevelEditorCameraMoveState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_originMousePosition = MouseWorldPoint;
    }

    public override void Motion(BaseInformation information)
    {
        if (m_information.GetInput.GetMouseMiddleButtonUp)
        {
            RemoveState();
            return;
        }
        
        Vector3 different = m_originMousePosition - MouseWorldPoint;

        GetTransform.position += different;
    }
}
