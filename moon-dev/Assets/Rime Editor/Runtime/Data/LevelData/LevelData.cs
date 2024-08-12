using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Used to store data of level
    /// </summary>
    public class LevelData
    {
        /// <summary>
        ///     Specifies the Prefab mapping dictionary for SubLevel
        /// </summary>
        public Dictionary<int, GameObject> PrefabDictionary;

        /// <summary>
        ///     List of sublevel data
        /// </summary>
        public List<SubLevel> SubLevelDataList;
    }
}