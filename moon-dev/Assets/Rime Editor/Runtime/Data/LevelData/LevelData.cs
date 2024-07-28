using System;
using System.Collections.Generic;
using System.Globalization;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Used to store level data
    /// </summary>
    /// <remarks>
    ///     Use value types to avoid GC
    /// </remarks>
    public struct LevelData
    {
        /// <inheritdoc cref="m_createDate" />
        [JsonIgnore]
        public string CreateTime => m_createDate.ToString(CultureInfo.CurrentCulture);

        /// <summary>
        ///     Update the information in the structure
        /// </summary>
        public void Update(string levelName = null, string author = null, string introduction = null, string version = null, Texture2D cover = null)
        {
            LevelName    = levelName;
            AuthorName   = author;
            Introduction = introduction;
            Version      = version;
            Cover        = cover;

            m_createDate = DateTime.Now;
        }

        /// <summary>
        ///     The name of the current level
        /// </summary>
        [JsonProperty("LevelName", Order = 2)] public string LevelName;

        /// <summary>
        ///     Author's name
        /// </summary>
        [JsonProperty("AuthorName", Order = 3)]
        public string AuthorName;

        /// <summary>
        ///     Introduction to the level
        /// </summary>
        [JsonProperty("Introduction", Order = 4)]
        public string Introduction;

        /// <summary>
        ///     Current level version (to be removed)
        /// </summary>
        [JsonProperty("Version", Order = 5)] public string Version;

        /// <summary>
        ///     Date of last modification
        /// </summary>
        [JsonProperty("CreateDate", Order = 6)]
        private DateTime m_createDate;

        /// <summary>
        ///     List of sublevel data
        /// </summary>
        [JsonProperty("SubLevelDataList", Order = 7)]
        public List<SubLevel> SubLevelDataList;

        /// <summary>
        ///     Current storage location
        /// </summary>
        [JsonIgnore] public string Path;

        /// <summary>
        ///     The cover of the level
        /// </summary>
        [JsonIgnore] public Texture2D Cover;

        public LevelData(int a)
        {
            Path             = default;
            LevelName        = default;
            AuthorName       = default;
            Introduction     = default;
            Version          = default;
            Cover            = default;
            SubLevelDataList = new List<SubLevel>();

            m_createDate = default;
        }
    }
}