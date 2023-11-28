using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace Moon.Kernel.Service
{
    public class ServiceControlManager
    {
        internal static readonly List<IService> RunningServices = new();

        #region public API

        public static void Run([NotNull] IService service)
        {
            if (RunningServices.Contains(service))
            {
                throw new Exception();
            }

            service.Run();
            RunningServices.Add(service);
        }

        public static void Abort([NotNull] IService service)
        {
            if (!RunningServices.Contains(service))
            {
                throw new NullReferenceException();
            }

            service.Abort();
            RunningServices.Remove(service);
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
                    var serviceAssembly = typeof(ServiceBase).Assembly;
                    var assemblyTypes = serviceAssembly.GetTypes();
                    var serviceTypes = assemblyTypes.Where(type => type.GetInterfaces().Contains(typeof(IService)));

                    foreach (var service in serviceTypes)
                    {
                        var typeName = service.ToString();

                        if (serviceAssembly.CreateInstance(typeName) is not IService instance)
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