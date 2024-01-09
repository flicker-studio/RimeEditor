using System.Collections;
using Cysharp.Threading.Tasks;
using Moon.Kernel;
using Moon.Kernel.Service;
using Moon.Kernel.Setting;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Test.Kernel.Service
{
    [Author("Mors"), TestOf(nameof(ResourcesService))]
    internal class ResourceServiceTest
    {
        [UnityTest]
        public IEnumerator LoadAssetAsyncPass()
        {
            return UniTask.ToCoroutine(async () =>
            {
                await Explorer.BootCompletionTask;

                var setting =
                    ResourcesService.LoadAssetAsync<MoonSetting>("Assets/Settings/Dev/MoonSetting.asset").Result;

                Assert.AreEqual(Explorer.Settings.MoonSetting, setting);
            });
        }
    }
}