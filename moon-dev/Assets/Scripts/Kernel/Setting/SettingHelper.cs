using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Cysharp.Threading.Tasks;
using Moon.Kernel.Attribute;
using UnityEngine.AddressableAssets;

namespace Moon.Kernel.Setting
{
    internal static class SettingHelper
    {
        private static readonly List<SettingBase> Settings = new();

        private static readonly Dictionary<Type, SettingBase> SettingCache = new();

        internal static T TryGetSetting<T>() where T : SettingBase
        {
            var serviceType = typeof(T);

            if (SettingCache.TryGetValue(serviceType, out var targetsValue))
            {
                return (T)targetsValue;
            }

            foreach (var setting in Settings.Where(setting => setting.GetType() == serviceType))
            {
                var ans = (T)setting;
                SettingCache.Add(setting.GetType(), ans);
                return ans;
            }

            throw new NullReferenceException();
        }

        internal static async UniTask LoadSettingsAsync()
        {
            var serviceAssembly = typeof(SettingBase).Assembly;
            var assemblyTypes = serviceAssembly.GetTypes();

            foreach (var type in assemblyTypes)
            {
                var attr = type.GetCustomAttribute<SystemSettingAttribute>();

                if (attr == null)
                {
                    continue;
                }

                var path = attr.Path;

                try
                {
                    var assets = await Addressables.LoadAssetAsync<SettingBase>(path);
                    Settings.Add(assets);
                }
                catch (Exception)
                {
                    throw new NullReferenceException();
                }
            }
        }
    }
}