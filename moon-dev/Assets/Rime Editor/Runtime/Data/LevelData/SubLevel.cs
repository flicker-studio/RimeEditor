using System.Collections.Generic;
using LevelEditor.Item;

namespace LevelEditor
{
    /// <summary>
    ///     Used to store sublevel data
    /// </summary>
    public class SubLevel
    {
        /// <summary>
        ///     A list of the resources referenced by the current level
        /// </summary>
        public List<ItemBase> ItemAssets = new();

        public string Name = string.Empty;
    }
}