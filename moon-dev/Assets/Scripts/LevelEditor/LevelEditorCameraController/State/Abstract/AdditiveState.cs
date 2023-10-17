using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public abstract class AdditiveState : AdditiveMotionState
    {
        protected Information m_information;
    
        protected AdditiveState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
            m_information = baseInformation as Information;
        }
    }

}