using System;
using System.IO;
using Data.ScriptableObject;
using Frame.Static.Global;
using UnityEditor;
using UnityEngine;
using Random = UnityEngine.Random;

namespace WindowExtension
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
            if (target == null)
            {
                return;
            }

            defaultEditor.OnInspectorGUI();
            DrawDefaultInspector();

            var collider = (PolygonCollider2D)target;

            if (GUILayout.Button("CreateColliderAndSetLayer"))
            {
                CreateColliderAndSetLayer(collider);
            }

            if (GUILayout.Button("CreateRigidbody"))
            {
                CreateRigidbody();
            }
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

            var rigidbodyParentPrefab = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory").RIGIDBODY_PARENT;
            var rigidbodyParent = Instantiate(rigidbodyParentPrefab);

            // TODO:Fix compilation errors
            // rigidbodyParent.transform.parent = target.GameObject().transform.parent;
            // target.GameObject().transform.parent = rigidbodyParent.transform;
            rigidbodyParent.name = rigidbodyParentPrefab.name + Random.Range(10000, 1000000);

            if (!target.name.Contains(GlobalSetting.ObjNameTag.RIGIDBODY_TAG))
            {
                target.name += GlobalSetting.ObjNameTag.RIGIDBODY_TAG;
            }

            Undo.RegisterCreatedObjectUndo(target, "CreateRigidbody " + target.name);
        }
    }

    [CustomEditor(typeof(RectTransform), true)]
    public class CustomRectTransformEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor defaultEditor;

        private void OnEnable()
        {
            defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.RectTransformEditor, UnityEditor"));
        }

        public override void OnInspectorGUI()
        {
            if (target == null)
            {
                return;
            }

            defaultEditor.OnInspectorGUI();

            if (GUILayout.Button("Copy Path"))
            {
                var rectTransform = (RectTransform)target;
                var path = GeneratePath(rectTransform);
                EditorGUIUtility.systemCopyBuffer = path;

                EditorWindow.focusedWindow.ShowNotification
                    (new GUIContent("Copied: " + path));
            }
        }

        private string GeneratePath(Transform transform)
        {
            var path = transform.name;

            while (transform.parent != null && transform.parent.GetComponent<RectTransform>() != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }

            var firstSlash = path.IndexOf('/');

            if (firstSlash >= 0)
            {
                path = path.Substring(firstSlash + 1);
            }

            return path;
        }

        private void OnDisable()
        {
            DestroyImmediate(defaultEditor);
        }
    }

    [CustomEditor(typeof(ItemProduct))]
    public class ItemProductEditor : UnityEditor.Editor
    {
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var itemProduct = (ItemProduct)target;

            if (GUILayout.Button("Set Prefab Name"))
            {
                if (itemProduct.ItemObject == null)
                {
                    return;
                }

                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(itemProduct.ItemObject)
                    , itemProduct.Name);

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Generate Image"))
            {
                if (itemProduct.ItemObject == null)
                {
                    return;
                }

                if (itemProduct.ItemIcon != null)
                {
                    if (itemProduct.ItemIcon.name != itemProduct.Name)
                    {
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(itemProduct.ItemIcon));
                    }
                    else
                    {
                        return;
                    }
                }

                var preview = AssetPreview.GetAssetPreview(itemProduct.ItemObject);

                var bytes = preview.EncodeToPNG();

                var typeDirectory = Enum.GetName(typeof(ITEMTYPEENUM), itemProduct.ItemType);

                var path = Application.dataPath +
                           $"/Resources/Items/Images/{typeDirectory}/{itemProduct.Name}" + ".png";

                Directory.CreateDirectory(Path.GetDirectoryName(path));

                File.WriteAllBytes(path, bytes);

                AssetDatabase.Refresh();

                EditorApplication.update += WaitForTextureImport;

                void WaitForTextureImport()
                {
                    var sprite = Resources.Load<Sprite>($"Items/Images/{typeDirectory}/{itemProduct.Name}");

                    if (sprite != null)
                    {
                        itemProduct.ItemIcon = sprite;
                        EditorApplication.update -= WaitForTextureImport;
                    }
                }
            }
        }
    }
}