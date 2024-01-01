using System;
using System.Reflection;
using JetBrains.Annotations;

namespace Moon.Kernel.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public class SystemServiceAttribute : System.Attribute
    {
        private readonly Type m_type;

        private readonly Assembly m_assembly;

        internal SystemServiceAttribute(Type t)
        {
            m_type = t;
            m_assembly = t.Assembly;
        }

        [CanBeNull]
        internal Service.Service Create()
        {
            var service = m_assembly.CreateInstance(m_type.FullName!) as Service.Service;
            service?.Instanced();
            return service;
        }
    }
}