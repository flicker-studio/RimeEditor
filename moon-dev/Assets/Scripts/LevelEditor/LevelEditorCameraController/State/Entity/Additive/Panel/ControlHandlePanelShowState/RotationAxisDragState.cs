using System;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
public class RotationAxisDragState : AdditiveState
{
    private ObservableList<ItemData> TagetItems => m_information.GetData.TargetItems;

    private List<GameObject> TargetObjs => m_information.GetData.TargetObjs;
    
    private ControlHandlePanel GetControlHandlePanel => m_information.GetUI.GetControlHandlePanel;
    private RectTransform GetRotationAxisRectTransform => GetControlHandlePanel.GetRotationRect;
    
    private Vector2 GetMouseCursorCompensation => GetControlHandlePanel
        .GetMouseCursorProperty.CURSOR_BOUND_CHECK_COMPENSATION;
    
    private Vector3 GetMousePosition => m_information.GetCamera.GetMousePosition;

    private bool GetMouseLeftButtonUp => m_information.GetInput.GetMouseLeftButtonUp;
    
    private CommandExcute GetExcute => m_information.GetCommandSet.GetExcute;
    
    private bool GetUseGrid => GetControlHandlePanel.GetControlHandleAction.UseGrid;

    private float GetRotationUnit => GetControlHandlePanel.GetGridSnappingProperty.ROTATION_UNIT;
    
    private Vector3 m_originMousePosition;

    private Vector3 m_currentMousePosition;

    private Vector3 m_originMouseToAxisDir;
    
    private Vector3 m_oriRotationAxisPos;

    private FlagProperty m_waitToNextFrame;

    private float GetRotationSpeed => m_information.GetUI.GetControlHandlePanel.GetRotationDragProperty.ROTATION_SPEED;

    private Transform GetCameraTransform => Camera.main.transform;

    private Vector3 GetRotationAxisScreenPosition => GetRotationAxisRectTransform.anchoredPosition;

    private Vector3 GetRotationAxisWorldPosition => Camera.main.ScreenToWorldPoint
        (GetRotationAxisScreenPosition.NewZ(Mathf.Abs(GetCameraTransform.position.z)));
    
    private List<Quaternion> m_targetOriginRotation = new List<Quaternion>();

    private List<Vector3> m_targetOriginPosition = new List<Vector3>();

    private List<Quaternion> m_targetCurrentRotation = new List<Quaternion>();

    private List<Vector3> m_targetCurrentPosition = new List<Vector3>();

    private List<Vector3> m_mousePosVectorList = new List<Vector3>();
    
    public RotationAxisDragState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        StateInit();
    }

    public override void Motion(BaseInformation information)
    {
        if (GetMouseLeftButtonUp)
        {
            GetRotationAxisRectTransform.transform.rotation = Quaternion.identity;
            for (var i = 0; i < TargetObjs.Count; i++)
            {
                m_targetCurrentPosition.Add(TargetObjs[i].transform.position);
                m_targetCurrentRotation.Add(TargetObjs[i].transform.rotation);
            }
            GetExcute?.Invoke(new ItemRotationCommand(TagetItems,m_targetOriginPosition,m_targetCurrentPosition,
            m_targetOriginRotation,m_targetCurrentRotation));
            RemoveState();
            return;
        }

        CheckMouseScreenPosition();
        UpdateRotation();
    }
    
    private void UpdateRotation()
    {
        if (m_waitToNextFrame.GetFlag) return;
        Vector3 mouseSumVector = Vector3.zero;
        foreach (var posVector in m_mousePosVectorList)
        {
            mouseSumVector += posVector;
        }
        m_currentMousePosition = GetMousePosition;
        mouseSumVector += m_currentMousePosition - m_originMousePosition;
        
        if(mouseSumVector.magnitude == 0) return;
        
        Vector3 mouseDir = mouseSumVector.normalized;
        float mouseDis = mouseSumVector.magnitude;
        Vector3 dirCross = Vector3.Cross(m_originMouseToAxisDir, mouseDir);
        float rotationDirAndMultiplying= dirCross.z;
        Quaternion rotationQuaternion = Quaternion
            .Euler(0, 0, (float)Math.Round(mouseDis * rotationDirAndMultiplying * GetRotationSpeed,2));
        
        if (GetUseGrid && TargetObjs.Count > 1)
        {
            rotationQuaternion = 
                Quaternion.Euler(rotationQuaternion.eulerAngles
                    .NewZ(GetRotationUnit * Mathf.RoundToInt(rotationQuaternion.eulerAngles.z / GetRotationUnit)));
        }
        
        GetRotationAxisRectTransform.rotation = rotationQuaternion;
        GetRotationAxisRectTransform.position = m_oriRotationAxisPos;
        for (var i = 0; i < TargetObjs.Count; i++)
        {
            if (TargetObjs[i].transform.rotation == m_targetOriginRotation[i] * rotationQuaternion)
            {
                continue;
            }
            
            TargetObjs[i].transform.rotation = m_targetOriginRotation[i] * rotationQuaternion;
            TargetObjs[i].transform.position = GetRotationAxisWorldPosition
                                                          + Quaternion.Euler(Vector3.forward * rotationQuaternion.eulerAngles.z).normalized *
                                                          (m_targetOriginPosition[i] - GetRotationAxisWorldPosition);

            if (GetUseGrid && TargetObjs.Count == 1)
            {
                TargetObjs[i].transform.rotation = Quaternion.Euler(TargetObjs[i].transform.rotation.eulerAngles
                    .NewZ(GetRotationUnit *  Mathf.RoundToInt(TargetObjs[i].transform.rotation.eulerAngles.z / GetRotationUnit)));
                GetRotationAxisRectTransform.rotation = TargetObjs[i].transform.rotation;
            }
        }
    }
    
    private void StateInit()
    {
        m_originMousePosition = GetMousePosition;
        m_originMouseToAxisDir = (m_originMousePosition - GetRotationAxisScreenPosition).normalized;
        m_oriRotationAxisPos = GetRotationAxisRectTransform.position;
        
        for (var i = 0; i < TargetObjs.Count; i++)
        {
            m_targetOriginRotation.Add(TargetObjs[i].transform.rotation);
            m_targetOriginPosition.Add(TargetObjs[i].transform.position);
        }
    }
    
    private void CheckMouseScreenPosition()
    {
        m_currentMousePosition = GetMousePosition;
        
        if (m_currentMousePosition.x >= Screen.width)
        {
            m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
            Mouse.current.WarpCursorPosition(new Vector2(GetMouseCursorCompensation.x, m_currentMousePosition.y));
            m_originMousePosition = GetMousePosition.NewX(0);
            m_waitToNextFrame.SetFlag = true;
        }
        if (m_currentMousePosition.x <= 0)
        {
            m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
            Mouse.current.WarpCursorPosition(new Vector2(Screen.width - GetMouseCursorCompensation.x, m_currentMousePosition.y));
            m_originMousePosition = GetMousePosition.NewX(Screen.width);
            m_waitToNextFrame.SetFlag = true;
        }

        if (m_currentMousePosition.y >= Screen.height)
        {
            m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
            Mouse.current.WarpCursorPosition(new Vector2(m_currentMousePosition.x, GetMouseCursorCompensation.y));
            m_originMousePosition = GetMousePosition.NewY(0);
            m_waitToNextFrame.SetFlag = true;
        }
        
        if (m_currentMousePosition.y <= 0)
        {
            m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
            Mouse.current.WarpCursorPosition(new Vector2(m_currentMousePosition.x, Screen.height - GetMouseCursorCompensation.y));
            m_originMousePosition = GetMousePosition.NewY(Screen.height);
            m_waitToNextFrame.SetFlag = true;
        }
    }
}

}