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
        internal static UniTaskCompletionSource Source = new();

        [RuntimeInitializeOnLoadMethod]
        private static async void RuntimeBoot()
        {
            Debug.Log("<color=green>[SYS]</color> System is Booting...");

            await SCM.RegisterServices();
            Source.TrySetResult();
        }
    }
}