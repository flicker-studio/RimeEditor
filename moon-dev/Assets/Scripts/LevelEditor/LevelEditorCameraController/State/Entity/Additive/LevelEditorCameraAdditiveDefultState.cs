using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEditor;
using UnityEngine;

public class LevelEditorCameraAdditiveDefultState : LevelEditorCameraAdditiveState
{
    private List<GameObject> TargetList => m_cameraInformation.TargetList;

    private LevelEditorCommandExcute GetExcute => m_cameraInformation.GetLevelEditorCommandExcute;

    private LevelEditorAction GetLevelEditorAction => m_cameraInformation.GetLevelEditorAction;

    private GameObject GetPositionAxisObj => m_cameraInformation.GetPositionAxisRectTransform.gameObject;

    private GameObject GetRotationAxisObj => m_cameraInformation.GetRotationAxisRectTransform.gameObject;

    private bool GetPositionAxisXButtonDown => m_cameraInformation.GetInput.GetPositionAxisXButtonDown;
    
    private bool GetPositionAxisYButtonDown => m_cameraInformation.GetInput.GetPositionAxisYButtonDown;
    
    private bool GetPositionAxisXYButtonDown => m_cameraInformation.GetInput.GetPositionAxisXYButtonDown;

    private bool GetPositionAxisButtonDown => GetPositionAxisXYButtonDown || GetPositionAxisXButtonDown || GetPositionAxisYButtonDown;

    private bool GetRotationAxisZButtonDown => m_cameraInformation.GetInput.GetRotationAxisZButtonDown;

    private bool GetUndoButtonDown => m_cameraInformation.GetInput.GetUndoButtonDown;
    
    private bool GetRedoButtonDown => m_cameraInformation.GetInput.GetRedoButtonDown;

    private bool GetViewButtonDown => m_cameraInformation.GetInput.GetViewButtonDown;

    private bool GetPositionButtonDown => m_cameraInformation.GetInput.GetPositionButtonDown;

    private bool GetRotationButtonDown => m_cameraInformation.GetInput.GetRotationButtonDown;

    private bool GetScaleButtonDown => m_cameraInformation.GetInput.GetScaleButtonDown;

    private bool GetRectButtonDown => m_cameraInformation.GetInput.GetRectButtonDown;
    
    
    public LevelEditorCameraAdditiveDefultState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        ShowActionView(GetLevelEditorAction.LevelEditorActionType);
        ButtonDetection();
    }
    
    private void ButtonDetection()
    {
        if (m_cameraInformation.GetInput.GetMouseMiddleButtonDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorCameraMoveState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraMoveState);
            }
        }

        if (m_cameraInformation.GetInput.GetMouseSrollDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorCameraChangeZState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraChangeZState);
            }
        }

        if (m_cameraInformation.GetInput.GetMouseLeftButtonDown && !UIMethod.IsPointerOverUIElement())
        {
            if (!CheckStates.Contains(typeof(MouseSelecteState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.MouseSelecteState);
            }
        }
        
        if (GetPositionAxisButtonDown)
        {
            if (!CheckStates.Contains(typeof(PositionAxisDragState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.PositionAxisDragState);
            }
        }

        if (GetRotationAxisZButtonDown)
        {
            if (!CheckStates.Contains(typeof(RotationAxisDragState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.RotationAxisDragState);
            }
        }

        if (GetPositionButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetLevelEditorAction,LEVELEDITORACTIONTYPE.PositionAxisButton));
        }else if (GetRotationButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetLevelEditorAction,LEVELEDITORACTIONTYPE.RotationAxisButton));
        }
    }
    
    private List<Vector3> GetVector3ListFromGameObjectList(List<GameObject> gameobjectList)
    {
        List<Vector3> vector3List = new List<Vector3>();
        foreach (var obj in gameobjectList)
        {
            vector3List.Add(obj.transform.position);
        }

        return vector3List;
    }
    
    private void ShowActionView(LEVELEDITORACTIONTYPE levelEditorActionType)
    {
        switch (levelEditorActionType)
        {
            case LEVELEDITORACTIONTYPE.PositionAxisButton:
                GetRotationAxisObj.SetActive(false);
                GetPositionAxisObj.transform.position = Camera.main
                    .WorldToScreenPoint(GetVector3ListFromGameObjectList(TargetList).GetCenterPoint());
                if (TargetList.Count > 0)
                {
                    GetPositionAxisObj.SetActive(true);
                }
                else
                {
                    GetPositionAxisObj.SetActive(false);
                }
                break;
            case LEVELEDITORACTIONTYPE.RotationAxisButton:
                GetPositionAxisObj.SetActive(false);
                GetRotationAxisObj.transform.position = Camera.main
                    .WorldToScreenPoint(GetVector3ListFromGameObjectList(TargetList).GetCenterPoint());
                if (TargetList.Count > 0)
                {
                    GetRotationAxisObj.SetActive(true);
                }
                else
                {
                    GetRotationAxisObj.SetActive(false);
                }
                break;
        }
    }
}
