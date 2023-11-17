using System.Collections.Generic;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;

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
    
        private bool GetPositionAxisXButtonDown => GetControlHandlePanel.GetPositionInputXDown;
        
        private bool GetPositionAxisYButtonDown => GetControlHandlePanel.GetPositionInputYDown;
        
        private bool GetPositionAxisXYButtonDown => GetControlHandlePanel.GetPositionInputXYDown;
    
        private bool GetPositionAxisButtonDown => GetPositionAxisXYButtonDown || GetPositionAxisXButtonDown || GetPositionAxisYButtonDown;
    
        private bool GetRotationAxisZButtonDown => GetControlHandlePanel.GetRotationInputZDown;

        private List<ItemData> m_copyDatas = new List<ItemData>();
        
        public ControlHandlePanelShowState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            InitEvent();
        }

        private void InitEvent()
        {
            GetData.SyncLevelData += ClearCopyDatas;
        }

        private void ClearCopyDatas(LevelData levelData)
        {
            m_copyDatas.Clear();
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
                    GetRotationAxisObj.SetActive(false);
                    GetPositionAxisObj.transform.position = Camera.main
                        .WorldToScreenPoint(GetPositionListFromGameObjectList(GetData.TargetObjs).GetCenterPoint());
                    if (GetData.TargetObjs.Count > 0)
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
                        .WorldToScreenPoint(GetPositionListFromGameObjectList(GetData.TargetObjs).GetCenterPoint());
                    if (GetData.TargetObjs.Count > 0)
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