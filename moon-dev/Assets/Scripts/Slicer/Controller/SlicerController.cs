using Frame.StateMachine;
using Slicer.Information;
using UnityEngine;

namespace Slicer.Controller
{
    public class SlicerController : MonoBehaviour
    {
        private MotionController m_motionController;
    
        private SlicerInformation m_slicerInformation;
        
    
        void ControllerInit()
        {
            m_slicerInformation = new SlicerInformation(transform);
            m_motionController = new MotionController(m_slicerInformation);
        }
        
        private void MotionInit()
        {
            m_motionController.ChangeMotionState(MOTIONSTATEENUM.SlicerCloseState);
        }
        
        private void Start()
        {
            ControllerInit();
            MotionInit();
        }
    
        private void FixedUpdate()
        {
            m_motionController.Motion(m_slicerInformation);
        }
    
    #if UNITY_EDITOR
        void OnDrawGizmos()
        {
            if(m_slicerInformation == null) m_slicerInformation = new SlicerInformation(transform);
            Gizmos.color = new Color(0, 1, 0, 0.5f);
            Matrix4x4 oldGizmosMatrix = Gizmos.matrix;
            Gizmos.matrix = Matrix4x4.TRS(transform.position,transform.rotation, m_slicerInformation.GetDetectionRange);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            Gizmos.color = new Color(1f, 0, 0f, 0.5f);
            (Vector3 pos, Vector3 size, Quaternion rot) = m_slicerInformation.GetSliceLeftData();
            Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            
            (pos, size, rot) = m_slicerInformation.GetSliceUpData();
            Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            
            (pos, size, rot) = m_slicerInformation.GetSliceRightData();
            Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            
            (pos, size, rot) = m_slicerInformation.GetSliceDownData();
            Gizmos.matrix = Matrix4x4.TRS(pos,rot, size);
            Gizmos.DrawCube(Vector3.zero, Vector3.one);
            
            Gizmos.matrix = oldGizmosMatrix;
        }
    #endif
    }
}
