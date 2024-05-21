using LevelEditor.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Item base class, which is the base class for the creatable sections in the Level Editor.
    /// </summary>
    [JsonConverter(typeof(ItemDataConverter))]
    public abstract class Item
    {
        /// <summary>
        ///     View of entries displayed in Hierarchy
        /// </summary>
        public ItemView View { get; }
        
        /// <summary>
        ///     The type of the item and it is an enumerated value
        /// </summary>
        public ItemType Type { get; }
        
        /// <summary>
        ///     Game Object bound to Item
        /// </summary>
        public readonly GameObject GameObject;
        
        /// <summary>
        ///     The location of the item in the editor
        /// </summary>
        public Transform Transform => GameObject.transform;
        
        /// <summary>
        ///     Generate new Item based on type
        /// </summary>
        /// <param name="type">Type required</param>
        protected internal Item(ItemType type)
        {
            Type = type;
            // TODO: Bound ItemView 
            // _view      = new ItemView();
            GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }
        
        /// <summary>
        ///     Set the item to inactive
        /// </summary>
        public abstract void Inactive();
        
        /// <summary>
        ///     Set the item to active
        /// </summary>
        public abstract void Active(bool a = false);
        
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