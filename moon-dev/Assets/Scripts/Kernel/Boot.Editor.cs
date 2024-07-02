using System;
using System.IO;
using System.Linq;
using Moon.Kernel.Service;
using Moon.Kernel.Setting;
using UnityEditor;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
namespace Moon.Kernel
{
    public static partial class Boot
    {
        public static string[] SceneName;

        private static string PersistenceSceneName => SceneService.PersistenceSceneName;

        [InitializeOnLoadMethod]
        private static void EditorBoot()
        {
            Scene();
            PreCheck();
            EditorApplication.playModeStateChanged += A;
        }
    }

    public static partial class Boot
    {
        private static void Scene()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath  = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            SceneName = scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]).ToArray();
        }

        private static void PreCheck()
        {
            var setting = AssetDatabase.LoadAssetAtPath<MoonSetting>("Assets/Settings/Dev/MoonSetting.asset");

            if (!setting.isCheck)
            {
                return;
            }

            if (!SceneName.Contains(PersistenceSceneName))
            {
                throw new Exception($"{PersistenceSceneName} scene is lose!");
            }
        }

        private static void A(PlayModeStateChange playModeStateChange)
        {
            if (playModeStateChange == PlayModeStateChange.ExitingPlayMode)
            {
                Destroy();
            }
        }
    }
}
#endif