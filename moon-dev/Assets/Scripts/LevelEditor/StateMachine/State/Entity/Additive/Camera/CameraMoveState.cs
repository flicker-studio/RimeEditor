using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public class CameraMoveState : AdditiveState
    {
        private Vector3 m_originMousePosition;

        private Transform GetTransform => Camera.main.transform;

        private Vector3 MouseWorldPoint => m_information.CameraManager.MouseWorldPosition;

        public CameraMoveState(BaseInformation information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_originMousePosition = MouseWorldPoint;
        }

        public override void Motion(BaseInformation information)
        {
            if (m_information.InputManager.GetMouseMiddleButtonUp)
            {
                RemoveState();
                return;
            }

            Vector3 different = m_originMousePosition - MouseWorldPoint;

            GetTransform.position += different;
        }
    }
}