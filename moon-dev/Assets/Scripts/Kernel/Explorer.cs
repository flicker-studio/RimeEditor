using Cysharp.Threading.Tasks;
using Moon.Kernel.Setting;
using SCM = Moon.Kernel.Service.ServiceControlManager;


namespace Moon.Kernel
{
    /// <summary>
    ///     This class obtains the system variables
    /// </summary>
    public static class Explorer
    {
        /// <summary>
        ///     Contains a variety of configuration items
        /// </summary>
        public static class Setting
        {
            /// <summary>
            ///     The basic setting of the project
            /// </summary>
            public static MoonSetting MoonSetting;
        }

        /// <summary>
        ///     The mark of completion of the initialization task
        /// </summary>
        public static UniTask BootCompletionTask => Boot.Source.Task;


        public static T TryGetService<T>() where T : Service.Service
        {
            return SCM.TryGetService<T>();
        }

        public static T TryGetSetting<T>() where T : SettingBase
        {
            return SettingHelper.TryGetSetting<T>();
        }
    }
}