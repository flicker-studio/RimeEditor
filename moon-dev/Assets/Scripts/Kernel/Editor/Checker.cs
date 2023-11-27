using System;
using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine.SceneManagement;

namespace Moon.Kernel.Editor
{
    internal static class Checker
    {
        private static string PersistenceSceneName => Boot.PersistenceSceneName;
#if UNITY_EDITOR

        [InitializeOnLoadMethod]
        private static void PreCheck()
        {
            var sceneCount = SceneManager.sceneCountInBuildSettings;
            var scenePath = new string[sceneCount];

            for (var i = 0; i < sceneCount; i++) scenePath[i] = Path.GetFileNameWithoutExtension(SceneUtility.GetScenePathByBuildIndex(i));

            var sceneName = scenePath.Select(path => path[(path.LastIndexOf('/') + 1)..]).ToArray();

            if (!sceneName.Contains(PersistenceSceneName))
                throw new Exception($"{PersistenceSceneName} scene is lose!");
        }

#endif
    }
}