using System.Collections.Generic;
using LevelEditor;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace RimeEditor.Editor
{
    /// <inheritdoc />
    public class LevelEditorSettingProvider : SettingsProvider
    {
        private static SerializedObject _settings;

        private LevelEditorSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null)
            : base(path, scopes, keywords)
        {
        }

        /// <inheritdoc />
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var asset = LevelEditorSetting.Instance;
            _settings = asset == null ? null : new SerializedObject(asset);
        }

        /// <inheritdoc />
        public override void OnDeactivate()
        {
            _settings = null;
        }

        /// <inheritdoc />
        public override void OnGUI(string searchContext)
        {
            #if ENABLE_INPUT_SYSTEM
            EditorGUILayout.HelpBox("Input System: enable", MessageType.Info, true);
            #else
            EditorGUILayout.HelpBox(new GUIContent("Input System: disable"));
            #endif
            if (_settings == null)
            {
                EditorGUILayout.HelpBox("Unable to detect profile!", MessageType.Error);

                if (!GUILayout.Button("Create a asset")) return;
                var data = ScriptableObject.CreateInstance<LevelEditorSetting>();
                AssetDatabase.CreateAsset(data, LevelEditorSetting.Path);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
                _settings = new SerializedObject(LevelEditorSetting.Instance);
            }
            else
            {
                EditorGUILayout.ObjectField(_settings.FindProperty("CameraSetting"), new GUIContent("CameraSetting"));
                EditorGUILayout.ObjectField(_settings.FindProperty("UISetting"));
                EditorGUILayout.ObjectField(_settings.FindProperty("PrefabSetting"));
                EditorGUILayout.ObjectField(_settings.FindProperty("OutlineSetting"));
                _settings.ApplyModifiedPropertiesWithoutUndo();
            }
        }

        [SettingsProvider]
        internal static SettingsProvider CreateSettingsProvider()
        {
            var provider = new LevelEditorSettingProvider("Project/Custom Settings", SettingsScope.Project)
                           {
                               keywords = GetSearchKeywordsFromGUIContentProperties<Styles>()
                           };
            return provider;
        }

        private class Styles
        {
            public static readonly GUIContent Start = new("UISetting");
        }
    }
}