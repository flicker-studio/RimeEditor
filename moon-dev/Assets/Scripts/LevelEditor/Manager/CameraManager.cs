using Frame.Static.Global;
using Moon.Kernel;
using Moon.Kernel.Extension;
using Moon.Kernel.Utils;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class CameraManager : IManager
    {
        public CameraProperty GetProperty { get; }

        public OutlinePainter GetOutlinePainter
        {
            get
            {
                if (m_outlinePainter == null)
                {
                    m_outlinePainter = new OutlinePainter();
                    m_outlinePainter.OutlineMode = GetProperty.GetOutlineProperty.OUTLINE_MODE;
                    m_outlinePainter.OutlineColor = GetProperty.GetOutlineProperty.OUTLINE_COLOR;
                    ;
                    m_outlinePainter.OutlineWidth = GetProperty.GetOutlineProperty.OUTLINE_WIDTH;
                    ;
                }

                return m_outlinePainter;
            }
        }

        public Vector3 GetMousePosition => Mouse.current.position.ReadValue();

        public Vector2 GetScreenScale => new(GlobalSetting.ScreenInfo.REFERENCE_RESOLUTION.x / Screen.width,
            GlobalSetting.ScreenInfo.REFERENCE_RESOLUTION.y / Screen.height);

        public Vector3 GetMouseWorldPoint =>
            Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Mathf.Abs(Camera.main.transform.position.z)));

        public Vector3 GetScreenWorldPoint(Vector3 sreenPoint)
        {
            return Camera.main.ScreenToWorldPoint(sreenPoint.NewZ(Mathf.Abs(Camera.main.transform.position.z)));
        }

        private OutlinePainter m_outlinePainter;

        public CameraManager()
        {
            GetProperty = Explorer.TryGetSetting<CameraProperty>();
        }
    }
}