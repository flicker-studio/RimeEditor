using System.Collections;
using Frame.StateMachine;
using UnityEngine;

namespace Slicer.State
{
    public class SlicerRotationFollowState : SlicerMainMotionState
    {
        private Vector2 m_playerOffset;

        private Vector2 m_oldPlayerPos;
        
        # region GetProperty

        private float GetRotationSpeed => m_slicerInformation.GetRotationSpeed;
        
        private Transform GetTransform => m_slicerInformation.GetTransform;

        private Transform GetPlayerTransform => m_slicerInformation.GetPlayerTransform;
        
        # endregion
        
        public SlicerRotationFollowState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            Debug.Log("旋转");
            m_oldPlayerPos = GetPlayerTransform.position;
        }

        public override void Motion(BaseInformation information)
        {
            if (Mathf.Abs(Mathf.Abs(GetTransform.rotation.y) - 1f) < .1f)
            {
                ChangeMotionState(MOTIONSTATEENUM.SlicerMoveFollowState);
            }
            
            // 移动期间角色偏移
            var position = GetPlayerTransform.position;
            m_playerOffset.x = position.x - m_oldPlayerPos.x;
            m_playerOffset.y = position.y - m_oldPlayerPos.y;
            
            var positionSlice = GetTransform.position;
            positionSlice = new Vector3(positionSlice.x + m_playerOffset.x, positionSlice.y + m_playerOffset.y, positionSlice.z);
            GetTransform.position = positionSlice;

            GetTransform.RotateAround(position, GetPlayerTransform.up, GetRotationSpeed);

            m_oldPlayerPos.x = position.x;
            m_oldPlayerPos.y = position.y;
        }
    }
}