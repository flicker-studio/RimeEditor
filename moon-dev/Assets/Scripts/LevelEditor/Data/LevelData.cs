using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace LevelEditor
{
    public class LevelData
    {
        [JsonIgnore]
        public string GetName => m_levelName;
        [JsonIgnore]
        public string GetTime => m_createDate.ToString();
        [JsonIgnore]
        public string GetAuthorName => m_authorName;
        [JsonIgnore]
        public string GetIntroduction => m_introduction;
        [JsonIgnore]
        public List<SubLevelData> GetSubLevelDatas => m_subLevelDatas.Copy();

        public void UpdateTime()
        {
            m_createDate = DateTime.Now;
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

        public List<SubLevelData> SetSubLevelDatas
        {
            set
            {
                m_subLevelDatas.Clear();
                m_subLevelDatas.AddRange(value);
            }
        }
        [JsonProperty("LevelName",Order = 1)]
        private string m_levelName;
        [JsonProperty("AuthorName",Order = 2)]
        private string m_authorName;
        [JsonProperty("Introduction",Order = 3)]
        private string m_introduction;
        [JsonProperty("CreateDate",Order = 4)]
        private DateTime m_createDate;
        [JsonProperty("SubLevelDatas",Order = 5)]
        private List<SubLevelData> m_subLevelDatas = new List<SubLevelData>();
        
        public LevelData(string name)
        {
            m_createDate = DateTime.Now;
            m_levelName = name;
        }
    }
}
