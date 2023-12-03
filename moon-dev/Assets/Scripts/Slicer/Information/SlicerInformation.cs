using System.Collections.Generic;
using System.Linq;
using Data.ScriptableObject;
using Frame.StateMachine;
using Frame.Static.Extensions;
using Frame.Static.Global;
using Frame.Tool;
using Frame.Tool.Pool;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Slicer
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

        public Vector2 GetSliceOffset => m_slicerProperty.SlicerMotion.SLICE_OFFSET;
        
        
        public Vector3 GetMousePosition => Mouse.current.position.ReadValue();
        public Vector3 GetMouseWorldPoint =>
            Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Mathf.Abs(Camera.main.transform.position.z)));
        
        public Vector3 GetDetectionRange => m_slicerProperty.SlicerSize.RANGE_OF_DETECTION;
    
        public Vector3 GetDetectionCompensationScale => m_slicerProperty.ConnectedObject.DETECTION_COMPENSATIONS_SCALE;

        public float GetRotationSpeed => m_slicerProperty.SlicerMotion.ROTATION_SPEED;
        
        public float GetSliceThinckNess => m_slicerProperty.SlicerSize.SLICE_THICKNESS;
    
        public float GetSliceSpacingCompensation => m_slicerProperty.SlicerSize.SLICE_SPACING_COMPENSATION;

        public int GetRotationThreshold =>  m_slicerProperty.SlicerMotion.ROTATION_THRESHOLD;
        
        public bool GetMouseLeftButtonDown => InputManager.Instance.GetMouseLeftButtonDown;

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
        public SlicerInformation(Transform transform)
        {
            m_slicerProperty = Resources.Load<SlicerProperty>("GlobalSettings/SlicerProperty");
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_cutMaterial = Resources.Load<Material>("Materials/Test");
            m_transform = transform;
        }
        
        public SlicerInformation(Transform transform, Transform playerTransform)
        {
            m_slicerProperty = Resources.Load<SlicerProperty>("GlobalSettings/SlicerProperty");
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_cutMaterial = Resources.Load<Material>("Materials/Test");
            m_transform = transform;
            m_playerTransform = playerTransform;
        }

        public void ResetCopy()
        {
            List<List<Collider2D>> colliderListGroup = TargetList.CheckColliderConnectivity(
                GetDetectionCompensationScale
                , GlobalSetting.LayerMasks.GROUND);

            foreach (var collider in TargetList)
            {
                ObjectPool.Instance.OnRelease(collider.gameObject);
            }

            ObjectPool.Instance.OnReleaseAll(GetCombinationRigidbodyParentPrefab);

            ObjectPool.Instance.OnReleaseAll(GetCombinationNotRigidbodyParentPrefab);

            List<Collider2D> tempList = new List<Collider2D>();

            foreach (var colliderList in colliderListGroup)
            {
                tempList.AddRange(colliderList);
            }

            tempList = tempList.Distinct().ToList();

            foreach (var collider in TargetList)
            {
                tempList.Remove(collider);
            }
        }
    }

}