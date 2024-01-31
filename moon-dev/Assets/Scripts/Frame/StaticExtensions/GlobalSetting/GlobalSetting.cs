using System;
using System.Collections.Generic;
using Moon.Kernel.Attribute;
using UnityEditor;
using UnityEngine;

namespace Frame.Static.Global
{
    public class GlobalSetting : ScriptableObject
    {
        private const string GlobalSettingPath = "Assets/Settings/GlobalSettings/GlobalSetting.asset";

        public Scenes GetScenes => m_scenes;

        public Tags GetTags => m_tags;

        public ObjNameTag GetObjNameTag => m_objNameTag;

        public ScreenInfo GetScreenInfo => m_screenInfo;

        public CriticalPath GetCriticalPath => m_criticalPath;

        public PersistentFileProperty GetPersistentFileProperty => m_persistentFileProperty;
        
        [SerializeField,Header("场景设置")]
        private Scenes m_scenes;

        [SerializeField,Header("层级设置")]
        private LayerMasks m_layerMasks;

        [SerializeField,Header("标签设置")]
        private Tags m_tags;
        
        [SerializeField,Header("名字标签属性设置")]
        private ObjNameTag m_objNameTag;

        [SerializeField,Header("场景配置")]
        private ScreenInfo m_screenInfo;

        [SerializeField,Header("关键路径设置")]
        private CriticalPath m_criticalPath;

        [SerializeField,Header("可持久化文件属性")]
        private PersistentFileProperty m_persistentFileProperty;
        
        internal static GlobalSetting GetOrCreateSettings()
        {
            var settings = AssetDatabase.LoadAssetAtPath<GlobalSetting>(GlobalSettingPath);
            if (settings == null)
            {
                settings = ScriptableObject.CreateInstance<GlobalSetting>();
                settings.m_scenes.LEVEL_EDITOR = "LevelEditor";
                settings.m_scenes.LEVEL_PLAY = "LevelPlay";
                settings.m_layerMasks.GROUND = LayerMask.NameToLayer("Ground");
                settings.m_tags.CONTROL_HANDLE = "ControlHandle";
                settings.m_objNameTag.RIGIDBODY_TAG = "<rigidbody>";
                settings.m_objNameTag.CAN_COPY_TAG = "<canCopy>";
                settings.m_criticalPath.ITEM_FILE_PATH = "Items\\ScriptableObject";
                settings.m_persistentFileProperty.LEVEL_DATA_NAME = "LevelDatas";
                settings.m_persistentFileProperty.GAMES_DATA_NAME = "GameDatas";
                settings.m_persistentFileProperty.IMAGES_DATA_NAME = "Images";
                settings.m_persistentFileProperty.SOUNDS_DATA_NAME = "Sounds";
                settings.m_persistentFileProperty.COVER_IMAGE_NAME = "CoverImage.png";
                settings.m_screenInfo.SCREEN_SIZE_STANDARD = new Vector2(1920f, 1080f);
                AssetDatabase.CreateAsset(settings, GlobalSettingPath);
                AssetDatabase.SaveAssets();
                AssetDatabase.Refresh();
            }
            return settings;
        }

        internal static SerializedObject GetSerializedSettings()
        {
            return new SerializedObject(GetOrCreateSettings());
        }
        [Serializable]
        public struct Scenes
        {
            [CustomLabel("关卡编辑器场景名字")]
            public string LEVEL_EDITOR;
            [CustomLabel("关卡播放器场景名字")]
            public string LEVEL_PLAY;
        }
        [Serializable]
        public struct LayerMasks
        {
            [CustomLabel("地面层级")]
            public LayerMask GROUND;
        }
        [Serializable]
        public struct Tags
        {
            [CustomLabel("控制柄标签")]
            public string CONTROL_HANDLE;
        }
        [Serializable]
        public struct ObjNameTag
        {
            [CustomLabel("刚体标签")]
            public string RIGIDBODY_TAG;
            [CustomLabel("可复制物体标签")]
            public string CAN_COPY_TAG;
        }
        [Serializable]
        public struct ScreenInfo
        {
            [CustomLabel("屏幕标准尺寸")]
            public Vector2 SCREEN_SIZE_STANDARD;
            public Vector2 REFERENCE_RESOLUTION => new Vector2(SCREEN_SIZE_STANDARD.x, SCREEN_SIZE_STANDARD.y / Screen.width * Screen.height);
        }
        [Serializable]
        public struct CriticalPath
        {
            [CustomLabel("物件文件路径")]
            public string ITEM_FILE_PATH;
        }
        [Serializable]
        public struct PersistentFileProperty
        {
            [CustomLabel("关卡数据名字")]
            public string LEVEL_DATA_NAME;
            public string LEVEL_DATA_PATH => $"{Application.persistentDataPath}/{LEVEL_DATA_NAME}";
            [CustomLabel("游戏数据文件夹名字")]
            public string GAMES_DATA_NAME;
            [CustomLabel("图像数据文件夹名字")]
            public string IMAGES_DATA_NAME;
            [CustomLabel("声音数据文件夹名字")]
            public string SOUNDS_DATA_NAME;
            [CustomLabel("背景图片文件名字")]
            public string COVER_IMAGE_NAME;
        }
    }

    static class GlobalSettingIMGUIRegister
    {
        [SettingsProvider]
        public static SettingsProvider GlobalSettingSettingsProvider()
        {
            var provider = new SettingsProvider("Project/GlobalSetting", SettingsScope.Project)
            {
                label = "GlobalSetting",
                guiHandler = (searchContext) =>
                {
                    var settings = GlobalSetting.GetSerializedSettings();
                    EditorGUILayout.PropertyField(settings.FindProperty("m_scenes"), new GUIContent("Scenes"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_layerMasks"), new GUIContent("LayerMasks"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_tags"), new GUIContent("Tags"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_objNameTag"), new GUIContent("ObjNameTag"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_screenInfo"), new GUIContent("ScreenInfo"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_criticalPath"), new GUIContent("CriticalPath"));
                    EditorGUILayout.PropertyField(settings.FindProperty("m_persistentFileProperty"), new GUIContent("PersistentFileProperty"));
                    settings.ApplyModifiedPropertiesWithoutUndo();
                },
                    
                keywords = new HashSet<string>(new[] { "Scenes", "LayerMasks","Tags","ObjNameTag","ScreenInfo","CriticalPath","PersistentFileProperty" })
            };

            return provider;
        }
    }
}
