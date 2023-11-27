using UnityEngine;
using SCM = Moon.Kernel.Service.ServiceControlManager;

namespace Moon.Kernel
{
    /// <summary>
    ///     The class of the entire game system is responsible for initialization at the lowest level.
    /// </summary>
    internal static class Boot
    {
        internal const string PersistenceSceneName = "Persistent";

        [RuntimeInitializeOnLoadMethod]
        private static async void BootLoader()
        {
            Debug.Log("<color=green>[SYS]</color> System is Booting...");
            await SCM.SCMInit();
        }
    }
}