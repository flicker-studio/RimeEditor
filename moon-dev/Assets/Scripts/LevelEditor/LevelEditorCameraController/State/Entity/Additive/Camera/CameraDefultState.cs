using Frame.StateMachine;
using UnityEngine.EventSystems;

namespace LevelEditor
{
    public class CameraDefultState : AdditiveState
    {
        private bool GetMouseMiddleButtonDown => m_information.GetInput.GetMouseMiddleButtonDown;

        private bool GetMouseSrollDown => m_information.GetInput.GetMouseSrollDown;
        public CameraDefultState(BaseInformation baseInformation, MotionCallBack motionCallBack) : base(baseInformation, motionCallBack)
        {
        }

        public override void Motion(BaseInformation information)
        {
            CheckCameraInput();
        }
    
        private void CheckCameraInput()
        {
            if (GetMouseMiddleButtonDown)
            {
                if(CheckStates.Contains(typeof(CameraMoveState))) return;
                if(EventSystem.current.IsPointerOverGameObject()) return;
                ChangeMotionState(typeof(CameraMoveState));
            }

            if (GetMouseSrollDown)
            {
                if(CheckStates.Contains(typeof(CameraChangeZState))) return;
                if(EventSystem.current.IsPointerOverGameObject()) return;
                ChangeMotionState(typeof(CameraChangeZState));
            }
        }
    }

}