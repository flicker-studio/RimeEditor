using System;
using UnityEditor;
using UnityEngine;

namespace RimeEditor.Editor
{
    [CustomEditor(typeof(RectTransform), true)]
    public class CustomRectTransformEditor : UnityEditor.Editor
    {
        private UnityEditor.Editor _defaultEditor;

        private void OnEnable()
        {
            _defaultEditor = CreateEditor(targets, Type.GetType("UnityEditor.RectTransformEditor, UnityEditor"));
        }

        private void OnDisable()
        {
            DestroyImmediate(_defaultEditor);
        }

        public override void OnInspectorGUI()
        {
            if (target == null) return;

            _defaultEditor.OnInspectorGUI();

            if (GUILayout.Button("Copy Path"))
            {
                var rectTransform = (RectTransform)target;
                var path          = GeneratePath(rectTransform);
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
                path      = transform.name + "/" + path;
            }

            var firstSlash = path.IndexOf('/');

            if (firstSlash >= 0) path = path.Substring(firstSlash + 1);

            return path;
        }
    }
}