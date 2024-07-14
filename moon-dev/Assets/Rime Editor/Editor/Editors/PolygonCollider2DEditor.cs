using System;
using UnityEditor;
using UnityEngine;

namespace RimeEditor.Editor
{
    [CustomEditor(typeof(PolygonCollider2D))]
    public class PolygonCollider2DEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor defaultEditor;

        private void OnEnable()
        {
            defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.PolygonCollider2DEditor, UnityEditor"));
        }

        private void OnDisable()
        {
            DestroyImmediate(defaultEditor);
        }

        public override void OnInspectorGUI()
        {
            if (target == null) return;

            defaultEditor.OnInspectorGUI();
            DrawDefaultInspector();

            var collider = (PolygonCollider2D)target;

            if (GUILayout.Button("CreateColliderAndSetLayer")) CreateColliderAndSetLayer(collider);

            if (GUILayout.Button("CreateRigidbody")) CreateRigidbody();
        }

        private void CreateColliderAndSetLayer(PolygonCollider2D collider)
        {
            throw new Exception("The methods do not exist!");

            // TODO:Fix compilation errors
            //  target.GetComponent<MeshFilter>().sharedMesh.CreatePolygonCollider(collider);
            //  target.GameObject().layer = GlobalSetting.LayerMasks.GROUND;
            Undo.RegisterCreatedObjectUndo(target, "CreateColliderAndSetLayer " + target.name);
        }

        private void CreateRigidbody()
        {
            throw new Exception("The methods do not exist!");

            // var rigidbodyParentPrefab = Explorer.TryGetSetting<PrefabFactory>().RIGIDBODY_PARENT;
            // var rigidbodyParent = Instantiate(rigidbodyParentPrefab);

            // TODO:Fix compilation errors
            // rigidbodyParent.transform.parent = target.GameObject().transform.parent;
            // target.GameObject().transform.parent = rigidbodyParent.transform;
            // rigidbodyParent.name = rigidbodyParentPrefab.name + Random.Range(10000, 1000000);
            //
            // if (!target.name.Contains(GlobalSetting.ObjNameTag.RIGIDBODY_TAG))
            // {
            //     target.name += GlobalSetting.ObjNameTag.RIGIDBODY_TAG;
            // }
            //
            // Undo.RegisterCreatedObjectUndo(target, "CreateRigidbody " + target.name);
        }
    }
}