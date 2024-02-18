using System;
using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor.Command;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class PositionAxisDragState : AdditiveState
    {
        public enum POSITIONDRAGTYPE
        {
            XAxis,

            YAxis,

            XYAxis
        }

        private POSITIONDRAGTYPE m_positionDragType;

        private ControlHandlePanel GetControlHandlePanel => m_information.UIManager.GetControlHandlePanel;

        private List<AbstractItem> TargetItems => m_information.DataManager.TargetItems;

        private List<GameObject> TargetObjs => m_information.DataManager.TargetObjs;

        private Vector3 GetMouseWorldPoint => m_information.CameraManager.MouseWorldPosition;

        private Vector2 GetMousePosition => m_information.CameraManager.MousePosition;

        private Vector2 GetMouseCursorCompensation => GetControlHandlePanel
            .GetMouseCursorProperty.CURSOR_BOUND_CHECK_COMPENSATION;

        private bool GetMouseLeftButtonUp => m_information.InputManager.GetMouseLeftButtonUp;


        private bool GetUseGrid => GetControlHandlePanel.GetControlHandleAction.UseGrid;

        private float GetCellHalfSize => GetControlHandlePanel.GetGridSnappingProperty.CELL_SIZE / 2f;

        private Vector3 m_originMouseWorldPosition;

        private Vector3 m_currentMouseWorldPosition;

        private List<Vector3> m_targetOriginScale = new List<Vector3>();

        private List<Vector3> m_targetCurrentScale = new List<Vector3>();

        private List<Vector3> m_targetOriginPosition = new List<Vector3>();

        private List<Vector3> m_targetCurrentPosition = new List<Vector3>();

        private Vector3 m_mouseScreenPosition;

        public PositionAxisDragState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            StateInit();
        }

        public override void Motion(BaseInformation information)
        {
            if (GetMouseLeftButtonUp)
            {
                for (var i = 0; i < TargetObjs.Count; i++)
                {
                    m_targetCurrentPosition.Add(TargetObjs[i].transform.position);
                }

                CommandInvoker.Execute(new Position(TargetItems, m_targetCurrentPosition));
                RemoveState();
                return;
            }

            CheckMouseScreenPosition();
            UpdatePosition();
        }

        private void StateInit()
        {
            if (m_information.UIManager.GetControlHandlePanel.GetPositionInputX)
            {
                m_positionDragType = POSITIONDRAGTYPE.XAxis;
            }
            else if (m_information.UIManager.GetControlHandlePanel.GetPositionInputY)
            {
                m_positionDragType = POSITIONDRAGTYPE.YAxis;
            }
            else if (m_information.UIManager.GetControlHandlePanel.GetPositionInputXY)
            {
                m_positionDragType = POSITIONDRAGTYPE.XYAxis;
            }

            m_originMouseWorldPosition = GetMouseWorldPoint;

            for (var i = 0; i < TargetObjs.Count; i++)
            {
                m_targetOriginPosition.Add(TargetObjs[i].transform.position);
            }
        }

        private void UpdatePosition()
        {
            m_currentMouseWorldPosition = GetMouseWorldPoint;
            Vector3 moveDir = m_currentMouseWorldPosition - m_originMouseWorldPosition;

            if (moveDir.magnitude == 0) return;

            if (GetUseGrid && TargetObjs.Count > 1)
            {
                moveDir = new Vector3(GetCellHalfSize * Mathf.RoundToInt(moveDir.x / GetCellHalfSize)
                    , GetCellHalfSize * Mathf.RoundToInt(moveDir.y / GetCellHalfSize)
                    , moveDir.z);
            }

            for (var i = 0; i < TargetObjs.Count; i++)
            {
                switch (m_positionDragType)
                {
                    case POSITIONDRAGTYPE.XAxis:
                        TargetObjs[i].transform.position = m_targetOriginPosition[i] + moveDir.NewY(0);
                        break;
                    case POSITIONDRAGTYPE.YAxis:
                        TargetObjs[i].transform.position = m_targetOriginPosition[i] + moveDir.NewX(0);
                        break;
                    case POSITIONDRAGTYPE.XYAxis:
                        TargetObjs[i].transform.position = m_targetOriginPosition[i] + moveDir;
                        break;
                    default:
                        continue;
                }

                TargetObjs[i].transform.position = TargetObjs[i].transform.position.NewX((float)Math.Round(TargetObjs[i].transform.position.x, 2))
                    .NewY((float)Math.Round(TargetObjs[i].transform.position.y, 2));

                if (GetUseGrid && TargetObjs.Count == 1)
                {
                    switch (m_positionDragType)
                    {
                        case POSITIONDRAGTYPE.XAxis:
                            TargetObjs[i].transform.position = TargetObjs[i].transform.position
                                .NewX(GetCellHalfSize * Mathf.RoundToInt(TargetObjs[i].transform.position.x / GetCellHalfSize));

                            break;
                        case POSITIONDRAGTYPE.YAxis:
                            TargetObjs[i].transform.position = TargetObjs[i].transform.position
                                .NewY(GetCellHalfSize * Mathf.RoundToInt(TargetObjs[i].transform.position.y / GetCellHalfSize));

                            break;
                        case POSITIONDRAGTYPE.XYAxis:
                            TargetObjs[i].transform.position =
                                new Vector3(GetCellHalfSize * Mathf.RoundToInt(TargetObjs[i].transform.position.x / GetCellHalfSize)
                                    , GetCellHalfSize * Mathf.RoundToInt(TargetObjs[i].transform.position.y / GetCellHalfSize)
                                    , TargetObjs[i].transform.position.z);

                            break;
                        default:
                            continue;
                    }
                }
            }
        }

        private void CheckMouseScreenPosition()
        {
            m_mouseScreenPosition = GetMousePosition;

            if (m_mouseScreenPosition.x >= Screen.width)
            {
                Mouse.current.WarpCursorPosition(new Vector2(GetMouseCursorCompensation.x, m_mouseScreenPosition.y));
            }

            if (m_mouseScreenPosition.x <= 0)
            {
                Mouse.current.WarpCursorPosition(new Vector2(Screen.width - GetMouseCursorCompensation.x, m_mouseScreenPosition.y));
            }

            if (m_mouseScreenPosition.y >= Screen.height)
            {
                Mouse.current.WarpCursorPosition(new Vector2(m_mouseScreenPosition.x, GetMouseCursorCompensation.y));
            }

            if (m_mouseScreenPosition.y <= 0)
            {
                Mouse.current.WarpCursorPosition(new Vector2(m_mouseScreenPosition.x, Screen.height - GetMouseCursorCompensation.y));
            }
        }
    }
}