using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Moon.Kernel.Attribute;
using Moon.Kernel.Setting;
using UnityEngine.AddressableAssets;

namespace Moon.Kernel.Service
{
    /// <inheritdoc />
    /// <summary>
    /// This service is used for resource management, which automatically or manually unloads resources to save memory
    /// </summary>
    [UsedImplicitly, SystemService(typeof(ResourcesService))]
    public sealed class ResourcesService : Service
    {
        private static class Const
        {
        }

        internal async override void OnStart()
        {
            await SettingHelper.LoadSettingsAsync();
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
            return Task.CompletedTask;
        }

        /// <summary>
        /// Load an addressable resource based on the address
        /// </summary>
        /// <param name="key">The address of the addressable resource</param>
        /// <typeparam name="T">The type of addressable resource</typeparam>
        /// <returns>Resource</returns>
        /// <exception cref="Exception">当资源不存在时会抛出错误</exception>
        public static async Task<T> LoadAssetAsync<T>(string key)
        {
            var a = await Addressables.LoadAssetAsync<T>(key);

            if (a == null)
            {
                throw new Exception($"Value of {key} doesn't seem to be right.");
            }

            return a;
        }
    }
}