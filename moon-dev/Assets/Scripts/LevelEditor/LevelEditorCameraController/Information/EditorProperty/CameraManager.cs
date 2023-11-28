using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class CameraManager
    {
        public CameraProperty GetProperty => m_cameraProperty;
        public OutlinePainter GetOutlinePainter
        {
            get
            {
                if (m_outlinePainter == null)
                {
                    m_outlinePainter = new OutlinePainter();
                    m_outlinePainter.OutlineMode = GetProperty.GetOutlineProperty.OUTLINE_MODE;
                    m_outlinePainter.OutlineColor = GetProperty.GetOutlineProperty.OUTLINE_COLOR;;
                    m_outlinePainter.OutlineWidth = GetProperty.GetOutlineProperty.OUTLINE_WIDTH;;
                }

                return m_outlinePainter;
            }
        }
        
        public Vector3 GetMousePosition => Mouse.current.position.ReadValue();
        
        public Vector3 GetMouseWorldPoint =>
            Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Mathf.Abs(Camera.main.transform.position.z)));

        public Vector3 GetScreenWorldPoint(Vector3 sreenPoint)
        {
            return Camera.main.ScreenToWorldPoint(sreenPoint.NewZ(Mathf.Abs(Camera.main.transform.position.z)));
        }
        
        private OutlinePainter m_outlinePainter;

        private CameraProperty m_cameraProperty;

        public CameraManager()
        {
            m_cameraProperty =
                Resources.Load<CameraProperty>("GlobalSettings/LevelEditorCameraProperty");
        }
    }
}
