using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

public class LevelEditorCameraChangeZState : LevelEditorCameraAdditiveState
{
    private float GetMouseScroll => m_cameraInformation.GetInput.GetMouseSroll;

    private bool GetMouseScrollUp => m_cameraInformation.GetInput.GetMouseSrollUp;

    private Transform GetCameraTransform => m_cameraInformation.GetCameraTransform;

    private float GetCameraMaxZ => m_cameraInformation.GetCameraMaxZ;

    private float GetCameraMinZ => m_cameraInformation.GetCameraMinZ;

    private float GetCameraZChangeSpeed => m_cameraInformation.GetCameraZChangeSpeed;

    private Vector3 GetMouseWorldPoint => m_cameraInformation.GetMouseWorldPoint;

    private Vector3 m_originMousePosition;

    private Vector3 m_currentMousePositon;
    
    public LevelEditorCameraChangeZState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
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
