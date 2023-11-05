using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEditor;
using UnityEngine;

namespace LevelEditor
{
    public class ControlHandlePanelShowState : AdditiveState
{
    private List<GameObject> TargetList => m_information.TargetList;

    private ControlHandlePanel GetControlHandlePanel => m_information.GetUI.GetControlHandlePanel;

    private ControlHandleAction GetControlHandleAction => GetControlHandlePanel.GetControlHandleAction;

    private GameObject GetPositionAxisObj => GetControlHandlePanel.GetPositionRect.gameObject;

    private GameObject GetRotationAxisObj => GetControlHandlePanel.GetRotationRect.gameObject;

    private bool GetPositionAxisXButtonDown => GetControlHandlePanel.GetPositionInputXDown;
    
    private bool GetPositionAxisYButtonDown => GetControlHandlePanel.GetPositionInputYDown;
    
    private bool GetPositionAxisXYButtonDown => GetControlHandlePanel.GetPositionInputXYDown;

    private bool GetPositionAxisButtonDown => GetPositionAxisXYButtonDown || GetPositionAxisXButtonDown || GetPositionAxisYButtonDown;

    private bool GetRotationAxisZButtonDown => GetControlHandlePanel.GetRotationInputZDown;
    
    public ControlHandlePanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
    {
    }

    public override void Motion(BaseInformation information)
    {
        ShowActionView(GetControlHandleAction.ControlHandleActionType);

        CheckButton();
    }

    private void CheckButton()
    {
        if (m_information.GetInput.GetMouseLeftButtonDown && !UIMethod.IsPointerOverUIElement())
        {
            if (!CheckStates.Contains(typeof(MouseSelecteState)))
            {
                ChangeMotionState(typeof(MouseSelecteState));
            }
        }
        
        if (GetPositionAxisButtonDown)
        {
            if (!CheckStates.Contains(typeof(PositionAxisDragState)))
            {
                ChangeMotionState(typeof(PositionAxisDragState));
            }
        }

        if (GetRotationAxisZButtonDown)
        {
            if (!CheckStates.Contains(typeof(RotationAxisDragState)))
            {
                ChangeMotionState(typeof(RotationAxisDragState));
            }
        }
    }
    
    private void ShowActionView(CONTROLHANDLEACTIONTYPE levelEditorActionType)
    {
        switch (levelEditorActionType)
        {
            case CONTROLHANDLEACTIONTYPE.PositionAxisButton:
                GetRotationAxisObj.SetActive(false);
                GetPositionAxisObj.transform.position = Camera.main
                    .WorldToScreenPoint(GetPositionListFromGameObjectList(TargetList).GetCenterPoint());
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
                    .WorldToScreenPoint(GetPositionListFromGameObjectList(TargetList).GetCenterPoint());
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
    
    private List<Vector3> GetPositionListFromGameObjectList(List<GameObject> gameobjectList)
    {
        List<Vector3> positionList = new List<Vector3>();
        foreach (var obj in gameobjectList)
        {
            positionList.Add(obj.transform.position);
        }

        return positionList;
    }
}

}