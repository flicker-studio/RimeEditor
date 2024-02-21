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
        public string Name => m_name;

        public string Author => m_author;

        public string Introduction => m_introduction;

        public Texture2D Cover => m_cover;

        /// <summary>
        ///     The name of level
        /// </summary>
        private string m_name;

        /// <summary>
        ///     Author's name
        /// </summary>
        private string m_author;

        /// <summary>
        ///     Introduction to the level
        /// </summary>
        private string m_introduction;

        /// <summary>
        ///     The cover of the level
        /// </summary>
        private readonly Texture2D m_cover;

        /// <summary>
        ///     The cover path of the level
        /// </summary>
        private readonly string m_coverPath;

        public LevelInfo([NotNull] string name, [NotNull] string author, [NotNull] string introduction, [NotNull] Texture2D cover)
        {
            m_name         = name;
            m_author       = author;
            m_introduction = introduction;
            m_coverPath    = null;
            m_cover        = cover;
        }

        public LevelInfo([NotNull] string name, [NotNull] string author, [NotNull] string introduction)
        {
            m_name         = name;
            m_author       = author;
            m_introduction = introduction;
            m_coverPath    = null;
            m_cover        = Texture2D.grayTexture;
        }

        public void UpdateInfo
        (
            string name         = null,
            string author       = null,
            string introduction = null
        )
        {
            if (name != null)
            {
                m_name = name;
            }

            if (author != null)
            {
                m_author = author;
            }

            if (introduction != null)
            {
                m_introduction = introduction;
            }
        }
/*
        public async UniTask UpdateCover(string rootPath)
        {
            var path = Path.Combine(rootPath, m_coverName + ".dat");

            // await using var reader = new FileStream(path, FileMode.Open);
            // var byteData = new byte[reader.Length];
            // var read = reader.Read(byteData, 0, byteData.Length);
            var uwr = UnityWebRequestTexture.GetTexture("file:///" + path);
            await uwr.SendWebRequest();
            m_cover = DownloadHandlerTexture.GetContent(uwr);
        }*/
    }
}