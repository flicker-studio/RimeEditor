using System;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class ScaleAxisDragState : AdditiveState
    {
        #region Other types of variables.

        public enum SCALEDRAGTYPE
        {
            XAxis,
            YAxis,
            XYAxis
        }

        private SCALEDRAGTYPE m_scaleDragType;
        
        private FlagProperty m_waitToNextFrame;

        #endregion

        #region Get properties.

        private CameraManager GetCamera => m_information.GetCamera;

        private DataManager GetData => m_information.GetData;

        private UIManager GetUI => m_information.GetUI;

        private InputController GetInput => m_information.GetInput;

        private CommandSet GetCommandSet => m_information.GetCommandSet;
        
        private ObservableList<ItemData> TargetItems => GetData.TargetItems;
        
        private List<GameObject> TargetObjs => GetData.TargetObjs;

        private RectTransform GetScaleRect => GetUI.GetControlHandlePanel.GetScaleRect;

        private RectTransform GetScaleXAxisRect => GetUI.GetControlHandlePanel.GetScaleXAxisRect;
        
        private RectTransform GetScaleYAxisRect => GetUI.GetControlHandlePanel.GetScaleYAxisRect;
        
        private float GetScaleSpeed => m_information.GetUI.GetControlHandlePanel.GetScaleDragProperty.SCALE_SPEED;

        private float GetCenterAxisCompensation =>
            m_information.GetUI.GetControlHandlePanel.GetScaleDragProperty.CENTER_AXIS_COMPENSATION;

        private Vector2 GetMousePosition => GetCamera.GetMousePosition;

        private Vector2 GetMouseCursorCompensation => GetUI.GetControlHandlePanel
            .GetMouseCursorProperty.CURSOR_BOUND_CHECK_COMPENSATION;
        
        private bool GetMouseLeftButtonUp => GetInput.GetMouseLeftButtonUp;
        
        private CommandExcute GetExcute => GetCommandSet.GetExcute;

        #endregion

        #region Position variate and scale variate.

        private Vector2 m_originMouseSumVector;
        
        private Vector2 m_originMousePosition;
    
        private Vector2 m_currentMousePosition;
    
        private List<Vector3> m_targetOriginScale = new List<Vector3>();
    
        private List<Vector3> m_targetCurrentScale = new List<Vector3>();

        private List<Vector3> m_targetOriginPosition = new List<Vector3>();

        private List<Vector3> m_targetCurrentPosition = new List<Vector3>();
        
        private List<Vector2> m_mousePosVectorList = new List<Vector2>();

        private Vector3 m_centerPosition;

        #endregion

        #region Scale's position variate and size variate.

        private Vector2 m_scaleAxisXOriginPos;
        
        private Vector2 m_scaleAxisYOriginPos;
        
        private Vector2 m_scaleAxisXOriginSize;
        
        private Vector2 m_scaleAxisYOriginSize;
        
        #endregion
        
        public ScaleAxisDragState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            StateInit();
        }

        public override void Motion(BaseInformation information)
        {
            if (GetMouseLeftButtonUp)
            {
                StateReset();
                
                for (var i = 0; i < TargetObjs.Count; i++)
                {
                    m_targetCurrentScale.Add(TargetObjs[i].transform.localScale);
                }
                for (var i = 0; i < TargetObjs.Count; i++)
                {
                    m_targetCurrentPosition.Add(TargetObjs[i].transform.position);
                }
                GetExcute?.Invoke(new ItemScaleCommand(TargetItems,m_targetOriginPosition
                    ,m_targetCurrentPosition,m_targetOriginScale,m_targetCurrentScale));
                RemoveState();
                return;
            }
            CheckMouseScreenPosition();
            UpdateScale();
        }
        
        private void UpdateScale()
        {
            if(m_waitToNextFrame.GetFlag) return;
            Vector2 mouseSumVector = Vector2.zero;
            foreach (var posVector in m_mousePosVectorList)
            {
                mouseSumVector += posVector;
            }
            m_currentMousePosition = GetMousePosition;
            mouseSumVector += m_currentMousePosition - m_originMousePosition;
            for (var i = 0; i < TargetObjs.Count; i++)
            {
                Vector3 currentMouseProject = Vector3.zero;
                Vector3 originMouseProject = Vector3.zero;
                Vector3 scaleDir;
                Vector3 positionOffset;
                Vector3 rate;
                positionOffset = m_targetOriginPosition[i] - m_centerPosition;
                switch (m_scaleDragType)
                {
                    case SCALEDRAGTYPE.XAxis:
                        scaleDir = GetScaleRect.transform.right;
                        currentMouseProject = Vector3.Project(mouseSumVector,scaleDir);
                        originMouseProject = Vector3.Project(m_originMouseSumVector,scaleDir);
                        rate = currentMouseProject.DivideVector(originMouseProject);
                        TargetObjs[i].transform.localScale = m_targetOriginScale[i].HadamardProduct(rate).NewY(1).NewZ(1);
                        TargetObjs[i].transform.position = m_centerPosition + positionOffset 
                            + Vector3.Project(positionOffset,GetScaleRect.right)* (rate.x - 1);
                        break;
                    case SCALEDRAGTYPE.YAxis:
                        scaleDir = GetScaleRect.transform.up;
                        currentMouseProject = Vector3.Project(mouseSumVector,scaleDir);
                        originMouseProject = Vector3.Project(m_originMouseSumVector,scaleDir);
                        rate = currentMouseProject.DivideVector(originMouseProject);
                        TargetObjs[i].transform.localScale = m_targetOriginScale[i].HadamardProduct(rate.NewX(1).NewZ(1));
                        TargetObjs[i].transform.position = m_centerPosition + positionOffset 
                            + Vector3.Project(positionOffset,GetScaleRect.up)* (rate.y - 1);
                        break;
                    case SCALEDRAGTYPE.XYAxis:
                        float upperRightProjectLength = Vector3.Dot(mouseSumVector, mouseSumVector.NewX(1).NewY(1));
                        float lowerLeftProjectLength = Vector3.Dot(mouseSumVector, mouseSumVector.NewX(-1).NewY(-1));
                        if (upperRightProjectLength >= lowerLeftProjectLength)
                        {
                            currentMouseProject = Vector3.Project(mouseSumVector, mouseSumVector.NewX(1).NewY(1));
                            originMouseProject = Vector3.Project(m_originMouseSumVector, mouseSumVector.NewX(1).NewY(1));
                        }
                        else
                        {
                            currentMouseProject = Vector3.Project(mouseSumVector, mouseSumVector.NewX(-1).NewY(-1));
                            originMouseProject = Vector3.Project(m_originMouseSumVector, mouseSumVector.NewX(-1).NewY(-1));
                        }
                        rate = Vector3.one + GetScaleSpeed * (currentMouseProject - originMouseProject);
                        TargetObjs[i].transform.localScale = m_targetOriginScale[i].HadamardProduct(rate.NewZ(1));
                        TargetObjs[i].transform.position = m_centerPosition + positionOffset.HadamardProduct(rate.NewZ(1));
                        break;
                }
                
                ChangeAxisScale(originMouseProject, currentMouseProject);
    
                TargetObjs[i].transform.localScale = TargetObjs[i].transform.localScale.NewX((float)Math.Round(TargetObjs[i].transform.localScale.x,2))
                    .NewY((float)Math.Round(TargetObjs[i].transform.localScale.y,2));
            }
        }
        
        private void StateInit()
        {
            InitDragType();
            InitMousePos();
            InitAxisPosAndSize();
        }

        private void StateReset()
        {
            GetScaleXAxisRect.anchoredPosition = m_scaleAxisXOriginPos;
            GetScaleYAxisRect.anchoredPosition = m_scaleAxisYOriginPos;
            GetScaleXAxisRect.sizeDelta = m_scaleAxisXOriginSize;
            GetScaleYAxisRect.sizeDelta = m_scaleAxisYOriginSize;
            GetScaleXAxisRect.localRotation = Quaternion.identity;
            GetScaleYAxisRect.localRotation = Quaternion.identity;
        }

        private void InitDragType()
        {
            if (m_information.GetUI.GetControlHandlePanel.GetScaleInputX)
            {
                m_scaleDragType = SCALEDRAGTYPE.XAxis;
            }else if (m_information.GetUI.GetControlHandlePanel.GetScaleInputY)
            {
                m_scaleDragType = SCALEDRAGTYPE.YAxis;
            }else if (m_information.GetUI.GetControlHandlePanel.GetScaleInputXY)
            {
                m_scaleDragType = SCALEDRAGTYPE.XYAxis;
            }
        }

        private void InitMousePos()
        {
            m_originMousePosition = GetScaleRect.anchoredPosition;
            m_originMouseSumVector = GetMousePosition - GetScaleRect.anchoredPosition;
            
            for (var i = 0; i < TargetObjs.Count; i++)
            {
                m_targetOriginScale.Add(TargetObjs[i].transform.localScale);
                m_targetOriginPosition.Add(TargetObjs[i].transform.position);
            }

            m_centerPosition = m_targetOriginPosition.GetCenterPoint();
        }

        
        private void ChangeAxisScale(Vector3 oriMouseProject,Vector3 currentMouseProject)
        {
            int axisDir;
            Vector3 mouseProjectDir = currentMouseProject - oriMouseProject;
            switch (m_scaleDragType)
            {
                case SCALEDRAGTYPE.XAxis:
                    axisDir = Vector3.Dot(mouseProjectDir, GetScaleRect.right) >= 0 ? 1 : -1;
                    GetScaleXAxisRect.anchoredPosition = GetScaleXAxisRect.anchoredPosition
                        .NewX(UseCenterAxisCompensation(m_scaleAxisXOriginPos.x + axisDir * mouseProjectDir.magnitude/2));
                    if (GetScaleXAxisRect.anchoredPosition.x > 0)
                    {
                        GetScaleXAxisRect.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        GetScaleXAxisRect.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                    GetScaleXAxisRect.sizeDelta = GetScaleXAxisRect.sizeDelta
                        .NewX(Mathf.Abs(GetScaleXAxisRect.anchoredPosition.x * 2));
                    break;
                case SCALEDRAGTYPE.YAxis:
                    axisDir = Vector3.Dot(mouseProjectDir, GetScaleRect.up) >= 0 ? 1 : -1;
                    GetScaleYAxisRect.anchoredPosition = GetScaleYAxisRect.anchoredPosition
                        .NewY(UseCenterAxisCompensation(m_scaleAxisYOriginPos.y + axisDir * mouseProjectDir.magnitude/2));
                    if (GetScaleYAxisRect.anchoredPosition.y >= 0)
                    {
                        GetScaleYAxisRect.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        GetScaleYAxisRect.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                    GetScaleYAxisRect.sizeDelta = GetScaleYAxisRect.sizeDelta
                        .NewY(Mathf.Abs(GetScaleYAxisRect.anchoredPosition.y * 2));
                    break;
                case SCALEDRAGTYPE.XYAxis:
                    axisDir = Vector3.Dot(mouseProjectDir, GetScaleRect.right) >= 0 ? 1 : -1;
                    GetScaleYAxisRect.anchoredPosition = GetScaleYAxisRect.anchoredPosition
                        .NewY(UseCenterAxisCompensation(m_scaleAxisYOriginPos.y + axisDir * mouseProjectDir.magnitude/2));
                    if (GetScaleYAxisRect.anchoredPosition.y >= 0)
                    {
                        GetScaleYAxisRect.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        GetScaleYAxisRect.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                    GetScaleYAxisRect.sizeDelta = GetScaleYAxisRect.sizeDelta
                        .NewY(Mathf.Abs(GetScaleYAxisRect.anchoredPosition.y * 2));
                    
                    GetScaleXAxisRect.anchoredPosition = GetScaleXAxisRect.anchoredPosition
                        .NewX(UseCenterAxisCompensation(m_scaleAxisXOriginPos.x + axisDir * mouseProjectDir.magnitude/2));
                    if (GetScaleXAxisRect.anchoredPosition.x >= 0)
                    {
                        GetScaleXAxisRect.localRotation = Quaternion.identity;
                    }
                    else
                    {
                        GetScaleXAxisRect.localRotation = Quaternion.Euler(0, 0, 180);
                    }
                    GetScaleXAxisRect.sizeDelta = GetScaleXAxisRect.sizeDelta
                        .NewX(Mathf.Abs(GetScaleXAxisRect.anchoredPosition.x * 2));
                    break;
                default:
                    return;
            }
        }

        private float UseCenterAxisCompensation(float axisValue)
        {
            float compensation = GetCenterAxisCompensation;
            return (axisValue > -compensation && axisValue < compensation)
                ? (axisValue >= 0 ? compensation : -compensation)
                : axisValue;
        }
        
        private void InitAxisPosAndSize()
        {
            m_scaleAxisXOriginPos = GetScaleXAxisRect.anchoredPosition;
            m_scaleAxisYOriginPos = GetScaleYAxisRect.anchoredPosition;
            m_scaleAxisXOriginSize = GetScaleXAxisRect.sizeDelta;
            m_scaleAxisYOriginSize = GetScaleYAxisRect.sizeDelta;
        }
        
        private void CheckMouseScreenPosition()
        {
            m_currentMousePosition = GetMousePosition;
        
            if (m_currentMousePosition.x >= Screen.width)
            {
                m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(GetMouseCursorCompensation.x, GetMousePosition.y));
                m_originMousePosition = GetMousePosition.NewX(0);
                m_waitToNextFrame.SetFlag = true;
                return;
            }
            if (m_currentMousePosition.x <= 0)
            {
                m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(Screen.width - GetMouseCursorCompensation.x, GetMousePosition.y));
                m_originMousePosition = GetMousePosition.NewX(Screen.width);
                m_waitToNextFrame.SetFlag = true;
                return;
            }

            if (m_currentMousePosition.y >= Screen.height)
            {
                m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(GetMousePosition.x, GetMouseCursorCompensation.y));
                m_originMousePosition = GetMousePosition.NewY(0);
                m_waitToNextFrame.SetFlag = true;
                return;
            }
        
            if (m_currentMousePosition.y <= 0)
            {
                m_mousePosVectorList.Add(m_currentMousePosition - m_originMousePosition);
                Mouse.current.WarpCursorPosition(new Vector2(GetMousePosition.x, Screen.height - GetMouseCursorCompensation.y));
                m_originMousePosition = GetMousePosition.NewY(Screen.height);
                m_waitToNextFrame.SetFlag = true;
                return;
            }
        }
    }
}
