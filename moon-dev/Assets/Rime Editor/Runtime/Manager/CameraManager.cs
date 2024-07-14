using System;
using Cysharp.Threading.Tasks;
using LevelEditor.Extension;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    ///     Camera management class
    /// </summary>
    public sealed class CameraManager : IManager
    {
        private readonly CameraSetting m_cameraSetting;

        public CameraManager(CameraSetting cameraSetting)
        {
            m_cameraSetting = cameraSetting;
        }

        /// <summary>
        ///     The rate of change in the Z distance of the camera
        /// </summary>
        public float CameraZChangeSpeed => m_cameraSetting.GetCameraMotionProperty.CAMERA_Z_CHANGE_SPEED;

        /// <summary>
        ///     The minimum distance in the Z direction of the camera
        /// </summary>
        public float CameraZMin => m_cameraSetting.GetCameraMotionProperty.CAMERA_MIN_Z;

        /// <summary>
        ///     The maximum distance of the camera in the Z direction
        /// </summary>
        public float CameraZMax => m_cameraSetting.GetCameraMotionProperty.CAMERA_MAX_Z;

        /// <summary>
        ///     The pointer that was added or used last by the user or null if there is no pointer device connected to the system
        /// </summary>
        public Vector3 MousePosition => Mouse.current.position.ReadValue();

        /// <summary>
        ///     Gets the current screen zoom
        /// </summary>
        public Vector2 ScreenScale => throw
            //TODO:需加载SO
            new Exception("需加载SO");

        // var x = GlobalSetting.ScreenInfo.REFERENCE_RESOLUTION.x / Screen.width;
        // var y = GlobalSetting.ScreenInfo.REFERENCE_RESOLUTION.y / Screen.height;
        // return new Vector2(x, y);
        /// <summary>
        ///     Gets the world position of the current mouse
        /// </summary>
        /// <exception cref="NullReferenceException">Unable to get the main camera</exception>
        public Vector3 MouseWorldPosition
        {
            get
            {
                var camera = Camera.main;

                if (camera == null) throw new NullReferenceException("The main camera could not be obtained!");

                var newPos = MousePosition.NewZ(Mathf.Abs(camera.transform.position.z));

                return camera.ScreenToWorldPoint(newPos);
            }
        }

        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }

        /// <summary>
        ///     Converts a point on the screen to a world space
        /// </summary>
        /// <param name="screenPosition">Screen position</param>
        /// <exception cref="NullReferenceException">Unable to get the main camera</exception>
        public Vector3 ScreenToWorldPoint(Vector3 screenPosition)
        {
            var camera = Camera.main;

            if (camera == null) throw new NullReferenceException("The main camera could not be obtained!");

            var abs       = Mathf.Abs(camera.transform.position.z);
            var screenPos = screenPosition.NewZ(abs);
            var newPos    = camera.ScreenToWorldPoint(screenPos);
            return newPos;
        }
    }
}