using System.Collections;
using System.Collections.Generic;
using Character.Information;
using Frame.StateMachine;
using UnityEngine;

namespace Character.State
{
    public abstract class PlayerAdditiveMotionState : AdditiveMotionState
    {
        protected PlayerInformation m_playerInformation;
        public PlayerAdditiveMotionState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_playerInformation = information as PlayerInformation;
        }
    }
}
