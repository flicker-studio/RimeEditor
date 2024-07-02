using Moon.Kernel.Attribute;
using UnityEditor;
using UnityEngine;

namespace Moon.Kernel.Editor.Drawer
{
    /// <inheritdoc />
    /// <summary>
    ///     Defines the drawing behavior of panel content for fields with the <see cref="CustomLabelAttribute" />.
    /// </summary>
    [CustomPropertyDrawer(typeof(CustomLabelAttribute))]
    public class CustomLabelDrawer : PropertyDrawer
    {
        private GUIContent m_label;

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (m_label == null)
            {
                var name = (attribute as CustomLabelAttribute)?.Name;
                m_label = new GUIContent(name);
            }

            EditorGUI.PropertyField(position, property, m_label);
        }
    }
}