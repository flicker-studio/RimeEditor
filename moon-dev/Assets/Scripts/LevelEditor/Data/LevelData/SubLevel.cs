using System.Collections.Generic;
using Newtonsoft.Json;

namespace LevelEditor
{
    /// <summary>
    ///     Used to store sublevel data
    /// </summary>
    public struct SubLevel
    {
        /// <summary>
        ///     The name of the sublevel
        /// </summary>
        [JsonProperty("Name", Order = 1)] public string Name;

        /// <summary>
        ///     A list of the resources referenced by the current level
        /// </summary>
        [JsonProperty("ItemAssets", Order = 2)]
        public List<Item> ItemAssets;

        /// <summary>
        ///     The default construction of sublevel data
        /// </summary>
        /// <param name="name">The name of the current sublevel</param>
        public SubLevel(string name)
        {
            Name       = name;
            ItemAssets = new List<Item>();
        }
    }
}