using System;

namespace Moon.Kernel.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]
    public sealed class SystemSettingAttribute : System.Attribute
    {
        internal readonly string Path;

        public SystemSettingAttribute(string path)
        {
            Path = path;
        }
    }
}