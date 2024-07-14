using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public class CameraMoveState : AdditiveState
    {
        private readonly Vector3 m_originMousePosition;

        public CameraMoveState(Information information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_originMousePosition = MouseWorldPoint;
        }

        private Transform GetTransform => Camera.main.transform;

        private Vector3 MouseWorldPoint => m_information.CameraManager.MouseWorldPosition;

        public override void Motion(Information information)
        {
            /*
                if (m_information.InputManager.GetMouseMiddleButtonUp)
                {
                    RemoveState();
                    return;
                }
    */
            var different = m_originMousePosition - MouseWorldPoint;

            GetTransform.position += different;
        }
    }
}