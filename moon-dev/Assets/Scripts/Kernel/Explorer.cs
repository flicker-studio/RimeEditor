using SCM = Moon.Kernel.Service.ServiceControlManager;

namespace Moon.Kernel
{
    /// <summary>
    ///     This class obtains the system variables
    /// </summary>
    public static class Explorer
    {
        public static T TryGetService<T>() where T : Service.Service
        {
            return SCM.TryGetService<T>();
        }
    }
}