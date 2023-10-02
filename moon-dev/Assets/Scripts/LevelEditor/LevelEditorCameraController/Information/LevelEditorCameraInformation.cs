using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Tool;
using UnityEngine;

public class LevelEditorCameraInformation : BaseInformation
{
    private Transform m_cameraTransform;

    private Camera m_camera;

    private LevelEditorCameraProperty m_cameraProperty;

    public Transform GetCameraTransform => m_cameraTransform;

    public Camera GetCamera => m_camera;

    public float GetCameraMaxZ => m_cameraProperty.FovProperty.CAMERA_MAX_Z;
    
    public float GetCameraMinZ => m_cameraProperty.FovProperty.CAMERA_MIN_Z;
    
    public float GetCameraZChangeSpeed => m_cameraProperty.FovProperty.CAMERA_Z_CHANGE_SPEED;
    
    public bool GetMouseLeftButton => InputManager.Instance.GetMouseLeftButton;
    
    public bool GetMouseLeftButtonDown => InputManager.Instance.GetMouseLeftButtonDown;
    
    public bool GetMouseLeftButtonUp => InputManager.Instance.GetMouseLeftButtonUp;
    
    public bool GetMouseRightButton => InputManager.Instance.GetMouseRightButton;
    
    public bool GetMouseRightButtonDown => InputManager.Instance.GetMouseRightButtonDown;
    
    public bool GetMouseRightButtonUp => InputManager.Instance.GetMouseRightButtonUp;

    
    public bool GetMouseMiddleButton => InputManager.Instance.GetMouseMiddleButton;

    public float GetMouseSroll => InputManager.Instance.GetMouseScroll;
    
    public bool GetMouseSrollDown => InputManager.Instance.GetMouseScrollDown;
    
    public bool GetMouseSrollUp => InputManager.Instance.GetMouseScrollUp;
    
    public bool GetMouseMiddleButtonDown => InputManager.Instance.GetMouseMiddleButtonDown;
    
    public bool GetMouseMiddleButtonUp => InputManager.Instance.GetMouseMiddleButtonUp;


    public LevelEditorCameraInformation(Transform cameraTransform)
    {
        m_cameraTransform = cameraTransform;
        m_camera = cameraTransform.GetComponent<Camera>();
        m_cameraProperty = Resources.Load<LevelEditorCameraProperty>("GlobalSettings/LevelEditorCameraProperty");
    }
}
