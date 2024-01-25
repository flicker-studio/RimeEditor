#if UNITY_EDITOR
using System;

namespace UnityToolkit.Editor
{
    public delegate bool ParseDelegate<in TData, TResult>(TData value, out TResult result);

    public static class Parser<TData, TResult>
    {
        public static ParseDelegate<TData, TResult> parser;
    }

    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Enum | AttributeTargets.Struct, Inherited = false)]
    public class ParseAttribute : Attribute
    {
    }
}
#endif