using System;
using System.IO;
using System.Linq;
using Moon.Kernel.Service;
using UnityEngine.SceneManagement;

namespace Moon.Kernel.Editor
{
    internal static class Checker
    {
        private static string PersistenceSceneName => SceneService.PersistenceSceneName;

        private static void PreCheck()
        {
            if (!MoonSettingProvider.MoonSetting.isCheck)
            {
                return;
            }

            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            var sceneName = scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]).ToArray();

            if (!sceneName.Contains(PersistenceSceneName))
                throw new Exception($"{PersistenceSceneName} scene is lose!");
        }
    }
}