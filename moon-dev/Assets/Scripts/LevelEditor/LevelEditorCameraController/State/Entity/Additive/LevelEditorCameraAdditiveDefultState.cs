using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEditor;
using UnityEngine;

public class LevelEditorCameraAdditiveDefultState : LevelEditorCameraAdditiveState
{
    private List<GameObject> TargetList => m_information.TargetList;

    private LevelEditorCommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

    private ControlHandleAction GetControlHandleAction => m_information.GetUI.GetControlHandlePanel.GetControlHandleAction;

    private GameObject GetPositionAxisObj => m_information.GetUI.GetControlHandlePanel.GetPositionRect.gameObject;

    private GameObject GetRotationAxisObj => m_information.GetUI.GetControlHandlePanel.GetRotationRect.gameObject;

    private bool GetPositionAxisXButtonDown => m_information.GetUI.GetControlHandlePanel.GetPositionInputXDown;
    
    private bool GetPositionAxisYButtonDown => m_information.GetUI.GetControlHandlePanel.GetPositionInputYDown;
    
    private bool GetPositionAxisXYButtonDown => m_information.GetUI.GetControlHandlePanel.GetPositionInputXYDown;

    private bool GetPositionAxisButtonDown => GetPositionAxisXYButtonDown || GetPositionAxisXButtonDown || GetPositionAxisYButtonDown;

    private bool GetRotationAxisZButtonDown => m_information.GetUI.GetControlHandlePanel.GetRotationInputZDown;

    private bool GetUndoButtonDown => m_information.GetUI.GetActionPanel.GetUndoInputDown;
    
    private bool GetRedoButtonDown => m_information.GetUI.GetActionPanel.GetRedoInputDown;

    private bool GetViewButtonDown => m_information.GetUI.GetActionPanel.GetViewInputDown;

    private bool GetPositionButtonDown => m_information.GetUI.GetActionPanel.GetPositionInputDown;

    private bool GetRotationButtonDown => m_information.GetUI.GetActionPanel.GetRotationInputDown;

    private bool GetScaleButtonDown => m_information.GetUI.GetActionPanel.GetScaleInputDown;

    private bool GetRectButtonDown => m_information.GetUI.GetActionPanel.GetRectInputDown;
    
    
    public LevelEditorCameraAdditiveDefultState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        ShowActionView(GetControlHandleAction.ControlHandleActionType);
        ButtonDetection();
    }
    
    private void ButtonDetection()
    {
        if (m_information.GetInput.GetMouseMiddleButtonDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorCameraMoveState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraMoveState);
            }
        }

        if (m_information.GetInput.GetMouseSrollDown)
        {
            if(!CheckStates.Contains(typeof(LevelEditorCameraChangeZState)))
            {
                ChangeMotionState(MOTIONSTATEENUM.LevelEditorCameraChangeZState);
            }
        }

        if (m_information.GetInput.GetMouseLeftButtonDown && !UIMethod.IsPointerOverUIElement())
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
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.PositionAxisButton));
        }else if (GetRotationButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.RotationAxisButton));
        }else if (GetViewButtonDown)
        {
            GetExcute?.Invoke(new LevelEditorActionChangeCommand(GetControlHandleAction,CONTROLHANDLEACTIONTYPE.ViewButton));
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
    
    private void ShowActionView(CONTROLHANDLEACTIONTYPE levelEditorActionType)
    {
        switch (levelEditorActionType)
        {
            case CONTROLHANDLEACTIONTYPE.PositionAxisButton:
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
            case CONTROLHANDLEACTIONTYPE.RotationAxisButton:
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
            case CONTROLHANDLEACTIONTYPE.ViewButton:
                GetPositionAxisObj.SetActive(false);
                GetRotationAxisObj.SetActive(false);
                break;
        }
    }
}
