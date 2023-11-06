using System;
using System.Collections;
using System.Collections.Generic;
using Frame.Tool;
using UnityEngine;

namespace Frame.StateMachine
{
    public class AdditiveMotionStateMachine : MotionStateMachine
    {
        public override void ChangeMotionState(Type motionStateType,BaseInformation information)
        {
            if (motionStateType == typeof(NoneMotionState))
            {
                List<MotionState> tempList = new List<MotionState>();
                tempList.AddRange(m_motionStates);
                foreach (var state in tempList)
                {
                    if ((state as AdditiveMotionState).IsEnd)
                    {
                        m_motionStates.Remove(state);
                    }
                }
                return;
            }
            MotionState motionState = CreateMotionState(motionStateType, information);
            if(motionState == null) return;
            if (m_motionStates.Contains(motionState)) return;
            m_motionStates.Add(motionState);
        }


        public AdditiveMotionStateMachine(MotionCallBack motionCallBack): base(motionCallBack)
        {
            
        }
    }

}