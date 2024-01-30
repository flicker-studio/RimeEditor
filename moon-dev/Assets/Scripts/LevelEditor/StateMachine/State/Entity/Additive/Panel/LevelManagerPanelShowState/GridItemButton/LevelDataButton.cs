using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    public class LevelDataButton : GridItemButton
    {
        public LevelData GetLevelData => m_levelData;

        private LevelData m_levelData;

        private TextMeshProUGUI m_levelName;

        private TextMeshProUGUI m_levelPath;

        private RawImage m_levelCoverImage;

        public LevelDataButton(GameObject buttonPrefab, Action<GridItemButton> onSelect, Transform parent, ScrollRect scrollRect,
            LevelData levelData, string levelTextName, string levelPathTextName, string levelImageName)
            : base(buttonPrefab, onSelect, parent, scrollRect)
        {
            m_levelName = m_buttonObj.transform.Find(levelTextName).GetComponent<TextMeshProUGUI>();
            m_levelPath = m_buttonObj.transform.Find(levelPathTextName).GetComponent<TextMeshProUGUI>();
            m_levelCoverImage = m_buttonObj.transform.Find(levelImageName).GetComponent<RawImage>();
            m_levelData = levelData;
            m_levelName.text = m_levelData.GetName;
            m_levelPath.text = m_levelData.Path;

            if (m_levelData.GetLevelCoverImage != null)
            {
                m_levelCoverImage.texture = m_levelData.GetLevelCoverImage;
            }
        }
    }
}