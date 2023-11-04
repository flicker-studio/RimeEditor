using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Frame.Static.Extensions
{
    public static class ComponentMethod
    {
         [Obsolete("Consider using the extension method under Moon.Extension.Unity.Method.Component.CopyComponentValue instead this.")]
        public static void CopyComponent(this Component original, Component target)
        {
            System.Type type = target.GetType();
            System.Reflection.FieldInfo[] fields = type.GetFields();
            foreach (System.Reflection.FieldInfo field in fields)
            {
                field.SetValue(original, field.GetValue(target));
            }
        }

    }
}
