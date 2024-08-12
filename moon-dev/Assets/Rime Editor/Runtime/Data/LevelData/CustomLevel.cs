using System;
using System.IO;
using LevelEditor;

namespace RimeEditor.Runtime
{
    /// <summary>
    ///     A custom level data structure with both information and data
    /// </summary>
    public sealed class CustomLevel
    {
        /// <summary>
        ///     The name of this level info file
        /// </summary>
        public const string InfoFileName = ".inf";

        private readonly string _rootPath;

        /// <summary>
        ///     Basic information about the level
        /// </summary>
        public readonly LevelInfo Info;

        /// <summary>
        ///     Specific level data can be instantiated in the scene
        /// </summary>
        public LevelData Data;

        /// <summary>
        ///     Generated based on level information
        /// </summary>
        /// <param name="info">Information about the target level</param>
        public CustomLevel(LevelInfo info)
        {
            Info      = info;
            Data      = new LevelData();
            _rootPath = Path.Combine(DataLoader.StoreFolderPath, Info.ID);

            var inf_file_path  = Path.Combine(_rootPath, InfoFileName);
            var data_fold_path = Path.Combine(_rootPath, "data");
        }

        /// <summary>
        ///     Create a new custom level
        /// </summary>
        public CustomLevel()
        {
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
            if (!File.Exists(_rootPath)) throw new Exception("No root directory found!");

            //remove from memory
            Data = null;

            //remove from disk
            File.Delete(_rootPath);
        }
    }
}