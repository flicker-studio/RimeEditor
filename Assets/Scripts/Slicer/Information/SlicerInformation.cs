using System.Collections.Generic;
using UnityEngine;

public class SlicerInformation : BaseInformation
{
    private SlicerProperty m_slicerProperty;

    private PrefabFactory m_prefabFactory;

    private Material m_cutMaterial;

    private Transform m_transform;

    public List<Collider2D> TargetList = new List<Collider2D>();

    public List<GameObject> ParentList = new List<GameObject>();
    
    public GameObject GetProductPrefab => m_prefabFactory.SLICE_OBJ;

    public GameObject GetCombinationRigidbodyParentPrefab => m_prefabFactory.COMBINATION_COLLIDES_RIGIDBODY_PARENT;
    
    public GameObject GetCombinationNotRigidbodyParentPrefab => m_prefabFactory.COMBINATION_COLLIDES_NOT_RIGIDBODY_PARENT;
    
    public GameObject GetRigidbodyParentPrefab => m_prefabFactory.RIGIDBODY_PARENT;

    public Material GetCutSurfaceMaterial => m_cutMaterial;

    public Transform GetTransform => m_transform;

    public Vector3 GetDetectionRange => m_slicerProperty.SlicerSize.RANGE_OF_DETECTION;

    public Vector3 GetDetectionCompensationScale => m_slicerProperty.ConnectedObject.DETECTION_COMPENSATIONS_SCALE;

    public float GetSliceThinckNess => m_slicerProperty.SlicerSize.SLICE_THICKNESS;

    public float GetSliceSpacingCompensation => m_slicerProperty.SlicerSize.SLICE_SPACING_COMPENSATION;

    public bool GetNum1Down => InputManager.Instance.GetDebuggerNum1Down;

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
}
