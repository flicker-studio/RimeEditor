using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;

public class LevelEditorCameraChangeFovState : LevelEditorCameraAdditiveState
{
    private float GetMouseScroll => m_cameraInformation.GetMouseSroll;

    private bool GetMouseScrollUp => m_cameraInformation.GetMouseSrollUp;

    private Transform GetCameraTransform => m_cameraInformation.GetCameraTransform;

    private float GetCameraMaxZ => m_cameraInformation.GetCameraMaxZ;

    private float GetCameraMinZ => m_cameraInformation.GetCameraMinZ;

    private float GetCameraZChangeSpeed => m_cameraInformation.GetCameraZChangeSpeed;
    
    public LevelEditorCameraChangeFovState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        ChangeFovValue();
    }

    public override void Motion(BaseInformation information)
    {
        
        if (GetMouseScrollUp)
        {
            RemoveState();
            return;
        }

        ChangeFovValue();
    }

    private void ChangeFovValue()
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
    }
}
