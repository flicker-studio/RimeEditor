using System.Collections.Generic;
using Moon.Kernel.Setting;
using UnityEditor;
using UnityEngine;
using UnityEngine.UIElements;

namespace Moon.Kernel.Editor.Provider
{
    /// <inheritdoc />
    public class GlobalSettingProvider : SettingsProvider
    {
        private static SerializedObject _serializedSetting;

        private const string GlobalSettingPath = "Assets/Settings/GlobalSettings/GlobalSetting.asset";

        private bool[] m_folds = { true, true, true, true, true, true };

        private GlobalSettingProvider(string path, SettingsScope scopes, IEnumerable<string> keywords = null) :
            base(path, scopes, keywords)
        {
        }

        /// <inheritdoc />
        public override void OnActivate(string searchContext, VisualElement rootElement)
        {
            // Called when the user clicks on the MyCustom element in the Settings window
            var setting = AssetDatabase.LoadAssetAtPath<GlobalSetting>(GlobalSettingPath);
            _serializedSetting = new SerializedObject(setting);
        }

        /// <inheritdoc />
        public override void OnDeactivate()
        {
            // User selected another setting or closed the Settings window
            _serializedSetting = null;
        }

        /// <inheritdoc />
        public override void OnGUI(string searchContext)
        {
            _serializedSetting.Update();
            GlobalSettingEditor.OnGUI(_serializedSetting, ref m_folds);
            _serializedSetting.ApplyModifiedPropertiesWithoutUndo();
        }

        private class Styles
        {
            public static readonly GUIContent SceneSetting = new("场景设置");

            public static readonly GUIContent LevelEditor = new("关卡编辑器场景名字");

            public static readonly GUIContent LevelPlay = new("关卡播放器场景名字");

            public static readonly GUIContent LayerSetting = new("层级设置");

            public static readonly GUIContent GroundLayer = new("地面层级");

            public static readonly GUIContent LabelSettings = new("标签设置");

            public static readonly GUIContent HandleLabels = new("控制柄标签");

            public static readonly GUIContent TagSettings = new("名字标签属性设置");

            public static readonly GUIContent RigidBodyLabels = new("刚体标签");

            public static readonly GUIContent ReproducibleTags = new("可复制物体标签");

            public static readonly GUIContent ScenarioConfiguration = new("场景配置");

            public static readonly GUIContent PersistentSetting = new("可持久化文件属性");
        }

        [SettingsProvider]
        private static SettingsProvider RegisterSettingsProvider()
        {
            var provider = new GlobalSettingProvider("Project/GlobalSetting", SettingsScope.Project);

            if (_serializedSetting != null)
            {
                provider.keywords = GetSearchKeywordsFromGUIContentProperties<Styles>();
            }

            return provider;
        }
    }
}