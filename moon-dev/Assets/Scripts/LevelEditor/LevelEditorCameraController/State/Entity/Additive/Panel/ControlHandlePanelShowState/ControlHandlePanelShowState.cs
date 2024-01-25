using System.Collections.Generic;
using System.Linq;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public class ControlHandlePanelShowState : AdditiveState
    {
        private DataManager GetData => m_information.GetData;

        private InputController GetInput => m_information.GetInput;

        private CameraManager GetCamera => m_information.GetCamera;

        private UIManager GetUI => m_information.GetUI;

        private CommandExcute GetExcute => m_information.GetCommandSet.GetExcute;
    
        private ControlHandlePanel GetControlHandlePanel => GetUI.GetControlHandlePanel;
    
        private ControlHandleAction GetControlHandleAction => GetControlHandlePanel.GetControlHandleAction;
    
        private GameObject GetPositionAxisObj => GetControlHandlePanel.GetPositionRect.gameObject;
    
        private GameObject GetRotationAxisObj => GetControlHandlePanel.GetRotationRect.gameObject;
        
        private GameObject GetScaleAxisObj => GetControlHandlePanel.GetScaleRect.gameObject;

        private GameObject GetRectObj => GetControlHandlePanel.GetRectRect.gameObject;

        private GameObject GetGridObj => GetControlHandlePanel.GetGridRect.gameObject;
    
        private bool GetPositionAxisXButtonDown => GetControlHandlePanel.GetPositionInputXDown;
        
        private bool GetPositionAxisYButtonDown => GetControlHandlePanel.GetPositionInputYDown;
        
        private bool GetPositionAxisXYButtonDown => GetControlHandlePanel.GetPositionInputXYDown;

        private bool GetScaleAxisXButtonDown => GetControlHandlePanel.GetScaleInputXDown;
        
        private bool GetScaleAxisYButtonDown => GetControlHandlePanel.GetScaleInputYDown;
        
        private bool GetScaleAxisXYButtonDown => GetControlHandlePanel.GetScaleInputXYDown;

        private bool GetRectTopRightCornerInputDown => GetControlHandlePanel.GetRectTopRightCornerInputDown;

        private bool GetRectTopLeftCornerInputDown => GetControlHandlePanel.GetRectTopLeftCornerInputDown;
        
        private bool GetRectBottomRightCornerInputDown => GetControlHandlePanel.GetRectBottomRightCornerInputDown;
        
        private bool GetRectBottomLeftCornerInputDown => GetControlHandlePanel.GetRectBottomLeftCornerInputDown;
        
        private bool GetRectTopEdgeInputDown => GetControlHandlePanel.GetRectTopEdgeInputDown;
        
        private bool GetRectRightEdgeInputDown => GetControlHandlePanel.GetRectRightEdgeInputDown;
        
        private bool GetRectBottomEdgeInputDown => GetControlHandlePanel.GetRectBottomEdgeInputDown;
        
        private bool GetRectLeftEdgeInputDown => GetControlHandlePanel.GetRectLeftEdgeInputDown;
        
        private bool GetRectCenterInputDown => GetControlHandlePanel.GetRectCenterInputDown;
    
        private bool GetPositionAxisButtonDown => GetPositionAxisXYButtonDown || GetPositionAxisXButtonDown || GetPositionAxisYButtonDown;
    
        private bool GetRotationAxisZButtonDown => GetControlHandlePanel.GetRotationInputZDown;

        private bool GetScaleAxisButtonDown => GetScaleAxisXButtonDown || GetScaleAxisYButtonDown || GetScaleAxisXYButtonDown;

        private bool GetRectAxisButtonDowm => GetRectCenterInputDown || GetRectLeftEdgeInputDown ||
                                              GetRectBottomEdgeInputDown || GetRectRightEdgeInputDown ||
                                              GetRectTopEdgeInputDown || GetRectBottomLeftCornerInputDown ||
                                              GetRectBottomRightCornerInputDown || GetRectTopLeftCornerInputDown ||
                                              GetRectTopRightCornerInputDown;

        private int GetGridSize => GetControlHandlePanel.GetGridSnappingProperty.GRID_SIZE;
        
        private float GetCellSize => GetControlHandlePanel.GetGridSnappingProperty.CELL_SIZE;
        
        private int GetGrowthFactor => GetControlHandlePanel.GetGridSnappingProperty.GROWTH_FACTOR;
        private Color GetGridColor => GetControlHandlePanel.GetGridSnappingProperty.GRID_COLOR;
        
        private List<ItemData> m_copyDatas = new List<ItemData>();

        private GridPainter m_gridPainter;
        
        public ControlHandlePanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitGird();
            InitEvent();
        }

        private void InitEvent()
        {
            GetData.SyncLevelData += ClearCopyDatas;
        }

        private void InitGird()
        {
            m_gridPainter = new GridPainter(GetGridObj, GetGridSize, GetCellSize, GetGrowthFactor, GetGridColor);
        }

        private void ClearCopyDatas(SubLevelData subLevelData)
        {
            m_copyDatas.Clear();
        }
        
        public override void Motion(BaseInformation information)
        {
            ShowActionView(GetControlHandleAction.ControlHandleActionType);
            ShowGrid();
            CheckButton();
        }

        private void ShowGrid()
        {
            m_gridPainter.SetActive(GetControlHandleAction.UseGrid);
            m_gridPainter.UpdateGrid();
        }

        private void CheckButton()
        {
            if (m_information.GetInput.GetMouseLeftButtonDown && !UI.IsPointerOverUIElement())
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

            if (GetScaleAxisButtonDown)
            {
                if (!CheckStates.Contains(typeof(ScaleAxisDragState)))
                {
                    ChangeMotionState(typeof(ScaleAxisDragState));
                }
            }

            if (GetRectAxisButtonDowm)
            {
                if (!CheckStates.Contains(typeof(RectAxisDragState)))
                {
                    ChangeMotionState(typeof(RectAxisDragState));
                }
            }

            if (GetInput.GetGButtonDown)
            {
                GetExcute?.Invoke(new ActionChangeCommand(GetControlHandleAction,!GetControlHandleAction.UseGrid));
            }
            
            if (GetInput.GetCtrlButton && GetInput.GetCButtonDown)
            {
                m_copyDatas.Clear();
                m_copyDatas.AddRange(GetData.TargetItems);
            }

            if (GetInput.GetCtrlButton && GetInput.GetVButtonDown)
            {
                if (m_copyDatas.Count > 0)
                {
                    GetExcute?.Invoke(new ItemCopyCommand(GetData.TargetItems,GetData.ItemAssets
                        ,GetCamera.GetOutlinePainter,m_copyDatas));
                }
            }
        }
    
        private void ShowActionView(CONTROLHANDLEACTIONTYPE levelEditorActionType)
        {
            switch (levelEditorActionType)
            {
                case CONTROLHANDLEACTIONTYPE.PositionAxisButton:
                    ShowPositionAxis();
                    break;
                case CONTROLHANDLEACTIONTYPE.RotationAxisButton:
                    ShowRotationAxis();
                    break;
                case CONTROLHANDLEACTIONTYPE.ScaleAxisButton:
                    ShowScaleAxis();
                    break;
                case CONTROLHANDLEACTIONTYPE.RectButton:
                    ShowRectAxis();
                    break;
                case CONTROLHANDLEACTIONTYPE.ViewButton:
                    HideAxis();
                    break;
            }
        }

        private void ShowPositionAxis()
        {
            GetRotationAxisObj.SetActive(false);
            GetScaleAxisObj.SetActive(false);
            GetRectObj.SetActive(false);
            if (GetData.TargetObjs.Count > 0)
            {
                GetPositionAxisObj.transform.position = Camera.main
                    .WorldToScreenPoint(GetPositionListFromGameObjectList(GetData.TargetObjs).GetCenterPoint());
                GetPositionAxisObj.SetActive(true);
            }
            else
            {
                GetPositionAxisObj.SetActive(false);
            }
        }

        private void ShowRotationAxis()
        {
            GetPositionAxisObj.SetActive(false);
            GetScaleAxisObj.SetActive(false);
            GetRectObj.SetActive(false);
            if (GetData.TargetObjs.Count > 0)
            {
                GetRotationAxisObj.transform.position = Camera.main
                    .WorldToScreenPoint(GetPositionListFromGameObjectList(GetData.TargetObjs).GetCenterPoint());
                GetRotationAxisObj.SetActive(true);
            }
            else
            {
                GetRotationAxisObj.SetActive(false);
            }
        }

        private void ShowScaleAxis()
        {
            GetPositionAxisObj.SetActive(false);
            GetRotationAxisObj.SetActive(false);
            GetRectObj.SetActive(false);
            if (GetData.TargetObjs.Count > 0)
            {
                GetScaleAxisObj.transform.position = Camera.main
                    .WorldToScreenPoint(GetPositionListFromGameObjectList(GetData.TargetObjs).GetCenterPoint());
                GetScaleAxisObj.transform.rotation = GetData.TargetObjs.Last().transform.rotation;
                GetScaleAxisObj.SetActive(true);
            }
            else
            {
                GetScaleAxisObj.SetActive(false);
            }
        }

        private void ShowRectAxis()
        {
            GetPositionAxisObj.SetActive(false);
            GetRotationAxisObj.SetActive(false);
            GetScaleAxisObj.SetActive(false);
            if (GetData.TargetObjs.Count == 0)
            {
                GetRectObj.SetActive(false);
                return;
            }
            
            if (GetData.TargetObjs.Count > 0)
            {
                GetRectObj.SetActive(true);
            }
            
            if (GetData.TargetObjs.Count == 1)
            {
                GameObject lastObj = GetData.TargetObjs.Last();
                
                Renderer renderer = lastObj.GetComponent<Renderer>();

                Quaternion objRotation = lastObj.transform.rotation;

                lastObj.transform.rotation = Quaternion.identity;
                
                Bounds bounds = renderer.bounds;

                lastObj.transform.rotation = objRotation;
                
                Vector3 minXminY = new Vector3(bounds.min.x, bounds.min.y, 0);
                Vector3 maxXminY = new Vector3(bounds.max.x, bounds.min.y, 0);
                Vector3 maxXmaxY = new Vector3(bounds.max.x, bounds.max.y, 0);

                Vector2 newSizeDelta = new Vector2(
                    (Camera.main.WorldToScreenPoint(maxXminY) - Camera.main.WorldToScreenPoint(minXminY)).magnitude,
                    (Camera.main.WorldToScreenPoint(maxXmaxY) - Camera.main.WorldToScreenPoint(maxXminY)).magnitude);
                
                GetRectObj.transform.position = Camera.main.WorldToScreenPoint(lastObj.transform.position);
                GetRectObj.transform.rotation = lastObj.transform.rotation;

                (GetRectObj.transform as RectTransform).sizeDelta = newSizeDelta;
                return;
            }

            if (GetData.TargetObjs.Count > 1)
            {
                float minX = float.MaxValue;
                float minY = float.MaxValue;
                float maxX = float.MinValue;
                float maxY = float.MinValue;
                
                foreach (var targetObj in GetData.TargetObjs)
                {
                    Renderer renderer = targetObj.GetComponent<Renderer>();
                
                    Bounds bounds = renderer.bounds;
                    
                    minX = Mathf.Min(minX, bounds.min.x);
                    minY = Mathf.Min(minY, bounds.min.y);
                    maxX = Mathf.Max(maxX, bounds.max.x);
                    maxY = Mathf.Max(maxY, bounds.max.y);
                }
                
                Vector3 minXminY = new Vector3(minX, minY, 0);
                Vector3 maxXminY = new Vector3(maxX, minY, 0);
                Vector3 maxXmaxY = new Vector3(maxX, maxY, 0);

                Vector2 newSizeDelta = new Vector2(
                    (Camera.main.WorldToScreenPoint(maxXminY) - Camera.main.WorldToScreenPoint(minXminY)).magnitude,
                    (Camera.main.WorldToScreenPoint(maxXmaxY) - Camera.main.WorldToScreenPoint(maxXminY)).magnitude);
                
                GetRectObj.transform.position = 
                    Camera.main.WorldToScreenPoint(new Vector3((minX + maxX)/2,(minY + maxY)/2,0));
                GetRectObj.transform.rotation = Quaternion.identity;

                (GetRectObj.transform as RectTransform).sizeDelta = newSizeDelta;
            }
        }

        private void HideAxis()
        {
            GetPositionAxisObj.SetActive(false);
            GetRotationAxisObj.SetActive(false);
            GetScaleAxisObj.SetActive(false);
            GetRectObj.SetActive(false);
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