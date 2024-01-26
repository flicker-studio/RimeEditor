using System;
using System.Collections;
using Cysharp.Threading.Tasks;
using Moon.Kernel;
using Moon.Kernel.Service;
using Moon.Kernel.Setting;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Test.Kernel
{
    [Author("Mors"), TestOf(nameof(Explorer)), TestFixture]
    internal class ExplorerTest
    {
        private class CustomSetting : SettingBase
        {
        }

        [UnityTest]
        public IEnumerator TryGetServicePasses()
        {
            Assert.IsInstanceOf<SceneService>(Explorer.TryGetService<SceneService>());
            yield return null;
        }

        [UnityTest]
        public IEnumerator TryGetSettingPass()
        {
            return UniTask.ToCoroutine(async () =>
            {
                await Explorer.BootCompletionTask;

                var actual = Explorer.TryGetSetting<MoonSetting>();
                var expected = await ResourcesService.LoadAssetAsync<MoonSetting>("Assets/Settings/Dev/MoonSetting.asset");

                Assert.AreEqual(expected, actual);
            });
        }

        [UnityTest]
        public IEnumerator TryGetSettingFail()
        {
            return UniTask.ToCoroutine(async () =>
            {
                await Explorer.BootCompletionTask;

                Assert.Throws<NullReferenceException>(Code);
                return;

                void Code()

                {
                    Explorer.TryGetSetting<CustomSetting>();
                }
            });
        }
    }
}