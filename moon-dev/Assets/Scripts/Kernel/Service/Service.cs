using System;
using System.Threading.Tasks;
using Cysharp.Threading.Tasks;

namespace Moon.Kernel.Service
{
    /// <summary>
    ///     Provides a base class for the service that will exist as part of the service application.
    ///     When you create a new service class, you must derive from <c>Service</c>.
    /// </summary>
    public abstract class Service : IDisposable
    {
        protected static bool IsInstanced;

        protected static bool IsActive;

        public bool IsRunning => IsInstanced && IsActive;

        /// <summary>
        ///     Specifies the action to take when the service starts( before <see cref="Run" /> ).
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
        /// <param name="disposing">
        ///     Dispose(bool disposing) executes in two distinct scenarios.
        ///     If disposing equals true, the method has been called directly
        ///     or indirectly by a user's code. Managed and unmanaged resources
        ///     can be disposed.
        ///     If disposing equals false, the method has been called by the
        ///     runtime from inside the finalizer and you should not reference
        ///     other objects. Only unmanaged resources can be disposed.
        /// </param>
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                // Dispose managed resources.
            }

            IsInstanced = false;
            IsActive    = false;
        }

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

        /// <summary>
        ///     Implement IDisposable.
        ///     Do not make this method virtual.
        ///     A derived class should not be able to override this method.
        /// </summary>
        public void Dispose()
        {
            Dispose(true);
            // This object will be cleaned up by the Dispose method.
            // Therefore, you should call GC.SuppressFinalize to
            // take this object off the finalization queue
            // and prevent finalization code for this object
            // from executing a second time.
            GC.SuppressFinalize(this);
        }
    }
}