using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;

public class RotationAxisDragState : LevelEditorCameraAdditiveState
{
    private List<GameObject> TagetList => m_information.TargetList;
    
    private RectTransform GetRotationAxisRectTransform => m_information.GetUI.GetControlHandlePanel.GetRotationRect;
    
    private Vector3 GetMousePosition => m_information.GetMousePosition;

    private bool GetMouseLeftButtonUp => m_information.GetInput.GetMouseLeftButtonUp;
    
    private LevelEditorCommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;
    
    private Vector3 m_originPosition;

    private Vector3 m_currentPosition;

    private Vector3 m_originDir;

    private float GetRotationSpeed => m_information.GetUI.GetControlHandlePanel.GetRotationDragProperty.ROTATION_SPEED;

    private Transform GetCameraTransform => Camera.main.transform;

    private Vector3 GetRotationAxisScreenPosition => GetRotationAxisRectTransform.anchoredPosition;

    private Vector3 GetRotationAxisWorldPosition => Camera.main.ScreenToWorldPoint
        (GetRotationAxisScreenPosition.NewZ(Mathf.Abs(GetCameraTransform.position.z)));
    
    private List<Quaternion> m_targetOriginRotation = new List<Quaternion>();

    private List<Vector3> m_targetOriginPosition = new List<Vector3>();

    private List<Quaternion> m_targetCurrentRotation = new List<Quaternion>();

    private List<Vector3> m_targetCurrentPosition = new List<Vector3>();
    
    public RotationAxisDragState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        StateInit();
    }

    public override void Motion(BaseInformation information)
    {
        if (GetMouseLeftButtonUp)
        {
            GetRotationAxisRectTransform.transform.rotation = Quaternion.identity;
            for (var i = 0; i < TagetList.Count; i++)
            {
                m_targetCurrentPosition.Add(TagetList[i].transform.position);
                m_targetCurrentRotation.Add(TagetList[i].transform.rotation);
            }
            GetExcute?.Invoke(new ItemRotationCommand(TagetList,m_targetOriginPosition,m_targetCurrentPosition,
            m_targetOriginRotation,m_targetCurrentRotation));
            RemoveState();
            return;
        }
        UpdateRotation();
    }

    //TODO:旋转拖动会有值突然非常大的情况，目前还不知道造成这个BUG的原因
    private void UpdateRotation()
    {
        m_currentPosition = GetMousePosition;
        Vector3 mouseDir = (m_currentPosition - m_originPosition).normalized;
        float mouseDis = (m_currentPosition - m_originPosition).magnitude;
        Vector3 dirCross = Vector3.Cross(m_originDir, mouseDir);
        float rotationDirAndMultiplying= dirCross.z;
        Quaternion rotationQuaternion = Quaternion.Euler(0, 0, mouseDis * rotationDirAndMultiplying * GetRotationSpeed);
        GetRotationAxisRectTransform.rotation = rotationQuaternion;
        
        for (var i = 0; i < TagetList.Count; i++)
        {
            TagetList[i].transform.rotation = m_targetOriginRotation[i] * rotationQuaternion;
            TagetList[i].transform.position = GetRotationAxisWorldPosition
                                              + Quaternion.Euler(Vector3.forward * (mouseDis * rotationDirAndMultiplying * GetRotationSpeed)) *
                                              (m_targetOriginPosition[i] - GetRotationAxisWorldPosition);
        }
    }
    
    private void StateInit()
    {
        m_originPosition = GetMousePosition;
        m_originDir = (m_originPosition - GetRotationAxisScreenPosition).normalized;
        
        for (var i = 0; i < TagetList.Count; i++)
        {
            m_targetOriginRotation.Add(TagetList[i].transform.rotation);
            m_targetOriginPosition.Add(TagetList[i].transform.position);
        }
    }
}
