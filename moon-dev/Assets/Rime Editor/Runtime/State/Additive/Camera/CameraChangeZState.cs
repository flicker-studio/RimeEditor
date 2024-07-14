using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public class CameraChangeZState : AdditiveState
    {
        private readonly Vector3 m_originMousePosition;

        private Vector3 m_currentMousePositon;

        public CameraChangeZState(Information information, MotionCallBack motionCallBack) : base(information, motionCallBack)
        {
            m_originMousePosition = GetMouseWorldPoint;
            ChangeZValue();
        } /*
        private float GetMouseScroll => m_information.InputManager.GetMouseSroll;

        private bool GetMouseScrollUp => m_information.InputManager.GetMouseSrollUp;
*/

        private Transform GetCameraTransform => Camera.main.transform;

        private float GetCameraMaxZ => m_information.CameraManager.CameraZMax;

        private float GetCameraMinZ => m_information.CameraManager.CameraZMin;

        private float GetCameraZChangeSpeed => m_information.CameraManager.CameraZChangeSpeed;

        private Vector3 GetMouseWorldPoint => m_information.CameraManager.MouseWorldPosition;

        public override void Motion(Information information)
        {
            /*
                if (GetMouseScrollUp)
                {
                    RemoveState();
                    return;
                }
    */
            ChangeZValue();
        }

        private void ChangeZValue()
        {
            /*
                if (GetMouseScroll < 0)
                {
                    if (GetCameraTransform.position.z < GetCameraMaxZ)
                    {
                        return;
                    }

                    GetCameraTransform.position = GetCameraTransform.position
                        .NewZ(GetCameraTransform.position.z - GetCameraZChangeSpeed);
                }

                if (GetMouseScroll > 0)
                {
                    if (GetCameraTransform.position.z > GetCameraMinZ)
                    {
                        return;
                    }

                    GetCameraTransform.position = GetCameraTransform.position
                        .NewZ(GetCameraTransform.position.z + GetCameraZChangeSpeed);
                }

                GetCameraTransform.position = GetCameraTransform.position
                    .NewZ(Mathf.Clamp(GetCameraTransform.position.z, GetCameraMaxZ, GetCameraMinZ));

                m_currentMousePositon = GetMouseWorldPoint;

                var moveDir = m_originMousePosition - m_currentMousePositon;
                GetCameraTransform.position += moveDir;
            */
        }
    }
}