using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Moon.Kernel;
using Moon.Kernel.Service;
using NUnit.Framework;
using UnityEngine.TestTools;
using SCM = Moon.Kernel.Service.ServiceControlManager;

namespace Test.Kernel.Service
{
    [Author("Mors"), TestOf(nameof(SceneService))]
    internal class SceneServiceTest
    {
        [UnityTest]
        public IEnumerator TransitionActiveScenePasses()
        {
            return UniTask.ToCoroutine(async () =>
            {
                try
                {
                    await Explorer.BootkCompletionTask;
                    var trans = SCM.TryGetService<SceneService>();
                    await trans.TransitionActiveScene("TestScene");
                    var sceneName = trans.ActiveScene.name;
                    Assert.AreEqual("TestScene", sceneName);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }
            });
        }
    }
}