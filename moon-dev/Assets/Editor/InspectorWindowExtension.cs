using System;
using Data.ScriptableObject;
using Frame.Static.Global;
using Slicer;
using Unity.VisualScripting;
using UnityEngine;
using UnityEditor;
using System.IO;
using Random = UnityEngine.Random;

namespace WindowExtension
{
    [CustomEditor(typeof(PolygonCollider2D))]
    public class PolygonCollider2DEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor defaultEditor;
        
        void OnEnable()
        {
            defaultEditor = CreateEditor(targets, System.Type.GetType("UnityEditor.PolygonCollider2DEditor, UnityEditor"));
        }
        
        void OnDisable()
        {
            DestroyImmediate(defaultEditor);
        }
        public override void OnInspectorGUI()
        {
            if(target == null) return;
            defaultEditor.OnInspectorGUI();
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

        void OnEnable()
        {
            defaultEditor = CreateEditor(targets, System.Type.GetType("UnityEditor.RectTransformEditor, UnityEditor"));
        }

        public override void OnInspectorGUI()
        {
            if(target == null) return;
            defaultEditor.OnInspectorGUI();

            if (GUILayout.Button("Copy Path"))
            {
                RectTransform rectTransform = (RectTransform)target;
                string path = GeneratePath(rectTransform);
                EditorGUIUtility.systemCopyBuffer = path;
                EditorWindow.focusedWindow.ShowNotification
                    (new GUIContent("Copied: " + path));
            }
        }

        string GeneratePath(Transform transform)
        {
            string path = transform.name;
            while (transform.parent != null && transform.parent.GetComponent<RectTransform>() != null)
            {
                transform = transform.parent;
                path = transform.name + "/" + path;
            }

            int firstSlash = path.IndexOf('/');
            if (firstSlash >= 0)
            {
                path = path.Substring(firstSlash + 1);
            }
            return path;
        }

        void OnDisable()
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

            ItemProduct itemProduct = (ItemProduct)target;
            if (GUILayout.Button("Set Prefab Name"))
            {
                if(itemProduct.ItemObject == null) return;
                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(itemProduct.ItemObject)
                    , itemProduct.Name);
                AssetDatabase.Refresh();
            }
            if (GUILayout.Button("Generate Image"))
            {
                if(itemProduct.ItemObject == null) return;

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
                
                Texture2D preview = AssetPreview.GetAssetPreview(itemProduct.ItemObject);
                
                byte[] bytes = preview.EncodeToPNG();

                string typeDirectory = Enum.GetName(typeof(ITEMTYPEENUM), itemProduct.ItemType);
                
                string path = Application.dataPath +
                              $"/Resources/Items/Images/{typeDirectory}/{itemProduct.Name}" + ".png";
                
                Directory.CreateDirectory(Path.GetDirectoryName(path));
                
                File.WriteAllBytes(path, bytes);
                
                AssetDatabase.Refresh();
                
                EditorApplication.update += WaitForTextureImport;
                void WaitForTextureImport()
                {
                    Sprite sprite = Resources.Load<Sprite>($"Items/Images/{typeDirectory}/{itemProduct.Name}");
                    if (sprite  != null)
                    {
                        itemProduct.ItemIcon = sprite;
                        EditorApplication.update -= WaitForTextureImport;
                    }
                }
            }
        }
    }

}