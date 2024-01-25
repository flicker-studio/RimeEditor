using System;
using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.InputSystem;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class ScaleAxisDragState : AdditiveState
    {

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

        private bool GetUseGrid => GetUI.GetControlHandlePanel.GetControlHandleAction.UseGrid;

        private float GetCellSize => GetUI.GetControlHandlePanel.GetGridSnappingProperty.CELL_SIZE;
        
        private float GetCellHalfSize => GetUI.GetControlHandlePanel.GetGridSnappingProperty.CELL_SIZE / 2f;

        #endregion

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
        
        private void StateInit()
        {
            InitDragType();
            InitData();
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
        
            Vector3 scaleDir = Vector3.zero;
            Vector3 currentMouseProject;
            Vector3 originMouseProject;
        
            if(mouseSumVector.magnitude == 0) return;
            
            switch (m_scaleDragType)
            {
                case SCALEDRAGTYPE.XAxis:
                    scaleDir = GetScaleRect.transform.right;
                    break;
                case SCALEDRAGTYPE.YAxis:
                    scaleDir = GetScaleRect.transform.up;
                    break;
                case SCALEDRAGTYPE.XYAxis:
                    scaleDir = GetXYAxisScaleDir(mouseSumVector);
                    break;
            }
        
            currentMouseProject = Vector3.Project(mouseSumVector, scaleDir);
            originMouseProject = Vector3.Project(m_originMouseSumVector, scaleDir);
        
            for (var i = 0; i < TargetObjs.Count; i++)
            {
                ApplyScaleToTarget(currentMouseProject, originMouseProject, i);
            }
        }
        
        private Vector3 GetXYAxisScaleDir(Vector2 mouseSumVector)
        {
            float upperRightProjectLength = Vector3.Dot(mouseSumVector, (GetScaleRect.up + GetScaleRect.right).normalized);
            float lowerLeftProjectLength = Vector3.Dot(mouseSumVector, -(GetScaleRect.up + GetScaleRect.right).normalized);
            if (upperRightProjectLength >= lowerLeftProjectLength)
            {
                return mouseSumVector.NewX(1).NewY(1);
            }
            else
            {
                return mouseSumVector.NewX(-1).NewY(-1);
            }
        }
        
        private void ApplyScaleToTarget(Vector3 currentMouseProject, Vector3 originMouseProject, int index)
        {
            Vector3 positionOffset = m_targetOriginPosition[index] - m_centerPosition;
            Vector3 rate = currentMouseProject.DivideVector(originMouseProject);
            Vector3 newScale = Vector3.zero;
            Vector3 newPosition = Vector3.zero;
            switch (m_scaleDragType)
            {
                case SCALEDRAGTYPE.XAxis:
                    newScale = m_targetOriginScale[index].HadamardProduct(rate.NewY(1).NewZ(1));
                    newPosition = m_centerPosition + positionOffset 
                        + Vector3.Project(positionOffset,GetScaleRect.right)* (rate.x - 1);
                    break;
                case SCALEDRAGTYPE.YAxis:
                    newScale = m_targetOriginScale[index].HadamardProduct(rate.NewX(1).NewZ(1));
                    newPosition = m_centerPosition + positionOffset 
                        + Vector3.Project(positionOffset,GetScaleRect.up)* (rate.y - 1);
                    break;
                case SCALEDRAGTYPE.XYAxis:
                    rate = GetScaleRect.up.y > 0
                        ? Vector3.one + GetScaleSpeed * (currentMouseProject - originMouseProject)
                        : Vector3.one + GetScaleSpeed * (originMouseProject - currentMouseProject);
                    newScale = m_targetOriginScale[index].HadamardProduct(rate.NewZ(1));
                    newPosition = m_centerPosition + positionOffset.HadamardProduct(rate.NewZ(1));
                    break;
            }
        
            ChangeAxisScale(originMouseProject, currentMouseProject);
            
            if (GetUseGrid)
            {
                Vector3 oldSize = TargetObjs[index].GetComponent<MeshFilter>().mesh.bounds.size
                    .HadamardProduct(TargetObjs[index].transform.localScale);
                Vector3 newSize = TargetObjs[index].GetComponent<MeshFilter>().mesh.bounds.size.HadamardProduct(newScale);
                oldSize = new Vector3(GetCellSize * Mathf.RoundToInt(oldSize.x/GetCellSize),
                    GetCellSize * Mathf.RoundToInt(oldSize.y/GetCellSize),
                GetCellSize * Mathf.RoundToInt(oldSize.z / GetCellSize));
                newSize = new Vector3(GetCellSize * Mathf.RoundToInt(newSize.x/GetCellSize),
                    GetCellSize * Mathf.RoundToInt(newSize.y/GetCellSize),
                    GetCellSize * Mathf.RoundToInt(newSize.z / GetCellSize));
                if(oldSize == newSize) return;
                Vector3 tempScale = newSize.DivideVector(TargetObjs[index].GetComponent<MeshFilter>().mesh.bounds.size);
                Vector3 tempPosition = new Vector3(GetCellHalfSize * Mathf.RoundToInt(newPosition.x/ GetCellHalfSize),
                    GetCellHalfSize * Mathf.RoundToInt(newPosition.y/ GetCellHalfSize),
                    GetCellHalfSize * Mathf.RoundToInt(newPosition.z / GetCellHalfSize));
                switch (m_scaleDragType)
                {
                    case SCALEDRAGTYPE.XAxis:
                        newScale = newScale.NewX(tempScale.x);
                        newPosition = newPosition.NewX(tempPosition.x);
                        break;
                    case SCALEDRAGTYPE.YAxis:
                        newScale = newScale.NewY(tempScale.y);
                        newPosition = newPosition.NewY(tempPosition.y);
                        break;
                    case SCALEDRAGTYPE.XYAxis:
                        newScale = newScale.NewX(tempScale.x).NewY(tempScale.y);
                        newPosition = newPosition.NewX(tempPosition.x).NewY(tempPosition.y);
                        break;
                }
            }

            if (GetUseGrid)
            {
                TargetObjs[index].transform.localScale = newScale.NewX(Mathf.RoundToInt(newScale.x))
                    .NewY(Mathf.RoundToInt(newScale.y));
            }
            else
            {
                TargetObjs[index].transform.localScale = newScale.NewX((float)Math.Round(newScale.x,2))
                    .NewY((float)Math.Round(newScale.y,2));
            }
            TargetObjs[index].transform.position = newPosition;
        }

        private void InitDragType()
        {
            if (GetUI.GetControlHandlePanel.GetScaleInputX)
            {
                m_scaleDragType = SCALEDRAGTYPE.XAxis;
            }else if (GetUI.GetControlHandlePanel.GetScaleInputY)
            {
                m_scaleDragType = SCALEDRAGTYPE.YAxis;
            }else if (GetUI.GetControlHandlePanel.GetScaleInputXY)
            {
                m_scaleDragType = SCALEDRAGTYPE.XYAxis;
            }
        }

        private void InitData()
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
                    axisDir = Vector3.Dot(mouseProjectDir, (GetScaleRect.right + GetScaleRect.up).normalized) >= 0 ? 1 : -1;
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
