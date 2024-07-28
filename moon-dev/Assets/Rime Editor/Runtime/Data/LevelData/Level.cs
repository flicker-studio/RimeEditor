using System;
using LevelEditor;

namespace RimeEditor.Runtime.Level
{
    /// <summary>
    ///     A custom level data structure with both information and data
    /// </summary>
    public struct Level
    {
        /// <summary>
        ///     Specific level data can be instantiated in the scene
        /// </summary>
        public readonly LevelData Data;

        /// <summary>
        ///     Basic information about the level
        /// </summary>
        public readonly LevelInfo Info;

        /// <summary>
        /// Unique constructor, which is generated based on level information and can be used for import and new creation
        /// </summary>
        /// <param name="info">Information about the current level</param>
        /// <exception cref="InvalidOperationException"></exception>
        public Level(LevelInfo info)
        {
            Info = info;
            Data = new LevelData();
        }

        public void Save()
        {
            // Collect paths

            //Write LevelInfo

            //Write LevelData
        }

        /// <summary>
        ///     Deleting the level, including removing the cache
        /// </summary>
        public void Delete()
        {
        }
    }
}