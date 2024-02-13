using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Moon.Kernel.Service
{
    /// <summary>
    ///     Provides a base class for the service that will exist as part of the service application.
    ///     When you create a new service class, you must derive from <c>Service</c>.
    /// </summary>
    public abstract class Service
    {
        protected static bool IsInstanced;

        protected static bool IsActive = false;

        public bool IsRunning => IsInstanced && IsActive;

        /// <summary>
        ///     Specifies the action to take when the service starts( before <see cref="Run"/> ).
        /// </summary>
        /// <remarks>
        ///     When the Service Control Manager (SCM) sends a Start command to a service,
        ///     or when the operating system starts (for services that start automatically).
        /// </remarks>
        internal abstract void OnStart();

        /// <summary>
        ///     Specifying the action to take when normal functionality resumes after the service is paused.
        /// </summary>
        /// <remarks>
        ///     Runs when the Service Control Manager (SCM) sends the Continue command to a service,
        /// </remarks>
        internal abstract void OnStop();

        /// <summary>
        ///     Dispose of occupied resources.
        /// </summary>
        /// <param name="all">
        ///     To release all resources, then <see langword="true" />;
        ///     If only temporary is released, it is <see langword="false" />.
        /// </param>
        internal abstract void Dispose(bool all);

        /// <summary>
        ///     Start the service, allocate system resources.
        /// </summary>
        /// <returns>The task at the end</returns>
        internal abstract UniTask Run();

        /// <summary>
        ///     Abort the service, release all the held resources.
        /// </summary>
        /// <returns>The task at the end</returns>
        internal abstract Task Abort();

        internal virtual void Instanced()
        {
            IsInstanced = true;
        }
    }
}