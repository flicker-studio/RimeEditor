using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Moon.Kernel.Service
{
    public sealed class ServiceControlManager
    {
        internal static readonly List<Service> RunningServices = new();

        private static readonly Dictionary<Type, IService> ServicesCache = new();


        #region public API

        public static void Run([NotNull] IService service)
        {
            if (service is not Service ser)
            {
                throw new Exception();
            }

            if (RunningServices.Contains(ser))
            {
                throw new Exception();
            }

            ser.Run();
            RunningServices.Add(ser);
        }

        public static void Abort([NotNull] IService service)
        {
            if (service is not Service ser)
            {
                throw new Exception();
            }

            if (!RunningServices.Contains(ser))
            {
                throw new NullReferenceException();
            }

            ser.Abort();
            RunningServices.Remove(ser);
        }


        /// <summary>
        ///     Get the running Service class
        /// </summary>
        /// <typeparam name="T">Type of Service</typeparam>
        /// <returns>The instance of service</returns>
        /// <exception cref="NullReferenceException">Throw exception if there is not the service in memory</exception>
        public static T TryGetService<T>() where T : Service, IService
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

        #endregion

        #region internal API

        internal static async Task SCMInit()
        {
            await InitService();
        }

        #endregion

        private static async Task InitService()
        {
            Debug.Log("<color=green>[SERVICE]</color> Initializing service");

            Debug.Log("<color=green>[SERVICE]</color> Instantiating the service");

            await Task.Run(() =>
                {
                    var serviceAssembly = typeof(Service).Assembly;
                    var assemblyTypes = serviceAssembly.GetTypes();
                    var serviceTypes = assemblyTypes.Where(type => type.GetInterfaces().Contains(typeof(IService)));

                    foreach (var service in serviceTypes)
                    {
                        var typeName = service.ToString();

                        if (serviceAssembly.CreateInstance(typeName) is not Service instance)
                        {
                            throw new WarningException($"There has some error in {service} class");
                        }

                        RunningServices.Add(instance);
                    }
                }
            );

            Debug.Log("<color=green>[SERVICE]</color>  Instantiation of service is complete");
            var runTask = RunningServices.Select(service => service.Run());
            await Task.WhenAll(runTask);
            Debug.Log("<color=green>[SERVICE]</color> Initialization is complete");
        }
    }
}