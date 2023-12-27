using System;
using System.IO;
using System.Linq;
using Cysharp.Threading.Tasks;
using Moon.Kernel.Service;
using Moon.Kernel.Setting;
using UnityEditor;
using UnityEngine.AddressableAssets;
using UnityEngine.SceneManagement;

#if UNITY_EDITOR
namespace Moon.Kernel
{
    public static partial class Boot
    {
        public static MoonSetting MoonSetting { get; private set; }

        public static string[] SceneName;

        private static string PersistenceSceneName => SceneService.PersistenceSceneName;

        [InitializeOnLoadMethod]
        private static async void EditorBoot()
        {
            MoonSetting = await Addressables.LoadAssetAsync<MoonSetting>("Assets/Settings/Dev/MoonSetting.asset");
            Scene();
            PreCheck();
        }
    }

    public static partial class Boot
    {
        private static void Scene()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            SceneName = scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]).ToArray();
        }

        private static void PreCheck()
        {
            if (!MoonSetting.isCheck)
            {
                return;
            }

            if (!SceneName.Contains(PersistenceSceneName))
            {
                throw new Exception($"{PersistenceSceneName} scene is lose!");
            }
        }
    }
}
#endif