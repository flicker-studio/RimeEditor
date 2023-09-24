using Frame.StateMachine;
using UnityEngine;

namespace Slicer.State
{
    public class SlicerMoveFollowState : SlicerMainMotionState
    {
        private Vector3 m_lastFrameCursorPosition;

        private float m_originAngle;
        
        private bool m_rotationDirection
        {
            get => m_slicerInformation.GetRotationDirection;
            set => m_slicerInformation.GetRotationDirection = value;
        }
        
        # region GetProperty
        
        private int GetRotationThreshold => m_slicerInformation.GetRotationThreshold;
        
        private Vector2 GetOffSet => m_slicerInformation.GetSliceOffset;
        
        private Transform GetTransform => m_slicerInformation.GetTransform;
        
        private Transform GetPlayerTransform => m_slicerInformation.GetPlayerTransform;
        
        # endregion
        
        public SlicerMoveFollowState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            Debug.Log("移动");
            m_lastFrameCursorPosition = Input.mousePosition;
            m_originAngle = Mathf.Atan(GetOffSet.y / GetOffSet.x) * 180 / Mathf.PI;
        }

        public override void Motion(BaseInformation information)    
        {
            if (GetPlayerTransform.rotation.eulerAngles.z != 0)
            {
                var eulerAngles = GetPlayerTransform.eulerAngles;
                var distance = Mathf.Sqrt(Mathf.Pow(GetOffSet.x, 2) + Mathf.Pow(GetOffSet.y, 2));
                
                GetTransform.position = GetPlayerTransform.position + new Vector3(
                    distance * (m_rotationDirection ? 1 : -1) * Mathf.Cos((m_rotationDirection ? m_originAngle + eulerAngles.z : m_originAngle - eulerAngles.z) * Mathf.PI / 180)
                    , distance * Mathf.Sin((m_rotationDirection ? m_originAngle + eulerAngles.z : m_originAngle - eulerAngles.z) * Mathf.PI / 180)
                    , 0);
                Debug.Log((m_originAngle + eulerAngles.z));
            }
            else
            {
                GetTransform.position = GetPlayerTransform.position + new Vector3(GetOffSet.x * (m_rotationDirection ? 1 : -1), GetOffSet.y, 0);
            }
            
            GetTransform.eulerAngles = GetPlayerTransform.eulerAngles;
            
            Vector3 cursorPosition = Input.mousePosition;
            
            if (cursorPosition.x - m_lastFrameCursorPosition.x > GetRotationThreshold && !m_rotationDirection)
            {
                // 向右
                ChangeMotionState(MOTIONSTATEENUM.SlicerRotationFollowState);
                m_rotationDirection = true;
            } 
            else if (cursorPosition.x - m_lastFrameCursorPosition.x < -GetRotationThreshold && m_rotationDirection)
            {
                // 向左
                ChangeMotionState(MOTIONSTATEENUM.SlicerRotationFollowState);
                m_rotationDirection = false;
            }
            
            m_lastFrameCursorPosition = cursorPosition;
        }
    }
}