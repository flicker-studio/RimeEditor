using System;
using System.Collections.Generic;
using System.Linq;
using Moon.Kernel.Service;

namespace Moon.Kernel
{
    /// <summary>
    ///     This class obtains the system variables
    /// </summary>
    public static class Explorer
    {
        private static IEnumerable<IService> Services => Boot.SystemServices;
        private static readonly Dictionary<Type, IService> ServicesCache = new();

        /// <summary>
        ///     Get the running Service class
        /// </summary>
        /// <typeparam name="T">Type of Service</typeparam>
        /// <returns>The instance of service</returns>
        /// <exception cref="NullReferenceException">Throw exception if there is not the service in memory</exception>
        public static T TryGetService<T>()
        {
            var serviceType = typeof(T);
            if (ServicesCache.TryGetValue(serviceType, out var targetsValue)) return (T)targetsValue;

            foreach (var service in Services.Where(service => service.GetType() == serviceType))
            {
                ServicesCache.Add(service.GetType(), service);
                return (T)service;
            }

            throw new NullReferenceException();
        }
    }
}