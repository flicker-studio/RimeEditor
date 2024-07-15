using Newtonsoft.Json;
using UnityEngine;

namespace RimeEditor.Runtime
{
    /// <summary>
    ///     Persistent data of the item
    /// </summary>
    [JsonConverter(typeof(ItemDataConverter))]
    public struct ItemData
    {
        /// <summary>
        ///     The prefab ID of the item, read only
        /// </summary>
        public readonly string ID;

        /// <summary>
        ///     Position in the scene, updated at any time
        /// </summary>
        public Transform Transform;

        /// <summary>
        ///     User information string,default is empty to save space
        /// </summary>
        public string UserData;

        /// <summary>
        ///     Default constructor,instantiate with existing data
        /// </summary>
        /// <param name="id">Saved ID</param>
        /// <param name="transform">Saved location information</param>
        /// <param name="useData">Saved user data</param>
        public ItemData(string id, Transform transform, string useData)
        {
            ID        = id;
            Transform = transform;
            UserData  = useData;
        }
    }
}