using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Moon.Kernel;
using Moon.Kernel.Service;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.Kernel.Service
{
    [Author("Mors")]
    [TestOf(nameof(SceneService))]
    internal class SceneServiceTest
    {
        [UnityTest]
        public IEnumerator TransitionScenePasses()
        {
            return UniTask.ToCoroutine(async () =>
            {
                try
                {
                    var trans = Explorer.TryGetService<SceneService>();
                    await trans.StartTask();
                    await trans.TransitionScene("TestScene", Action);

                    var sceneName = trans.ActiveScene.name;
                    Assert.AreEqual("TestScene", sceneName);
                }
                catch (Exception e)
                {
                    Assert.Fail(e.Message);
                }

                return;

                void Action()
                {
                    Debug.Log("Finish");
                }
            });
        }
    }
}