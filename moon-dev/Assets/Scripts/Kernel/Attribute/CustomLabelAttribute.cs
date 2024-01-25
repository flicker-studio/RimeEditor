using System;
using UnityEngine;

namespace Moon.Kernel.Attribute
{
    /// <inheritdoc />
    /// <summary>
    /// Make the field display the custom name in the Inspector.
    /// </summary>
    [AttributeUsage(AttributeTargets.Field)]
    public class CustomLabelAttribute : PropertyAttribute
    {
        /// <summary>
        ///     The name you want to display
        /// </summary>
        public readonly string Name;

        /// <summary>
        /// Make the field display the custom name in the Inspector.
        /// </summary>
        /// <param name="name">Custom name</param>
        public CustomLabelAttribute(string name)
        {
            Name = name;
        }
    }
}