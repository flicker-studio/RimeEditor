using System;

namespace Moon.Kernel.Service
{
    /// <summary>
    ///     Provides a base class for the service that will exist as part of the service application.
    ///     When you create a new service class, you must derive from <c>ServiceBase</c>.
    /// </summary>
    public abstract class ServiceBase
    {
        /// <summary>
        ///     Specifies the action to take when the service starts.
        /// </summary>
        /// <remarks>
        ///     When the Service Control Manager (SCM) sends a Start command to a service,
        ///     or when the operating system starts (for services that start automatically).
        /// </remarks>
        protected virtual void OnStart()
        {
            throw new NotImplementedException();
        }


        /// <summary>
        ///     Specifying the action to take when normal functionality resumes after the service is paused.
        /// </summary>
        /// <remarks>
        ///     Runs when the Service Control Manager (SCM) sends the Continue command to a service,
        /// </remarks>
        protected virtual void OnStop()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Dispose of occupied resources.
        /// </summary>
        /// <param name="all">
        ///     To release all resources, then <see langword="true" />;
        ///     If only temporary is released, it is <see langword="false" />.
        /// </param>
        protected abstract void Dispose(bool all);
    }
}