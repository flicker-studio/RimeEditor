using Frame.Tool.Pool;
using LevelEditor.Data;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Item base class
    /// </summary>
    [JsonConverter(typeof(ItemDataConverter))]
    public abstract class AbstractItem
    {
        /// <summary>
        /// </summary>
        public ItemView View => _view;

        public ItemType Type => _type;

        /// <summary>
        ///     Game Object bound to Item
        /// </summary>
        public readonly GameObject GameObject;

        public Transform Transform => GameObject.transform;

        private readonly ItemType _type;
        private readonly ItemView _view;

        /// <summary>
        ///     Generate new Item based on type
        /// </summary>
        /// <param name="type">Type required</param>
        /// <param name="view">Bound ItemView</param>
        protected AbstractItem(ItemType type, ItemView view)
        {
            _type      = type;
            _view      = view;
            GameObject = GameObject.CreatePrimitive(PrimitiveType.Cube);
        }

        /// <summary>
        ///     Set the item to inactive
        /// </summary>
        public void Inactive()
        {
        }

        /// <summary>
        ///     Set the item to active
        /// </summary>
        public void Active(bool a = false)
        {
        }

        public virtual void SetActivePlay(bool active)
        {
            if (active)
            {
            }
            else
            {
                ObjectPool.Instance.GameObjectPool.Release(GameObject);
            }
        }

        public void Delete()
        {
            ObjectPool.Instance.GameObjectPool.Release(GameObject);
        }
    }
}