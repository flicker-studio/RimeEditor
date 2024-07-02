using Frame.StateMachine;
using Moon.Kernel.Extension;
using UnityEngine;

namespace Slicer.State
{
    public class SlicerMoveFollowState : SlicerMainMotionState
    {
        # region GetProperty

        private Vector3 GetMouseWorldPoint => m_slicerInformation.GetMouseWorldPoint;

        private Vector2 GetOffSet => m_slicerInformation.GetSliceOffset;

        private Transform GetTransform => m_slicerInformation.GetTransform;

        private Transform GetPlayerTransform => m_slicerInformation.GetPlayerTransform;

        # endregion

        public SlicerMoveFollowState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            Vector3 dir = GetMouseWorldPoint - GetPlayerTransform.position;

            float angle = Vector3.SignedAngle(Vector3.right, dir, Vector3.forward);

            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

            Vector3 currentOffset = rotation * GetOffSet;

            if (Vector3.Dot(dir, GetPlayerTransform.right) < 0)
            {
                currentOffset = rotation * GetOffSet.NewY(-GetOffSet.y);
                GetTransform.localScale = GetTransform.localScale.NewY(-1);
            }
            else
            {
                GetTransform.localScale = GetTransform.localScale.NewY(1);
            }

            GetTransform.position = GetPlayerTransform.position + currentOffset;

            GetTransform.rotation = rotation;
        }
    }
}