using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelEditor
{
    public delegate void SyncLevelData(LevelData levelData);
    public class DataManager
    {
        public ObservableList<ItemData> TargetItems = new ObservableList<ItemData>();

        public List<GameObject> TargetObjs = new List<GameObject>();

        public ObservableList<ItemData> ItemAssets => GetCurrentLevel.ItemAssets;
        
        public LevelData GetCurrentLevel => m_levelDatas[m_index];

        public int GetCurrentIndex => m_index;

        public SyncLevelData SyncLevelData;

        public void SetItemDatasActive(bool value)
        {
            foreach (var itemAsset in ItemAssets)
            {
                itemAsset.SetActiveEditor(value);
            }
        }
        
        public LevelData AddLevel()
        {
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_levelDatas.Add(new LevelData($"Level {m_levelDatas.Count}"));
            m_index = m_levelDatas.Count - 1;
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentLevel);
            return m_levelDatas.Last();
        }

        public List<LevelData> SetLevels(List<LevelData> levelDatas)
        {
            m_levelDatas.Clear();
            m_levelDatas.AddRange(levelDatas);
            m_index = Mathf.Clamp(m_index, 0, m_levelDatas.Count - 1);
            SyncLevelData?.Invoke(GetCurrentLevel);
            return m_levelDatas;
        }

        public void DeleteLevel()
        {
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_levelDatas.RemoveAt(m_index);
            m_index = Mathf.Clamp(m_index, 0, m_levelDatas.Count - 1);
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentLevel);
        }

        public void SetLevelIndex(int index)
        {
            if(m_index == index) return;
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_index = Mathf.Clamp(index, 0, m_levelDatas.Count - 1);
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentLevel);
        }

        public List<LevelData> ShowLevels()
        {
            List<LevelData> tempList = new List<LevelData>();
            tempList.AddRange(m_levelDatas);
            return tempList;
        }
        
        private List<LevelData> m_levelDatas = new List<LevelData>();

        private int m_index;

        public DataManager()
        {
            InitLevel();
            InitEvent();
        }

        private void InitLevel()
        {
            m_index = 0;
            m_levelDatas.Add(new LevelData($"Level {m_levelDatas.Count - 1}"));
        }
        
        private void InitEvent()
        {
            TargetItems.OnAddRange += SyncTargetObj;
            TargetItems.OnAdd += SyncTargetObj;
            TargetItems.OnClear += SyncTargetObj;
        }
        
        private void SetItemAssetActive(ObservableList<ItemData> itemDatas,bool active)
        {
            foreach (var itemData in itemDatas)
            {
                itemData.SetActiveEditor(active);
            }
        }

        private void SyncTargetObj(List<ItemData> list)
        {
            SyncTargetObj();
        }
        
        private void SyncTargetObj(ItemData list)
        {
            SyncTargetObj();
        }
        
        private void SyncTargetObj()
        {
            TargetObjs = TargetItems.GetItemObjs();
        }
    }
}
