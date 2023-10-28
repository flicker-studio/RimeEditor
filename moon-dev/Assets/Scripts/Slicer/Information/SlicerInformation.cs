using System.Collections.Generic;
using Data.ScriptableObject;
using Frame.StateMachine;
using Frame.Static.Extensions;
using Frame.Tool;
using Slicer.Controller;
using Slicer.Data;
using UnityEngine;

namespace Slicer.Information
{
    public class SlicerInformation : BaseInformation
    {
        private SlicerProperty m_slicerProperty;
    
        private PrefabFactory m_prefabFactory;
    
        private Material m_cutMaterial;
    
        private Transform m_transform;

        private Transform m_playerTransform;

        public List<Collider2D> TargetList = new List<Collider2D>();
        
        public PrefabFactory GetPrefabFactory => m_prefabFactory;
        
        public GameObject GetProductPrefab => m_prefabFactory.SLICE_OBJ;
    
        public GameObject GetCombinationRigidbodyParentPrefab => m_prefabFactory.COMBINATION_COLLIDES_RIGIDBODY_PARENT;
        
        public GameObject GetCombinationNotRigidbodyParentPrefab => m_prefabFactory.COMBINATION_COLLIDES_NOT_RIGIDBODY_PARENT;
        
        public GameObject GetRigidbodyParentPrefab => m_prefabFactory.RIGIDBODY_PARENT;
    
        public Material GetCutSurfaceMaterial => m_cutMaterial;
    
        public Transform GetTransform => m_transform;
        
        public Transform GetPlayerTransform => m_playerTransform;
    
        public Vector3 GetDetectionRange => m_slicerProperty.SlicerSize.RANGE_OF_DETECTION;
    
        public Vector3 GetDetectionCompensationScale => m_slicerProperty.ConnectedObject.DETECTION_COMPENSATIONS_SCALE;

        public Vector3 GetMousePosition => Input.mousePosition;

        public Vector2 GetSliceOffset => m_slicerProperty.SlicerMotion.SLICE_OFFSET;

        public int GetRotationThreshold => m_slicerProperty.SlicerMotion.ROTATION_THRESHOLD;
        
        public float GetRotationSpeed => m_slicerProperty.SlicerMotion.ROTATION_SPEED;
        
        public float GetSliceThinckNess => m_slicerProperty.SlicerSize.SLICE_THICKNESS;
    
        public float GetSliceSpacingCompensation => m_slicerProperty.SlicerSize.SLICE_SPACING_COMPENSATION;
    
        public bool GetNum1Down => InputManager.Instance.GetDebuggerNum1Down;

        public bool GetRotationDirection { get; set; } = true;

        public (Vector3,Vector3,Quaternion) GetSliceLeftData()
        {
            return m_slicerProperty.GetSliceLeftData(m_transform);
        }
        
        public (Vector3,Vector3,Quaternion) GetSliceUpData()
        {
            return m_slicerProperty.GetSliceUpData(m_transform);
        }
        
        public (Vector3,Vector3,Quaternion) GetSliceRightData()
        {
            return m_slicerProperty.GetSliceRightData(m_transform);
        }
        
        public (Vector3,Vector3,Quaternion) GetSliceDownData()
        {
            return m_slicerProperty.GetSliceDownData(m_transform);
        }
        public SlicerInformation(Transform transform, Transform playerTransform)
        {
            m_slicerProperty = Resources.Load<SlicerProperty>("GlobalSettings/SlicerProperty");
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_cutMaterial = Resources.Load<Material>("Materials/Test");
            m_transform = transform;
            m_playerTransform = playerTransform;
        }
    }

}