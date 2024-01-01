using Cysharp.Threading.Tasks;
using Moon.Kernel.Setting;
using UnityEngine;
using UnityEngine.AddressableAssets;
using SCM = Moon.Kernel.Service.ServiceControlManager;

namespace Moon.Kernel
{
    /// <summary>
    ///     The class of the entire game system is responsible for initialization at the lowest level.
    /// </summary>
    public static partial class Boot
    {
        /// <summary>
        /// </summary>
        public static UniTask InitTask => _source.Task;

        public static MoonSetting MoonSetting { get; private set; }

        private static UniTaskCompletionSource _source;

        [RuntimeInitializeOnLoadMethod]
        private static async void RuntimeBoot()
        {
            _source = new UniTaskCompletionSource();
            Debug.Log("<color=green>[SYS]</color> System is Booting...");

            MoonSetting = await Addressables.LoadAssetAsync<MoonSetting>("Assets/Settings/Dev/MoonSetting.asset");

            await SCM.RegisterServices();
            _source.TrySetResult();
        }
    }
}