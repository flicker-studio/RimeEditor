using System.Collections.Generic;
using Moon.Kernel.Setting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Moon.Kernel.Editor
{
    /// <inheritdoc />
    /// <summary>
    /// </summary>
    public class MoonSettingProvider : SettingsProvider
    {
        private const string Path = "Assets/Settings/Dev/MoonSetting.asset";

        private static SerializedObject _settings;

        private MoonSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
        }

        [InitializeOnLoadMethod]
        private static void LoadSetting()
        {
            MoonSetting = AssetDatabase.LoadAssetAtPath<MoonSetting>(Path);
        }

        public static MoonSetting MoonSetting { get; private set; }

        /// <inheritdoc />
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            _settings = new SerializedObject(MoonSetting);
        }

        /// <inheritdoc />
        public override void OnGUI(string searchContext)
        {
            EditorGUILayout.PropertyField(_settings.FindProperty("isCheck"), Styles.Start);
            _settings.ApplyModifiedPropertiesWithoutUndo();
        }

        private class Styles
        {
            public static readonly GUIContent Start = new("Enable status checks");
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