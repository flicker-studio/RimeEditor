using System;
using System.Collections.Generic;

namespace LevelEditor
{
    public class LevelData
    {
        public string GetName => m_levelName;

        public string GetTime => m_createDate.ToString();

        public string GetAuthorName => m_authorName;

        public string GetIntroduction => m_introduction;

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
        
        private string m_levelName;

        private string m_authorName;

        private string m_introduction;

        private DateTime m_createDate;

        private List<SubLevelData> m_subLevelDatas = new List<SubLevelData>();
        
        public LevelData(string name)
        {
            m_createDate = DateTime.Now;
            m_levelName = name;
        }
    }
}
