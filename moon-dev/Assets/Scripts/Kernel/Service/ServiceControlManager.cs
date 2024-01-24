using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Moon.Kernel.Attribute;
using UnityEngine;

namespace Moon.Kernel.Service
{
    /// <summary>
    ///     Use this class to manage system services
    /// </summary>
    public static class ServiceControlManager
    {
        private static readonly List<Service> RunningServices = new();

        private static readonly Dictionary<Type, Service> ServicesCache = new();

        private static Action _onStart;

        /// <summary>
        ///     Register a service that is not running
        /// </summary>
        /// <param name="service">Service instances</param>
        /// <exception cref="Exception">An exception is thrown when the service is already running</exception>
        public static void Run([NotNull] Service service)
        {
            if (RunningServices.Contains(service))
            {
                throw new Exception();
            }

            service.Run();
            RunningServices.Add(service);
        }


        /// <summary>
        ///     Terminate a running service to free up memory
        /// </summary>
        /// <param name="service">The service instance that needs to be terminated</param>
        /// <exception cref="NullReferenceException">When the service is not running, an exception is thrown</exception>
        public static void Abort([NotNull] Service service)
        {
            if (!RunningServices.Contains(service))
            {
                throw new NullReferenceException();
            }

            service.Abort();
            RunningServices.Remove(service);
        }


        /// <summary>
        ///     Get the running Service class
        /// </summary>
        /// <typeparam name="T">Type of Service</typeparam>
        /// <returns>The instance of service</returns>
        /// <exception cref="NullReferenceException">Throw exception if there is not the service in memory</exception>
        public static T TryGetService<T>() where T : Service
        {
            var serviceType = typeof(T);

            if (ServicesCache.TryGetValue(serviceType, out var targetsValue))
            {
                return (T)targetsValue;
            }

            foreach (var service in RunningServices.Where(service => service.GetType() == serviceType))
            {
                ServicesCache.Add(service.GetType(), (T)service);
                return (T)service;
            }

            throw new NullReferenceException();
        }


        /// <summary>
        ///     Register for services by attribute
        /// </summary>
        /// <exception cref="WarningException">Thrown when an error occurs during the creation of an instance</exception>
        internal static async Task RegisterServices()
        {
            Debug.Log("<color=green>[SERVICE]</color> Initializing service");

            Debug.Log("<color=green>[SERVICE]</color> Instantiating the service");

            await Task.Run(() =>
                {
                    var serviceAssembly = typeof(Service).Assembly;
                    var assemblyTypes = serviceAssembly.GetTypes();

                    foreach (var type in assemblyTypes)
                    {
                        var attr = type.GetCustomAttribute<SystemServiceAttribute>();

                        if (attr == null)
                        {
                            continue;
                        }

                        var instance = attr.Create();

                        if (instance is null)
                        {
                            throw new WarningException($"There has some error in {type} class");
                        }

                        RunningServices.Add(instance);

                        _onStart += instance.OnStart;
                    }
                }
            );

            _onStart.Invoke();

            Debug.Log("<color=green>[SERVICE]</color>  Instantiation of service is complete");
            var runTask = RunningServices.Select(service => service.Run());
            await Task.WhenAll(runTask);
            Debug.Log("<color=green>[SERVICE]</color> Initialization is complete");
        }
    }
}