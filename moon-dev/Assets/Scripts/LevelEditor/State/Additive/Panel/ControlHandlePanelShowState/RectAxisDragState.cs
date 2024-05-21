using System;
using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor.Command;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.InputSystem;
using Rect = LevelEditor.Command.Rect;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class RectAxisDragState : AdditiveState
    {
        private UIManager GetUI => m_information.UIManager;
        private RectTransform GetRectRect => GetUI.GetControlHandlePanel.GetRectRect;
        
        private List<Item> TargetItems => m_information.DataManager.TargetItems;

        private List<GameObject> TargetObjs => m_information.DataManager.TargetObjs;

        private Vector3 GetMouseWorldPoint => m_information.CameraManager.MouseWorldPosition;

        private Vector2 GetMousePosition => m_information.CameraManager.MousePosition;

        private Vector2 GetMouseCursorCompensation => m_information.UIManager.GetControlHandlePanel
            .GetMouseCursorProperty.CURSOR_BOUND_CHECK_COMPENSATION;

        private bool GetMouseLeftButtonUp => m_information.InputManager.GetMouseLeftButtonUp;


        private bool GetUseGrid => GetUI.GetControlHandlePanel.GetControlHandleAction.UseGrid;

        private float GetCellHalfSize => GetCellSize / 2f;

        private float GetCellHalfHalfSize => GetCellHalfSize / 2f;

        private float GetCellSize => GetUI.GetControlHandlePanel.GetGridSnappingProperty.CELL_SIZE;

        public enum RECTDRAGTYPE
        {
            TopRightCorner,

            TopLeftCorner,

            BottomRightCorner,

            BottomLeftCorner,

            TopEdge,

            RightEdge,

            BottomEdge,

            LeftEdge,

            Center
        }

        private RECTDRAGTYPE m_rectDragType;

        private Vector3 m_mouseScreenPosition;

        private Vector3 m_currentMouseWorldPosition;

        private Vector3 m_originMouseWorldPosition;

        private Vector3 m_centerPosition;

        private List<Vector3> m_targetOriginPosition = new List<Vector3>();

        private List<Vector3> m_targetCurrentPosition = new List<Vector3>();

        private List<Vector3> m_targetOriginScale = new List<Vector3>();

        private List<Vector3> m_targetCurrentScale = new List<Vector3>();

        public RectAxisDragState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
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
                    m_targetCurrentScale.Add(TargetObjs[i].transform.localScale);
                }

                CommandInvoker.Execute(new Rect(TargetItems, m_targetOriginPosition
                                              , m_targetCurrentPosition, m_targetOriginScale, m_targetCurrentScale));

                RemoveState();
                return;
            }

            CheckMouseScreenPosition();
            UpdateRect();
        }

        private void UpdateRect()
        {
            m_currentMouseWorldPosition = GetMouseWorldPoint;
            Vector3 moveDir = m_currentMouseWorldPosition - m_originMouseWorldPosition;

            if (moveDir.magnitude == 0) return;

            for (var index = 0; index < TargetObjs.Count; index++)
            {
                Vector3 scaleDir = Vector3.one;
                Vector3 currentMouseProject;
                Vector3 originMouseProject;
                Vector3 positionOffset = m_targetOriginPosition[index] - m_centerPosition;
                Vector3 rate = Vector3.zero;
                Vector3 newScale = Vector3.zero;
                Vector3 oldScale = Vector3.zero;
                Vector3 newPosition = Vector3.zero;
                Vector3 positionOffsetProject = Vector3.zero;

                switch (m_rectDragType)
                {
                    case RECTDRAGTYPE.Center:
                        if (GetUseGrid && TargetObjs.Count > 1)
                        {
                            moveDir = new Vector3(GetCellHalfSize * Mathf.RoundToInt(moveDir.x / GetCellHalfSize)
                                , GetCellHalfSize * Mathf.RoundToInt(moveDir.y / GetCellHalfSize)
                                , moveDir.z);
                        }

                        TargetObjs[index].transform.position = m_targetOriginPosition[index] + moveDir;

                        TargetObjs[index].transform.position.NewX((float)Math.Round(TargetObjs[index].transform.position.x, 2))
                            .NewY((float)Math.Round(TargetObjs[index].transform.position.y, 2));

                        if (GetUseGrid && TargetObjs.Count == 1)
                        {
                            TargetObjs[index].transform.position =
                                new Vector3(GetCellHalfSize * Mathf.RoundToInt(TargetObjs[index].transform.position.x / GetCellHalfSize)
                                    , GetCellHalfSize * Mathf.RoundToInt(TargetObjs[index].transform.position.y / GetCellHalfSize)
                                    , TargetObjs[index].transform.position.z);
                        }

                        continue;
                    case RECTDRAGTYPE.TopEdge:
                        scaleDir = GetRectRect.transform.up;
                        break;
                    case RECTDRAGTYPE.BottomEdge:
                        scaleDir = -GetRectRect.transform.up;
                        break;
                    case RECTDRAGTYPE.RightEdge:
                        scaleDir = GetRectRect.transform.right;
                        break;
                    case RECTDRAGTYPE.LeftEdge:
                        scaleDir = -GetRectRect.transform.right;
                        break;
                    case RECTDRAGTYPE.BottomLeftCorner:
                        break;
                    case RECTDRAGTYPE.BottomRightCorner:
                        break;
                    case RECTDRAGTYPE.TopLeftCorner:
                        break;
                    case RECTDRAGTYPE.TopRightCorner:
                        break;
                    default:
                        continue;
                }

                currentMouseProject = m_currentMouseWorldPosition + m_originMouseWorldPosition - 2 * m_centerPosition;
                originMouseProject = 2 * (m_originMouseWorldPosition - m_centerPosition);

                if (scaleDir != Vector3.one)
                {
                    currentMouseProject = Vector3
                        .Project(currentMouseProject, scaleDir);

                    originMouseProject = Vector3
                        .Project(originMouseProject, scaleDir);
                }

                rate = currentMouseProject.DivideVector(originMouseProject);

                if (scaleDir == GetRectRect.transform.up || scaleDir == -GetRectRect.transform.up)
                {
                    rate = rate.NewX(1);
                }

                if (scaleDir == GetRectRect.transform.right || scaleDir == -GetRectRect.transform.right)
                {
                    rate = rate.NewY(1);
                }

                rate = rate.NewZ(1);

                newScale = m_targetOriginScale[index].HadamardProduct(rate);

                if (scaleDir != Vector3.one)
                {
                    positionOffsetProject = Vector3.Project(positionOffset, scaleDir);
                }
                else
                {
                    positionOffsetProject = positionOffset;
                }

                newPosition = m_centerPosition + positionOffset + positionOffsetProject
                    .HadamardProduct(new Vector3(rate.x - 1, rate.y - 1, rate.z - 1)) + (currentMouseProject - originMouseProject) / 2;

                if (GetUseGrid)
                {
                    Vector3 oldSize = TargetObjs[index].GetComponent<MeshFilter>().mesh.bounds.size
                        .HadamardProduct(TargetObjs[index].transform.localScale);

                    Vector3 newSize = TargetObjs[index].GetComponent<MeshFilter>().mesh.bounds.size.HadamardProduct(newScale);

                    oldSize = new Vector3(GetCellHalfSize * Mathf.RoundToInt(oldSize.x / GetCellHalfSize),
                        GetCellHalfSize * Mathf.RoundToInt(oldSize.y / GetCellHalfSize),
                        GetCellHalfSize * Mathf.RoundToInt(oldSize.z / GetCellHalfSize));

                    newSize = new Vector3(GetCellHalfSize * Mathf.RoundToInt(newSize.x / GetCellHalfSize),
                        GetCellHalfSize * Mathf.RoundToInt(newSize.y / GetCellHalfSize),
                        GetCellHalfSize * Mathf.RoundToInt(newSize.z / GetCellHalfSize));

                    if (oldSize == newSize) return;

                    Vector3 tempScale = newSize.DivideVector(TargetObjs[index].GetComponent<MeshFilter>().mesh.bounds.size);

                    Vector3 tempPosition = new Vector3(GetCellHalfHalfSize * Mathf.RoundToInt(newPosition.x / GetCellHalfHalfSize),
                        GetCellHalfHalfSize * Mathf.RoundToInt(newPosition.y / GetCellHalfHalfSize),
                        GetCellHalfHalfSize * Mathf.RoundToInt(newPosition.z / GetCellHalfHalfSize));

                    switch (m_rectDragType)
                    {
                        case RECTDRAGTYPE.LeftEdge:
                            newScale = newScale.NewX(tempScale.x);
                            newPosition = newPosition.NewX(tempPosition.x);
                            break;
                        case RECTDRAGTYPE.RightEdge:
                            newScale = newScale.NewX(tempScale.x);
                            newPosition = newPosition.NewX(tempPosition.x);
                            break;
                        case RECTDRAGTYPE.TopEdge:
                            newScale = newScale.NewY(tempScale.y);
                            newPosition = newPosition.NewY(tempPosition.y);
                            break;
                        case RECTDRAGTYPE.BottomEdge:
                            newScale = newScale.NewY(tempScale.y);
                            newPosition = newPosition.NewY(tempPosition.y);
                            break;
                        default:
                            newScale = newScale.NewX(tempScale.x).NewY(tempScale.y);
                            newPosition = newPosition.NewX(tempPosition.x).NewY(tempPosition.y);
                            break;
                    }
                }

                if (GetUseGrid)
                {
                    TargetObjs[index].transform.localScale = newScale.NewX((float)Math.Round(newScale.x, 1))
                        .NewY((float)Math.Round(newScale.y, 1));
                }
                else
                {
                    TargetObjs[index].transform.localScale = newScale.NewX((float)Math.Round(newScale.x, 2))
                        .NewY((float)Math.Round(newScale.y, 2));
                }

                TargetObjs[index].transform.position = newPosition;

                TargetObjs[index].transform.position = TargetObjs[index].transform.position.NewX((float)Math.Round(TargetObjs[index].transform.position.x, 2))
                    .NewY((float)Math.Round(TargetObjs[index].transform.position.y, 2));
            }
        }

        private void StateInit()
        {
            InitDragType();
            InitData();
        }

        private void InitData()
        {
            m_originMouseWorldPosition = GetMouseWorldPoint;

            for (var i = 0; i < TargetObjs.Count; i++)
            {
                m_targetOriginScale.Add(TargetObjs[i].transform.localScale);
                m_targetOriginPosition.Add(TargetObjs[i].transform.position);
            }

            m_centerPosition = m_targetOriginPosition.GetCenterPoint();
        }

        private void InitDragType()
        {
            if (GetUI.GetControlHandlePanel.GetRectCenterInput)
            {
                m_rectDragType = RECTDRAGTYPE.Center;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectBottomEdgeInput)
            {
                m_rectDragType = RECTDRAGTYPE.BottomEdge;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectLeftEdgeInput)
            {
                m_rectDragType = RECTDRAGTYPE.LeftEdge;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectRightEdgeInput)
            {
                m_rectDragType = RECTDRAGTYPE.RightEdge;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectTopEdgeInput)
            {
                m_rectDragType = RECTDRAGTYPE.TopEdge;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectBottomLeftCornerInput)
            {
                m_rectDragType = RECTDRAGTYPE.BottomLeftCorner;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectBottomRightCornerInput)
            {
                m_rectDragType = RECTDRAGTYPE.BottomRightCorner;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectTopLeftCornerInput)
            {
                m_rectDragType = RECTDRAGTYPE.TopLeftCorner;
                return;
            }

            if (GetUI.GetControlHandlePanel.GetRectTopRightCornerInput)
            {
                m_rectDragType = RECTDRAGTYPE.TopRightCorner;
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
                Mouse.current.WarpCursorPosition(new Vector2(Screen.width - GetMouseCursorCompensation.x,
                    m_mouseScreenPosition.y));
            }

            if (m_mouseScreenPosition.y >= Screen.height)
            {
                Mouse.current.WarpCursorPosition(new Vector2(m_mouseScreenPosition.x, GetMouseCursorCompensation.y));
            }

            if (m_mouseScreenPosition.y <= 0)
            {
                Mouse.current.WarpCursorPosition(new Vector2(m_mouseScreenPosition.x,
                    Screen.height - GetMouseCursorCompensation.y));
            }
        }
    }
}