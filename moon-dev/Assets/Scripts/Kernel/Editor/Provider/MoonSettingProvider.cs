using System.Collections.Generic;
using Moon.Kernel.Setting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Moon.Kernel.Editor.Provider
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class MoonSettingProvider : SettingsProvider
    {
        private static SerializedObject _settings;

        private MoonSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
        }

        /// <inheritdoc />
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            var setting = AssetDatabase.LoadAssetAtPath<MoonSetting>("Assets/Settings/Dev/MoonSetting.asset");
            _settings = new SerializedObject(setting);
        }

        /// <inheritdoc />
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(_settings.FindProperty("isCheck"), Styles.Start);
            EditorGUILayout.PropertyField(_settings.FindProperty("AutoStartScene"), Styles.Enable);

            if (_settings.FindProperty("AutoStartScene").boolValue)
            {
                EditorGUILayout.PropertyField(_settings.FindProperty("startScene"), Styles.Scene);
            }

            _settings.ApplyModifiedPropertiesWithoutUndo();
        }

        private class Styles
        {
            public static readonly GUIContent Start = new("Enable status checks");

            public static readonly GUIContent Enable = new("Enable auto load scene");

            public static readonly GUIContent Scene = new("Start Scene");
        }

        [SettingsProvider]
        private static SettingsProvider CreateSettingsProvider()
        {
            var provider = new MoonSettingProvider("Project/Moon", SettingsScope.Project);

            if (_settings != null)
            {
                provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            }

            return provider;
        }
    }
}