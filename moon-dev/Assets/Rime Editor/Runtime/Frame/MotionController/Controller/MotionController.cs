using System;
using System.Collections.Generic;
using System.Linq;
using LevelEditor;

namespace Frame.StateMachine
{
    public class MotionController
    {
        private readonly Information m_information;

        private readonly MotionCallBack           m_motionCallBack;
        private readonly List<MotionStateMachine> m_motionStateMachines;

        public MotionController(Information information)
        {
            m_motionStateMachines = new List<MotionStateMachine>();
            m_information         = information;
            m_motionCallBack = new MotionCallBack
                               {
                                   CheckGlobalStatesCallBack = CheckGlobalStates,
                                   ChangeMotionStateCallBack = ChangeMotionState
                               };
        }

        public void Motion(Information Information)
        {
            var tempList = new List<MotionStateMachine>();
            tempList.AddRange(m_motionStateMachines);
            foreach (var motionStateMachine in tempList) motionStateMachine.Motion(Information);
        }

        public void ChangeMotionState(Type motionStateType)
        {
            if (motionStateType.IsSubclassOf(typeof(MainMotionState)))
                ChangeMotionStateInMainMachine(motionStateType);
            else
                ChangeMotionStateInAdditiveMachine(motionStateType);
        }

        private void ChangeMotionStateInMainMachine(Type motionStateType)
        {
            var motionMachine = m_motionStateMachines.FirstOrDefault(state => state is MainMotionStateMachine);
            if (motionMachine == null)
            {
                motionMachine = new MainMotionStateMachine(m_motionCallBack);
                m_motionStateMachines.Add(motionMachine);
            }

            motionMachine.ChangeMotionState(motionStateType, m_information);
        }

        private void ChangeMotionStateInAdditiveMachine(Type motionStateType)
        {
            var motionMachine = m_motionStateMachines.FirstOrDefault(state => state is AdditiveMotionStateMachine);
            if (motionMachine == null)
            {
                motionMachine = new AdditiveMotionStateMachine(m_motionCallBack);
                m_motionStateMachines.Add(motionMachine);
            }

            motionMachine.ChangeMotionState(motionStateType, m_information);
        }

        private List<Type> CheckGlobalStates()
        {
            var tempList = new List<Type>();
            foreach (var motionStateMachine in m_motionStateMachines) tempList.AddRange(motionStateMachine.CheckStates());

            return tempList;
        }
    }
}