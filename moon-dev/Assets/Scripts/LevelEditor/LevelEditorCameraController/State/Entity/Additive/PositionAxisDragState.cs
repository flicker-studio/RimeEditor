using System;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;

public class PositionAxisDragState : LevelEditorAdditiveState
{
    public enum POSITIONDRAGTYPE
    {
        XAxis,
        YAxis,
        XYAxis
    }

    private POSITIONDRAGTYPE m_positionDragType;

    private List<GameObject> TagetList => m_information.TargetList;
    
    private Vector3 GetMouseWorldPoint => m_information.GetMouseWorldPoint;

    private bool GetMouseLeftButtonUp => m_information.GetInput.GetMouseLeftButtonUp;

    private LevelEditorCommandExcute GetExcute => m_information.GetLevelEditorCommandExcute;

    private Vector3 m_originPosition;

    private Vector3 m_currentPosition;

    private List<Vector3> m_targetOriginPosition = new List<Vector3>();

    private List<Vector3> m_targetCurrentPosition = new List<Vector3>();
    
    public PositionAxisDragState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
    {
        StateInit();
    }

    public override void Motion(BaseInformation information)
    {
        if (GetMouseLeftButtonUp)
        {
            for (var i = 0; i < TagetList.Count; i++)
            {
                m_targetCurrentPosition.Add(TagetList[i].transform.position);
            }
            GetExcute?.Invoke(new ItemPositionCommand(TagetList,m_targetOriginPosition,m_targetCurrentPosition));
            RemoveState();
            return;
        }
        UpdatePosition();
    }

    private void StateInit()
    {
        if (m_information.GetUI.GetControlHandlePanel.GetPositionInputX)
        {
            m_positionDragType = POSITIONDRAGTYPE.XAxis;
        }else if (m_information.GetUI.GetControlHandlePanel.GetPositionInputY)
        {
            m_positionDragType = POSITIONDRAGTYPE.YAxis;
        }else if (m_information.GetUI.GetControlHandlePanel.GetPositionInputXY)
        {
            m_positionDragType = POSITIONDRAGTYPE.XYAxis;
        }

        m_originPosition = GetMouseWorldPoint;
        
        for (var i = 0; i < TagetList.Count; i++)
        {
            m_targetOriginPosition.Add(TagetList[i].transform.position);
        }
    }
    
    private void UpdatePosition()
    {
        m_currentPosition = GetMouseWorldPoint;

        Vector3 moveDir = m_currentPosition - m_originPosition;
        
        for (var i = 0; i < TagetList.Count; i++)
        {
            switch (m_positionDragType)
            {
                case POSITIONDRAGTYPE.XAxis:
                    TagetList[i].transform.position = m_targetOriginPosition[i] + moveDir.NewY(0);
                    break;
                case POSITIONDRAGTYPE.YAxis:
                    TagetList[i].transform.position = m_targetOriginPosition[i] + moveDir.NewX(0);
                    break;
                case POSITIONDRAGTYPE.XYAxis:
                    TagetList[i].transform.position = m_targetOriginPosition[i] + moveDir;
                    break;
                default:
                    continue;
            }

            TagetList[i].transform.position = TagetList[i]
                .transform.position.NewX((float)Math.Round(TagetList[i].transform.position.x,2))
                                    .NewY((float)Math.Round(TagetList[i].transform.position.y,2));
        }
    }
}
