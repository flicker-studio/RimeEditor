using System;
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
        private static MoonSetting _setting;

        private static SerializedObject _serializedSetting;

        private MoonSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
        }

        

        /// <inheritdoc />
        public override void OnGUI(string searchContext)
        {
            _setting = (MoonSetting)EditorGUILayout.ObjectField(_setting, typeof(MoonSetting), false);

            if (_setting == null)
            {
                return;
            }

            _serializedSetting = new SerializedObject(_setting);
            EditorGUILayout.PropertyField(_serializedSetting.FindProperty("isCheck"), Styles.Start);
            EditorGUILayout.PropertyField(_serializedSetting.FindProperty("AutoStartScene"), Styles.Enable);

            if (_serializedSetting.FindProperty("AutoStartScene").boolValue)
            {
                EditorGUILayout.PropertyField(_serializedSetting.FindProperty("startScene"), Styles.Scene);
            }

            _serializedSetting.ApplyModifiedPropertiesWithoutUndo();
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

            if (_serializedSetting != null)
            {
                provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            }

            return provider;
        }
    }
}