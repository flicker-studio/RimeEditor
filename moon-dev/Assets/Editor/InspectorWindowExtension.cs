using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PolygonCollider2D))]
public class PolygonCollider2DEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector(); 

        PolygonCollider2D collider = (PolygonCollider2D)target;
        if(GUILayout.Button("CreateColliderAndSetLayer")) 
        {
            CreateColliderAndSetLayer(collider); 
        }
        if(GUILayout.Button("CreateRigidbody")) 
        {
            CreateRigidbody(); 
        }
    }

    void CreateColliderAndSetLayer(PolygonCollider2D collider)
    {
        target.GetComponent<MeshFilter>().sharedMesh.CreatePolygonCollider(collider);
        target.GameObject().layer = GlobalSetting.LayerMasks.GROUND;
        Undo.RegisterCreatedObjectUndo(target, "CreateColliderAndSetLayer " + target.name);
    }

    void CreateRigidbody()
    {
        GameObject rigidbodyParentPrefab = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory").RIGIDBODY_PARENT;
        GameObject rigidbodyParent = GameObject.Instantiate(rigidbodyParentPrefab);
        rigidbodyParent.transform.parent = target.GameObject().transform.parent;
        target.GameObject().transform.parent = rigidbodyParent.transform;
        rigidbodyParent.name = rigidbodyParentPrefab.name + Random.Range(10000,1000000);
        if (!target.name.Contains(GlobalSetting.ObjNameTag.rigidbodyTag))
        {
            target.name += GlobalSetting.ObjNameTag.rigidbodyTag;
        }
        Undo.RegisterCreatedObjectUndo(target, "CreateRigidbody " + target.name);
    }
}