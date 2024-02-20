using System;
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
        public static UniTaskCompletionSource Source;

        private static bool AsyncInitialized  => Source != null && Source.Task.GetAwaiter().IsCompleted;
        private static bool AsyncInitializing => Source != null && !Source.Task.GetAwaiter().IsCompleted;

        public static event Action PostBoot;

        [RuntimeInitializeOnLoadMethod]
        private static async void RuntimeBoot()
        {
            if (AsyncInitialized) return;
            if (AsyncInitializing)
            {
                await Source.Task;
                return;
            }

            Source = new UniTaskCompletionSource();
            Debug.Log("<color=green>[SYS]</color> System is Booting...");
            await SCM.RegisterServices();
            Source.TrySetResult();
            PostBoot?.Invoke();
        }
        
        public static void Destroy ()
        {
            Source = null;
            SCM.Destroy();
        }
    }
}