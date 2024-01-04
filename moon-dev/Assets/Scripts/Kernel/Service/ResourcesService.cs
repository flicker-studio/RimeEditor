using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Moon.Kernel.Attribute;
using Moon.Kernel.Setting;
using UnityEngine.AddressableAssets;

namespace Moon.Kernel.Service
{
    [UsedImplicitly, SystemService(typeof(ResourcesService))]
    public sealed class ResourcesService : Service
    {
        private static class Const
        {
            internal const string Key1 = "Assets/Settings/Dev/MoonSetting.asset";
        }

        private MoonSetting m_basicSetting;

        internal async override void OnStart()
        {
            m_basicSetting = await Addressables.LoadAssetAsync<MoonSetting>(Const.Key1);
            Explorer.MoonSetting = m_basicSetting;
        }

        internal override void OnStop()
        {
        }

        internal override void Dispose(bool all)
        {
        }

        internal override Task Run()
        {
            return Task.CompletedTask;
        }

        internal override Task Abort()
        {
            Addressables.Release(m_basicSetting);
            return Task.CompletedTask;
        }
    }
}