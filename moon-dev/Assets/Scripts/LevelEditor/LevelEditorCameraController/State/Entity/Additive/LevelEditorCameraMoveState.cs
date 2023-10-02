using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditorCameraMoveState : LevelEditorCameraAdditiveState
{
    private Vector3 m_originMousePosition;
    private Vector3 GetMousePosition => Mouse.current.position.ReadValue();

    private Transform GetTransform => m_cameraInformation.GetCameraTransform;

    private Vector3 MouseWorldPoint =>
        Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Camera.main.farClipPlane * .5f));
    
    public LevelEditorCameraMoveState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_originMousePosition = MouseWorldPoint;
    }

    public override void Motion(BaseInformation information)
    {
        if (m_cameraInformation.GetMouseMiddleButtonUp)
        {
            RemoveState();
            return;
        }
        Vector3 different = MouseWorldPoint - GetTransform.position;
        GetTransform.position = m_originMousePosition - different;
    }
}
