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
    ///     This service is used for resource management, which automatically or manually unloads resources to save memory
    /// </summary>
    [UsedImplicitly, SystemService]
    public sealed class ResourcesService : Service
    {
        ~ResourcesService()
        {
            // Do not re-create Dispose clean-up code here.
            // Calling Dispose(disposing: false) is optimal in terms of
            // readability and maintainability.
            Dispose(false);
        }

        internal override void OnStart()
        {
        }

        internal override void OnStop()
        {
        }

        internal override async UniTask Run()
        {
            await SettingHelper.LoadSettingsAsync();
        }

        internal override Task Abort()
        {
            return Task.CompletedTask;
        }

        /// <summary>
        ///     Load an addressable resource based on the address
        /// </summary>
        /// <param name="key">The address of the addressable resource</param>
        /// <typeparam name="T">The type of addressable resource</typeparam>
        /// <returns>Resource</returns>
        /// <exception cref="Exception">An error is thrown when the resource does not exist</exception>
        public static async UniTask<T> LoadAssetAsync<T>(string key)
        {
            var a = await Addressables.LoadAssetAsync<T>(key);

            if (a == null) throw new Exception($"Value of {key} doesn't seem to be right.");

            return a;
        }
    }
}