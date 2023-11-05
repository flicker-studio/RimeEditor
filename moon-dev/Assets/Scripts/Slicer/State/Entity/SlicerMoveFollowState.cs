using System;
using Frame.StateMachine;
using UnityEngine;
using static UnityEngine.GridBrushBase;

namespace Slicer.State
{
    public class SlicerMoveFollowState : SlicerMainMotionState
    {
        private Vector3 m_lastFrameCursorPosition;
        
        private float m_angle;

        private float m_distance;

        private Vector3 m_currentMousePosition;
        
        # region GetProperty
        
        private int GetRotationThreshold => m_slicerInformation.GetRotationThreshold;

        private bool GetRotationDirection => m_slicerInformation.GetRotationDirection;

        private Vector3 GetMousePosition => m_slicerInformation.GetMousePosition;
        
        private Vector2 GetOffSet => m_slicerInformation.GetSliceOffset;
        
        private Transform GetTransform => m_slicerInformation.GetTransform;
        
        private Transform GetPlayerTransform => m_slicerInformation.GetPlayerTransform;
        
        # endregion
        
        public SlicerMoveFollowState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            Debug.Log("移动");
            m_lastFrameCursorPosition = Input.mousePosition;
            m_distance = GetOffSet.magnitude;
            m_angle = Mathf.Atan(GetOffSet.y / GetOffSet.x);
        }

        public override void Motion(BaseInformation information)    
        {
            m_currentMousePosition = GetMousePosition;
            
            var curThreshold = m_currentMousePosition.x - m_lastFrameCursorPosition.x;
            
            if (curThreshold > GetRotationThreshold && !GetRotationDirection)
            {
                ChangeMotionState(typeof(SlicerRotationFollowState));
                return;
            } else if (curThreshold < -GetRotationThreshold && GetRotationDirection)
            {
                ChangeMotionState(typeof(SlicerRotationFollowState));
                return;
            }
            
            m_lastFrameCursorPosition = m_currentMousePosition;
            GetTransform.position = GetPlayerTransform.position +
                                    GetPlayerTransform.rotation * 
                                    new Vector3((GetRotationDirection ? 1 : -1) * Mathf.Cos(m_angle), Mathf.Sin(m_angle), 0) * 
                                    m_distance;
            var angles = GetPlayerTransform.eulerAngles;
            GetTransform.rotation = Quaternion.Euler(angles.x,angles.y, angles.z);
        }
    }
}