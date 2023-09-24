using Frame.StateMachine;
using UnityEngine;

namespace Slicer.State
{
    public class SlicerMoveFollowState : SlicerMainMotionState
    {
        public SlicerMoveFollowState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            
        }

        public override void Motion(BaseInformation information)
        {
            if (m_slicerInformation.GetNum1Down)
            {
                Debug.Log(1);
            } 
        }
    }
}