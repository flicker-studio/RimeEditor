using System;
using System.IO;
using LevelEditor.Data.Serialization;
using Newtonsoft.Json;
using RimeEditor.Runtime;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Used to store created level information
    /// </summary>
    /// <remarks>
    ///     Use value types to avoid GC
    /// </remarks>
    [JsonConverter(typeof(LevelInfoConverter))]
    public struct LevelInfo
    {
        /// <summary>
        ///     The name of level cover file
        /// </summary>
        private const string CoverFileName = "cover.jpg";

        /// <summary>
        ///     The name of the level
        /// </summary>
        public string Name;

        /// <summary>
        ///     Author's name
        /// </summary>
        public string Author;

        /// <summary>
        ///     Introduction to the level
        /// </summary>
        public string Introduction;

        /// <summary>
        ///     The cover of the level
        /// </summary>
        public Texture2D Cover;

        /// <summary>
        ///     The ID of the level, randomly generated and unique
        /// </summary>
        public string ID => _id.ToString();

        private readonly Guid _id;

        public LevelInfo(string name, string author, string introduction, Texture2D cover)
        {
            _id          = Guid.NewGuid();
            Name         = name;
            Author       = author;
            Introduction = introduction;

            Cover = cover == null ? Texture2D.grayTexture : cover;
        }

        public LevelInfo(string name, string author, string introduction, string id)
        {
            Name         = name;
            Author       = author;
            Introduction = introduction;
            _id          = Guid.Parse(id);
            Cover        = null;
        }

        public void UpdateCover()
        {
            var cover_path = Path.Combine(DataLoader.StoreFolderPath, _id.ToString(), CoverFileName);

            using var reader    = new FileStream(cover_path, FileMode.Open);
            var       byte_data = new byte[reader.Length];
            var       read      = reader.Read(byte_data, 0, byte_data.Length);
            Cover.LoadImage(byte_data);
        }

        /// <inheritdoc />
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            if (obj is not LevelInfo other) return false;

            return _id.Equals(other._id)
                && Name == other.Name
                && Author == other.Author
                && Introduction == other.Introduction;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return HashCode.Combine(_id, Name, Author, Introduction);
        }
    }
}