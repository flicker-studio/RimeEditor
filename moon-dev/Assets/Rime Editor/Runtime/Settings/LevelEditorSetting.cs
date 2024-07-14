using System;
using LevelEditor.Settings;
using UnityEngine;

namespace LevelEditor
{
    public class LevelEditorSetting : ScriptableObject
    {
        /// <summary>
        ///     The storage path of the current setting file
        /// </summary>
        public const string Path = "Assets/Settings/LevelEditorSetting.asset";

        public CameraSetting CameraSetting;

        public UISetting UISetting;

        public PrefabSetting PrefabSetting;

        public OutlineSetting OutlineSetting;

        public GlobalSetting GlobalSetting;

        /// <summary>
        ///     A unique instance of the setting
        /// </summary>
        public static LevelEditorSetting Instance { get; private set; }

        private void Awake()
        {
            if (Instance == null)
                Instance = this;
            else
                throw new Exception("Objects are created repeatedly!");
        }

        private void OnEnable()
        {
            Instance = this;
        }

        private void OnDestroy()
        {
            Instance = null;
        }
    }
}