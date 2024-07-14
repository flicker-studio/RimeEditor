#region

using JetBrains.Annotations;
using LevelEditor.Data.Serialization;
using Newtonsoft.Json;
using UnityEngine;

#endregion

namespace LevelEditor
{
    /// <summary>
    ///     Used to store created level information
    /// </summary>
    [JsonConverter(typeof(LevelInfoConverter))]
    public struct LevelInfo
    {
        public string Name => _name;

        public string Author => _author;

        public string Introduction => _introduction;

        public Texture2D Cover => _cover;

        /// <summary>
        ///     The name of level
        /// </summary>
        private string _name;

        /// <summary>
        ///     Author's name
        /// </summary>
        private string _author;

        /// <summary>
        ///     Introduction to the level
        /// </summary>
        private string _introduction;

        /// <summary>
        ///     The cover of the level
        /// </summary>
        private readonly Texture2D _cover;

        /// <summary>
        ///     The cover path of the level
        /// </summary>
        private readonly string _coverPath;

        public LevelInfo([NotNull] string name, [NotNull] string author, [NotNull] string introduction, [NotNull] Texture2D cover)
        {
            _name         = name;
            _author       = author;
            _introduction = introduction;
            _coverPath    = null;
            _cover        = cover;
        }

        public LevelInfo([NotNull] string name, [NotNull] string author, [NotNull] string introduction)
        {
            _name         = name;
            _author       = author;
            _introduction = introduction;
            _coverPath    = null;
            _cover        = Texture2D.grayTexture;
        }

        public void UpdateInfo
        (
            string name         = null,
            string author       = null,
            string introduction = null
        )
        {
            if (name != null) _name = name;

            if (author != null) _author = author;

            if (introduction != null) _introduction = introduction;
        }
/*
        public async UniTask UpdateCover(string rootPath)
        {
            var path = Path.Combine(rootPath, _coverName + ".dat");

            // await using var reader = new FileStream(path, FileMode.Open);
            // var byteData = new byte[reader.Length];
            // var read = reader.Read(byteData, 0, byteData.Length);
            var uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();
            _cover = DownloadHandlerTexture.GetContent(uwr);
        }*/
    }
}