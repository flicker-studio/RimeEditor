using System;
using System.Collections.Generic;
using System.Globalization;
using Moon.Kernel.Extension;
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

        /// <inheritdoc cref="m_hashKey" />
        [JsonIgnore]
        public string HashKey => m_hashKey;

        /// <summary>
        ///     Update the information in the structure
        /// </summary>
        public void Update(string levelName = null, string author = null, string introduction = null, string version = null, Texture2D cover = null)
        {
            LevelName = levelName;
            AuthorName = author;
            Introduction = introduction;
            Version = version;
            Cover = cover;

            m_createDate = DateTime.Now;
            var strings = LevelName + m_createDate;
            m_hashKey = strings.ToSHA256();
        }

        /// <summary>
        ///     Gets the hash of the current data
        /// </summary>
        [JsonProperty("Key", Order = 1)]
        private string m_hashKey;

        /// <summary>
        ///     The name of the current level
        /// </summary>
        [JsonProperty("LevelName", Order = 2)]
        public string LevelName;

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
        [JsonProperty("Version", Order = 5)]
        public string Version;

        /// <summary>
        ///     Date of last modification
        /// </summary>
        [JsonProperty("CreateDate", Order = 6)]
        private DateTime m_createDate;

        /// <summary>
        ///     Sublevel data
        /// </summary>
        [JsonProperty("SubLevelDatas", Order = 7)]
        public List<SubLevelData> SubLevelDatas;

        /// <summary>
        ///     Current storage location
        /// </summary>
        [JsonIgnore]
        public string Path;

        /// <summary>
        ///     The cover of the level
        /// </summary>
        [JsonIgnore]
        public Texture2D Cover;

        public LevelData(int a)
        {
            Path = default;
            LevelName = default;
            AuthorName = default;
            Introduction = default;
            Version = default;
            Cover = default;
            SubLevelDatas = new List<SubLevelData>();
            m_hashKey = default;
            m_createDate = default;
        }
    }
}