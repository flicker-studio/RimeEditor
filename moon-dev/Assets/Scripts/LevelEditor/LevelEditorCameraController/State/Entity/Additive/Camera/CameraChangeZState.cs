using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class CameraChangeZState : AdditiveState
{
    private float GetMouseScroll => m_information.GetInput.GetMouseSroll;

    private bool GetMouseScrollUp => m_information.GetInput.GetMouseSrollUp;

    private Transform GetCameraTransform => Camera.main.transform;

    private float GetCameraMaxZ => m_information.GetCameraProperty.GetCameraMotionProperty.CAMERA_MAX_Z;

    private float GetCameraMinZ => m_information.GetCameraProperty.GetCameraMotionProperty.CAMERA_MIN_Z;

    private float GetCameraZChangeSpeed => m_information.GetCameraProperty.GetCameraMotionProperty.CAMERA_Z_CHANGE_SPEED;

    private Vector3 GetMouseWorldPoint => m_information.GetMouseWorldPoint;

    private Vector3 m_originMousePosition;

    private Vector3 m_currentMousePositon;
    
    public CameraChangeZState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        m_originMousePosition = GetMouseWorldPoint;
        ChangeZValue();
    }

    public override void Motion(BaseInformation information)
    {
        if (GetMouseScrollUp)
        {
            RemoveState();
            return;
        }

        ChangeZValue();
    }

    private void ChangeZValue()
    {
        if (GetMouseScroll < 0)
        {
            if (GetCameraTransform.position.z < GetCameraMaxZ) return;

            GetCameraTransform.position = GetCameraTransform.position
                .NewZ(GetCameraTransform.position.z - GetCameraZChangeSpeed);
        }

        if (GetMouseScroll > 0)
        {
            if (GetCameraTransform.position.z > GetCameraMinZ) return;

            GetCameraTransform.position = GetCameraTransform.position
                .NewZ(GetCameraTransform.position.z + GetCameraZChangeSpeed);
        }

        GetCameraTransform.position = GetCameraTransform.position
            .NewZ(Mathf.Clamp(GetCameraTransform.position.z, GetCameraMaxZ, GetCameraMinZ));
        
        m_currentMousePositon = GetMouseWorldPoint;
        
        Vector3 moveDir = m_originMousePosition - m_currentMousePositon;
        GetCameraTransform.position += moveDir;
    }
}

}