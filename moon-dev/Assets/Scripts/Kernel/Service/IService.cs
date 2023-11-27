using System.Threading.Tasks;

namespace Moon.Kernel.Service
{
    /// <summary>
    ///     System service interface. This interface allows users to create long-running, executable applications in .
    ///     Services start automatically when the game starts, can be paused and restarted.
    /// </summary>
    /// <remarks>Services are ideal for situations where long-running functionality is required.</remarks>
    public interface IService
    {
        /// <summary>
        ///     Start the service, allocate system resources.
        /// </summary>
        /// <returns>The task at the end</returns>
        public Task Run();

        /// <summary>
        ///     Abort the service, release all the held resources.
        /// </summary>
        /// <returns>The task at the end</returns>
        public Task Abort();
    }
}