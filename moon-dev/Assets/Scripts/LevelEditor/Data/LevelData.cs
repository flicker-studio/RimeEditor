using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    public class LevelData
    {
        [JsonIgnore] 
        public Texture2D GetLevelCoverImage => m_levelCoverImage;
        [JsonIgnore]
        public string GetName => m_levelName;
        [JsonIgnore]
        public string GetTime => m_createDate.ToString();
        [JsonIgnore]
        public string GetAuthorName => m_authorName;
        [JsonIgnore]
        public string GetIntroduction => m_introduction;
        [JsonIgnore]
        public string GetVersion => m_version;
        [JsonIgnore]
        public List<SubLevelData> GetSubLevelDatas => m_subLevelDatas;
        [JsonIgnore]
        public string GetKey => m_hashKey;
        [JsonIgnore]
        public string Path;

        public void UpdateTime()
        {
            m_createDate = DateTime.Now;
        }

        public Texture2D SetLevelCoverImage
        {
            set => m_levelCoverImage = value;
        }
        
        public string SetName
        {
            set => m_levelName = value;
        }

        public string SetAuthorName
        {
            set => m_authorName = value;
        }
        
        public string SetIntroduction
        {
            set => m_introduction = value;
        }

        public string SetVersion
        {
            set => m_version = value;
        }

        public string SetKey
        {
            set => m_hashKey = value;
        }

        public List<SubLevelData> SetSubLevelDatas
        {
            set
            {
                m_subLevelDatas.Clear();
                m_subLevelDatas.AddRange(value);
            }
        }
        [JsonIgnore]
        private Texture2D m_levelCoverImage;
        [JsonProperty("Key",Order = 1)]
        private string m_hashKey;
        [JsonProperty("LevelName",Order = 2)]
        private string m_levelName;
        [JsonProperty("AuthorName",Order = 3)]
        private string m_authorName;
        [JsonProperty("Introduction",Order = 4)]
        private string m_introduction;
        [JsonProperty("Version",Order = 5)]
        private string m_version;
        [JsonProperty("CreateDate",Order = 6)]
        private DateTime m_createDate;
        [JsonProperty("SubLevelDatas",Order = 7)]
        private List<SubLevelData> m_subLevelDatas = new List<SubLevelData>();
    }
}
