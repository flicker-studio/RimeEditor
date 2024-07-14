using LevelEditor.Data;
using LevelEditor.View.Element;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor.Item
{
    /// <summary>
    ///     Item base class, which is the base class for the creatable sections in the Level Editor.
    /// </summary>
    [JsonConverter(typeof(ItemDataConverter))]
    public abstract class ItemBase
    {
        /// <summary>
        ///     Game Object bound to Item
        /// </summary>
        public readonly GameObject GameObject;

        /// <summary>
        ///     Generate new Item based on type
        /// </summary>
        /// <param name="type">Type required</param>
        protected internal ItemBase(ItemType type)
        {
            Type = type;
            // TODO: Bound ItemView 
            // _view      = new ItemView();
            GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        /// <summary>
        ///     View of entries displayed in Hierarchy
        /// </summary>
        public ItemView View { get; }

        /// <summary>
        ///     The type of the item and it is an enumerated value
        /// </summary>
        public ItemType Type { get; }

        /// <summary>
        ///     The location of the item in the editor
        /// </summary>
        public Transform Transform => GameObject.transform;

        /// <summary>
        ///     Set the item to inactive
        /// </summary>
        public virtual void Inactive()
        {
            View.Inactive();
            GameObject.SetActive(false);
        }

        /// <summary>
        ///     Set the item to active
        /// </summary>
        public virtual void Active(bool a = false)
        {
            View.Active();
            GameObject.SetActive(true);
        }

        /// <summary>
        ///     Called when preview mode is enabled
        /// </summary>
        public virtual void Preview()
        {
        }

        public virtual void Reset()
        {
        }
    }
}