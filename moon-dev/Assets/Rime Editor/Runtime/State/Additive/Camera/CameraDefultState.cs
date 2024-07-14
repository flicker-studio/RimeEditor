using System;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class CameraDefultState : AdditiveState
    {
        /*
            private bool GetMouseMiddleButtonDown => m_information.InputManager.GetMouseMiddleButtonDown;

            private bool GetMouseSrollDown => m_information.InputManager.GetMouseSrollDown;
    */
        public CameraDefultState(Information Information, MotionCallBack motionCallBack) : base(Information, motionCallBack)
        {
        }

        public override void Motion(Information information)
        {
            CheckCameraInput();
        }

        private void CheckCameraInput()
        {
            //TODO:需加载SO
            throw new Exception("需加载SO");
            // if (GetMouseMiddleButtonDown)
            // {
            //     if (CheckStates.Contains(typeof(CameraMoveState))) return;
            //
            //     GameObject pointerUIObj = GetTopUIObjectUnderMouse();
            //     if (pointerUIObj && !pointerUIObj.CompareTag(GlobalSetting.Tags.CONTROL_HANDLE)) return;
            //
            //     ChangeMotionState(typeof(CameraMoveState));
            // }
            //
            // if (GetMouseSrollDown)
            // {
            //     if (CheckStates.Contains(typeof(CameraChangeZState))) return;
            //
            //     GameObject pointerUIObj = GetTopUIObjectUnderMouse();
            //     if (pointerUIObj && !pointerUIObj.CompareTag(GlobalSetting.Tags.CONTROL_HANDLE)) return;
            //
            //     ChangeMotionState(typeof(CameraChangeZState));
            // }
        }

        private GameObject GetTopUIObjectUnderMouse()
        {
            var pointerData = new PointerEventData(EventSystem.current)
                              {
                                  position = Mouse.current.position.ReadValue()
                              };

            var results = new List<RaycastResult>();
            EventSystem.current.RaycastAll(pointerData, results);

            if (results.Count > 0) return results[0].gameObject;

            return null;
        }
    }
}