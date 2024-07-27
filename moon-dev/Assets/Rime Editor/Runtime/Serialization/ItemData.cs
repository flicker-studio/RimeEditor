using System;
using JetBrains.Annotations;
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
        [NotNull] public readonly string ID;

        /// <summary>
        ///     Position in the scene, updated at any time
        /// </summary>
        [NotNull] public Transform Transform;

        /// <summary>
        ///     User information string,default is empty to save space
        /// </summary>
        [CanBeNull] public string UserData;

        /// <summary>
        ///     Default constructor,instantiate with existing data
        /// </summary>
        /// <param name="id">Saved ID</param>
        /// <param name="transform">Saved location information</param>
        /// <param name="useData">Saved user data</param>
        public ItemData([NotNull] string id, [NotNull] Transform transform, [CanBeNull] string useData)
        {
            //check value
            if (string.IsNullOrEmpty(id) || string.IsNullOrWhiteSpace(id)) throw new NullReferenceException();

            ID        = id;
            Transform = transform;
            UserData  = useData;
        }
    }
}