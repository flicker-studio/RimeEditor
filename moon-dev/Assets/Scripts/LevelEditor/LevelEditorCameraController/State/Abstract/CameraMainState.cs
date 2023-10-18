using System.Collections;
using System.Collections.Generic;
using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public abstract class CameraMainState : MainMotionState
    {
        protected Information m_information;
    
        protected CameraMainState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_information = information as Information;
        }
    }
}
