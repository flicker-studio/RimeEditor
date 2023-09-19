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
    }

    void CreateColliderAndSetLayer(PolygonCollider2D collider)
    {
        target.GetComponent<MeshFilter>().sharedMesh.CreatePolygonCollider(collider);
        target.GameObject().layer = GlobalSetting.LayerMasks.GROUND;
    }
}