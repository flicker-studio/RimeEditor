using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    public delegate void SyncLevelData(SubLevelData subLevelData);
    public class DataManager
    {
        public ObservableList<ItemData> TargetItems = new ObservableList<ItemData>();

        public List<GameObject> TargetObjs = new List<GameObject>();

        public ObservableList<ItemData> ItemAssets => GetCurrentSubLevel.ItemAssets;
        
        public SubLevelData GetCurrentSubLevel => m_levelDatas[m_index];

        public int GetCurrentIndex => m_index;

        public SyncLevelData SyncLevelData;

        public event Action ReloadLevelAction;

        public void SetActiveEditors(bool value)
        {
            foreach (var itemAsset in ItemAssets)
            {
                itemAsset.SetActiveEditor(value);
            }
        }
        
        public SubLevelData AddLevel()
        {
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_levelDatas.Add(new SubLevelData($"Level {m_levelDatas.Count}"));
            m_index = m_levelDatas.Count - 1;
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return m_levelDatas.Last();
        }

        public List<SubLevelData> SetLevels(List<SubLevelData> levelDatas)
        {
            m_levelDatas.Clear();
            m_levelDatas.AddRange(levelDatas);
            m_index = Mathf.Clamp(m_index, 0, m_levelDatas.Count - 1);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return m_levelDatas;
        }

        public void DeleteLevel()
        {
            SetItemAssetActive(ItemAssets,false);
            TargetItems.Clear();
            m_levelDatas.RemoveAt(m_index);
            m_index = Mathf.Clamp(m_index, 0, m_levelDatas.Count - 1);
            SetItemAssetActive(ItemAssets,true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public void SetLevelIndex(int index, bool isReload = false)
        {
            if(m_index == index && !isReload) return;
            if (isReload)
            {
                for (var i = 0; i < m_levelDatas.Count; i++)
                {
                    SetItemAssetActive(m_levelDatas[i].ItemAssets,false,isReload);
                }
            }
            else
            {
                SetItemAssetActive(ItemAssets,false,isReload);
            }
            TargetItems.Clear();
            m_index = Mathf.Clamp(index, 0, m_levelDatas.Count - 1);
            SetItemAssetActive(ItemAssets,true,isReload);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public List<SubLevelData> ShowLevels()
        {
            List<SubLevelData> tempList = new List<SubLevelData>();
            tempList.AddRange(m_levelDatas);
            return tempList;
        }
        
        public void ToJson()
        {
            SetActiveEditors(false);
            LevelData levelData = new LevelData("123");
            levelData.SetSubLevelDatas = m_levelDatas;
            string json = JsonConvert.SerializeObject(levelData, Formatting.Indented);
            m_levelDatas = JsonConvert.DeserializeObject<LevelData>(json).GetSubLevelDatas;
            SetLevelIndex(0, true);
            ReloadLevelAction?.Invoke();
        }

        public void FromJson(string bytes)
        {
            
        }
        
        private List<SubLevelData> m_levelDatas = new List<SubLevelData>();

        private int m_index;

        public DataManager()
        {
            InitLevel();
            InitEvent();
        }

        private void ClearLevel()
        {
            foreach (var subLevelData in m_levelDatas)
            {
                foreach (var itemAsset in subLevelData.ItemAssets)
                {
                    itemAsset.SetActiveEditor(false);
                }
            }
        }

        private void InitLevel()
        {
            m_index = 0;
            m_levelDatas.Add(new SubLevelData($"Level {m_levelDatas.Count - 1}"));
        }
        
        private void InitEvent()
        {
            TargetItems.OnAddRange += SyncTargetObj;
            TargetItems.OnAdd += SyncTargetObj;
            TargetItems.OnClear += SyncTargetObj;
        }
        
        private void SetItemAssetActive(ObservableList<ItemData> itemDatas,bool active,bool isReload = false)
        {
            foreach (var itemData in itemDatas)
            {
                itemData.SetActiveEditor(active,isReload);
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
