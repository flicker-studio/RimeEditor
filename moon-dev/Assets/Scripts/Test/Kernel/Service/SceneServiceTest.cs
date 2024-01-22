using System;
using System.Collections;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Moon.Kernel;
using Moon.Kernel.Service;
using NUnit.Framework;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using SCM = Moon.Kernel.Service.ServiceControlManager;

namespace Test.Kernel.Service
{
    [Author("Mors"), TestOf(nameof(SceneService))]
    internal class SceneServiceTest
    {
        private SceneService m_sceneService;

        [UnitySetUp]
        public IEnumerator Setup()
        {
            return UniTask.ToCoroutine(async () =>
            {
                await Explorer.BootCompletionTask;
                m_sceneService = SCM.TryGetService<SceneService>();
                SceneManager.MergeScenes(m_sceneService.ActiveScene, m_sceneService.PersistenceScene);
                await m_sceneService.TryLoadActiveScene("Test Scene 1");
            });
        }

        [UnityTest]
        public IEnumerator TransitionActiveScenePass()
        {
            return UniTask.ToCoroutine(async () =>
            {
                await m_sceneService.TransitionActiveScene("Test Scene 2");
                var sceneName = m_sceneService.ActiveScene.name;
                Assert.AreEqual("Test Scene 2", sceneName);
            });
        }

        [UnityTest]
        public IEnumerator TransitionActiveSceneFail()
        {
            return UniTask.ToCoroutine(() =>
            {
                Assert.ThrowsAsync<Exception>(Code);

                return UniTask.CompletedTask;

                async Task Code()
                {
                    await m_sceneService.TransitionActiveScene("Test??");
                }
            });
        }
    }
}