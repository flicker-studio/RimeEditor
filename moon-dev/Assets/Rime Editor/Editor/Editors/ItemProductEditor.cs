using System;
using System.IO;
using LevelEditor.Data;
using UnityEditor;
using UnityEngine;

namespace RimeEditor.Editor
{
    /// <summary>
    ///     <see cref="ItemProduct" /> custom display in editor
    /// </summary>
    [CustomEditor(typeof(ItemProduct))]
    public class ItemProductEditor : UnityEditor.Editor
    {
        /// <inheritdoc />
        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            var itemProduct = (ItemProduct)target;

            if (GUILayout.Button("Set Prefab Name"))
            {
                if (itemProduct.ItemObject == null) return;

                AssetDatabase.RenameAsset(AssetDatabase.GetAssetPath(itemProduct.ItemObject)
                                        , itemProduct.Name);

                AssetDatabase.Refresh();
            }

            if (GUILayout.Button("Generate Image"))
            {
                if (itemProduct.ItemObject == null) return;

                if (itemProduct.ItemIcon != null)
                {
                    if (itemProduct.ItemIcon.name != itemProduct.Name)
                        AssetDatabase.DeleteAsset(AssetDatabase.GetAssetPath(itemProduct.ItemIcon));
                    else
                        return;
                }

                var preview = AssetPreview.GetAssetPreview(itemProduct.ItemObject);

                var bytes = preview.EncodeToPNG();

                var typeDirectory = Enum.GetName(typeof(ItemType), itemProduct.ItemType);

                var path = Application.dataPath +
                           $"/Resources/Items/Images/{typeDirectory}/{itemProduct.Name}" + ".png";

                Directory.CreateDirectory(Path.GetDirectoryName(path) ?? string.Empty);

                File.WriteAllBytes(path, bytes);

                AssetDatabase.Refresh();

                EditorApplication.update += WaitForTextureImport;

                void WaitForTextureImport()
                {
                    var sprite = Resources.Load<Sprite>($"Items/Images/{typeDirectory}/{itemProduct.Name}");

                    if (sprite != null)
                    {
                        itemProduct.ItemIcon     =  sprite;
                        EditorApplication.update -= WaitForTextureImport;
                    }
                }
            }
        }
    }
}