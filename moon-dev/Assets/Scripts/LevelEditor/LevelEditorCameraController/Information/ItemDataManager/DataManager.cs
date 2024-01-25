using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using UnityEngine;

namespace LevelEditor
{
    public delegate void SyncLevelData(SubLevelData subLevelData);

    public class DataManager
    {
        public ObservableList<ItemData> TargetItems = new ObservableList<ItemData>();

        public List<GameObject> TargetObjs = new List<GameObject>();

        public ObservableList<ItemData> ItemAssets => GetCurrentSubLevel?.ItemAssets;

        public SubLevelData GetCurrentSubLevel => GetCurrentLevel?.GetSubLevelDatas[m_subLevelIndex];

        public LevelData GetCurrentLevel => m_chooseLevelData;

        public List<LevelData> GetAllLevels => m_levelDatas;

        public int GetCurrentSubLevelIndex => m_subLevelIndex;

        public SyncLevelData SyncLevelData;

        public void SetActiveEditors(bool value)
        {
            if (ItemAssets == null) return;

            foreach (var itemAsset in ItemAssets)
            {
                itemAsset.SetActiveEditor(value);
            }
        }

        public SubLevelData AddSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            m_subLevelDatas.Add(new SubLevelData($"Level {m_subLevelDatas.Count}"));
            m_subLevelIndex = m_subLevelDatas.Count - 1;
            SetItemAssetActive(ItemAssets, true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return m_subLevelDatas.Last();
        }

        public List<SubLevelData> SetSubLevels(List<SubLevelData> levelDatas)
        {
            m_subLevelDatas.Clear();
            m_subLevelDatas.AddRange(levelDatas);
            m_subLevelIndex = Mathf.Clamp(m_subLevelIndex, 0, m_subLevelDatas.Count - 1);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return m_subLevelDatas;
        }

        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            m_subLevelDatas.RemoveAt(m_subLevelIndex);
            m_subLevelIndex = Mathf.Clamp(m_subLevelIndex, 0, m_subLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public void SetSubLevelIndex(int index, bool isReload = false)
        {
            if (m_subLevelIndex == index && !isReload) return;

            if (isReload)
            {
                for (var i = 0; i < m_subLevelDatas.Count; i++)
                {
                    SetItemAssetActive(m_subLevelDatas[i].ItemAssets, false, isReload);
                }
            }
            else
            {
                SetItemAssetActive(ItemAssets, false, isReload);
            }

            TargetItems.Clear();
            m_subLevelIndex = Mathf.Clamp(index, 0, m_subLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true, isReload);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public List<SubLevelData> ShowSubLevels()
        {
            List<SubLevelData> tempList = new List<SubLevelData>();
            tempList.AddRange(m_subLevelDatas);
            return tempList;
        }

        public async UniTask LoadLevelFiles()
        {
            SetActiveEditors(false);
            await m_levelLoader.LoadLevelFiles(m_levelDatas);
        }

        public bool OpenLocalLevelDirectory(string path)
        {
            return m_levelLoader.OpenLocalLevelDirectory(path, m_levelDatas);
        }

        public void ToJson()
        {
            foreach (var itemAsset in GetCurrentSubLevel.ItemAssets)
            {
                itemAsset.GetTransformToData();
            }

            m_levelLoader.ToJson(m_chooseLevelData);
        }

        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            return m_levelLoader.FromJson(json);
        }

        public void CreateLevel()
        {
            m_chooseLevelData = new LevelData();
            InitLevel();
        }

        public void OpenLevel(LevelData levelData)
        {
            m_chooseLevelData = levelData;
            InitLevel(levelData);
        }

        public bool DeleteLevel(LevelData levelData)
        {
            return m_levelLoader.DeleteLevel(levelData);
        }

        private List<SubLevelData> m_subLevelDatas => GetCurrentLevel.GetSubLevelDatas;

        private List<LevelData> m_levelDatas = new List<LevelData>();

        private LevelData m_chooseLevelData;

        private LevelLoader m_levelLoader = new LocalLevelLoader();

        private int m_subLevelIndex;

        public DataManager()
        {
            InitEvent();
        }

        private void ClearLevel()
        {
            foreach (var subLevelData in m_subLevelDatas)
            {
                foreach (var itemAsset in subLevelData.ItemAssets)
                {
                    itemAsset.SetActiveEditor(false);
                }
            }
        }

        private void InitLevel()
        {
            m_subLevelIndex = 0;
            m_subLevelDatas.Add(new SubLevelData($"Level {m_subLevelDatas.Count}"));
        }

        private void InitLevel(LevelData levelData)
        {
            SetSubLevelIndex(0, true);
        }

        private void InitEvent()
        {
            TargetItems.OnAddRange += SyncTargetObj;
            TargetItems.OnAdd += SyncTargetObj;
            TargetItems.OnClear += SyncTargetObj;
        }

        private void SetItemAssetActive(ObservableList<ItemData> itemDatas, bool active, bool isReload = false)
        {
            foreach (var itemData in itemDatas)
            {
                itemData.SetActiveEditor(active, isReload);
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