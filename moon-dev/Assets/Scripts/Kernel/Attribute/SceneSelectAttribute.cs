using System;
using UnityEngine;

namespace Moon.Kernel.Attribute
{
    /// <inheritdoc />
    [AttributeUsage(AttributeTargets.Field)]
    public sealed class SceneSelectAttribute : PropertyAttribute
    {
    }
}