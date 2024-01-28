using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace LevelEditor
{
    public class DataManager : IManager
    {
        public ObservableList<ItemData> ItemAssets => GetCurrentSubLevel?.ItemAssets;

        public SubLevelData GetCurrentSubLevel => GetCurrentLevel?.GetSubLevelDatas[GetCurrentSubLevelIndex];

        public LevelData GetCurrentLevel { get; private set; }

        public List<LevelData> GetAllLevels { get; } = new();

        public int GetCurrentSubLevelIndex { get; private set; }

        public ObservableList<ItemData> TargetItems = new();

        public List<GameObject> TargetObjs = new();

        public Action<SubLevelData> SyncLevelData;

        private List<SubLevelData> SubLevelDatas => GetCurrentLevel.GetSubLevelDatas;

        private readonly LevelLoader m_levelLoader = new LocalLevelLoader();

        public DataManager()
        {
            InitEvent();
        }

        public void SetActiveEditors(bool value)
        {
            if (ItemAssets == null)
            {
                return;
            }

            foreach (var itemAsset in ItemAssets) itemAsset.SetActiveEditor(value);
        }

        public SubLevelData AddSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDatas.Add(new SubLevelData($"Level {SubLevelDatas.Count}"));
            GetCurrentSubLevelIndex = SubLevelDatas.Count - 1;
            SetItemAssetActive(ItemAssets, true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return SubLevelDatas.Last();
        }

        public List<SubLevelData> SetSubLevels(List<SubLevelData> levelDatas)
        {
            SubLevelDatas.Clear();
            SubLevelDatas.AddRange(levelDatas);
            GetCurrentSubLevelIndex = Mathf.Clamp(GetCurrentSubLevelIndex, 0, SubLevelDatas.Count - 1);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
            return SubLevelDatas;
        }

        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDatas.RemoveAt(GetCurrentSubLevelIndex);
            GetCurrentSubLevelIndex = Mathf.Clamp(GetCurrentSubLevelIndex, 0, SubLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public void SetSubLevelIndex(int index, bool isReload = false)
        {
            if (GetCurrentSubLevelIndex == index && !isReload)
            {
                return;
            }

            if (isReload)
            {
                foreach (var levelData in SubLevelDatas) SetItemAssetActive(levelData.ItemAssets, false, true);
            }
            else
            {
                SetItemAssetActive(ItemAssets, false);
            }

            TargetItems.Clear();
            GetCurrentSubLevelIndex = Mathf.Clamp(index, 0, SubLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true, isReload);
            SyncLevelData?.Invoke(GetCurrentSubLevel);
        }

        public List<SubLevelData> ShowSubLevels()
        {
            var tempList = new List<SubLevelData>();
            tempList.AddRange(SubLevelDatas);
            return tempList;
        }

        public async UniTask LoadLevelFiles()
        {
            SetActiveEditors(false);
            await m_levelLoader.LoadLevelFiles(GetAllLevels);
        }

        public bool OpenLocalLevelDirectory(string path)
        {
            return m_levelLoader.OpenLocalLevelDirectory(path, GetAllLevels);
        }

        public void ToJson()
        {
            foreach (var itemAsset in GetCurrentSubLevel.ItemAssets) itemAsset.GetTransformToData();

            m_levelLoader.ToJson(GetCurrentLevel);
        }

        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            return m_levelLoader.FromJson(json);
        }

        public void CreateLevel()
        {
            GetCurrentLevel = new LevelData();
            InitLevel();
        }

        public void OpenLevel(LevelData levelData)
        {
            GetCurrentLevel = levelData;
            InitLevel(levelData);
        }

        public bool DeleteLevel(LevelData levelData)
        {
            return m_levelLoader.DeleteLevel(levelData);
        }

        [UsedImplicitly]
        private void ClearLevel()
        {
            foreach (var subLevelData in SubLevelDatas)
            foreach (var itemAsset in subLevelData.ItemAssets)
                itemAsset.SetActiveEditor(false);
        }

        private void InitLevel()
        {
            GetCurrentSubLevelIndex = 0;
            SubLevelDatas.Add(new SubLevelData($"Level {SubLevelDatas.Count}"));
        }

        private void InitLevel([UsedImplicitly] LevelData levelData)
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
            foreach (var itemData in itemDatas) itemData.SetActiveEditor(active, isReload);
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