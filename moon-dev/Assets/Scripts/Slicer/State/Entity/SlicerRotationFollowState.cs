using System.Collections;
using Frame.StateMachine;
using UnityEngine;

namespace Slicer.State
{
    public class SlicerRotationFollowState : SlicerMainMotionState
    {
        private Quaternion m_originRotation;

        private float m_leanedAngle;
        
        private float m_timeCount;
        
        # region GetProperty

        private bool GetDirection
        {
            get => m_slicerInformation.GetRotationDirection;
            set => m_slicerInformation.GetRotationDirection = value;
        }
        
        private float GetRotationSpeed => m_slicerInformation.GetRotationSpeed;
        
        private Transform GetTransform => m_slicerInformation.GetTransform;

        private Transform GetPlayerTransform => m_slicerInformation.GetPlayerTransform;

        private Vector2 GetSliceOffset => m_slicerInformation.GetSliceOffset;
        
        # endregion
        
        public SlicerRotationFollowState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_originRotation = GetTransform.rotation;
            GetDirection = !GetDirection;
        }

        public override void Motion(BaseInformation information)
        {
            // 初始角度和移动中角度改变的差值
            m_leanedAngle = m_originRotation.eulerAngles.z - GetPlayerTransform.rotation.eulerAngles.z;
            GetTransform.rotation = Quaternion.Lerp(m_originRotation,
                m_originRotation * Quaternion.Euler(0, 180, m_leanedAngle), m_timeCount * GetRotationSpeed);
            var newPos = GetDirection
                ? GetPlayerTransform.position - GetTransform.right * GetSliceOffset.x
                : GetPlayerTransform.position + GetTransform.right * GetSliceOffset.x;
                
            GetTransform.position = newPos + GetTransform.up * GetSliceOffset.y;
    
            if (m_timeCount >= 1)
            {
                ChangeMotionState(typeof(SlicerMoveFollowState));
                return;
            }
                
            m_timeCount += Time.deltaTime;
        }
    }
}