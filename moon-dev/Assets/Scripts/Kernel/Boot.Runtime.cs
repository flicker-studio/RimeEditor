using Cysharp.Threading.Tasks;
using UnityEngine;
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

        private static UniTaskCompletionSource _source;

        [RuntimeInitializeOnLoadMethod]
        private static async void RuntimeBoot()
        {
            _source = new UniTaskCompletionSource();
            Debug.Log("<color=green>[SYS]</color> System is Booting...");
            await SCM.SCMInit();
            _source.TrySetResult();
        }
    }
}