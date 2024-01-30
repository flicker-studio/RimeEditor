using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    ///     Manage the loading of resources and datas
    /// </summary>
    public class DataManager : IManager
    {
        public ObservableList<ItemDataBase> ItemAssets => GetCurrentSubLevel?.ItemAssets;

        public SubLevelData GetCurrentSubLevel => GetCurrentLevel?.GetSubLevelDatas[GetCurrentSubLevelIndex];

        public LevelData GetCurrentLevel { get; private set; }

        public List<LevelData> GetAllLevels => m_levelDatas;

        public int GetCurrentSubLevelIndex { get; private set; }

        public ObservableList<ItemDataBase> TargetItems = new();

        public List<GameObject> TargetObjs = new();

        public Action<SubLevelData> SyncLevelData;

        private List<SubLevelData> SubLevelDatas => GetCurrentLevel.GetSubLevelDatas;

        private List<LevelData> m_levelDatas = new();

        /// <summary>
        ///     Default constructor
        /// </summary>
        public DataManager()
        {
            RegisterEvent();
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
            m_levelDatas = await LevelLoader.LoadLevelDatas();
        }

        public bool OpenLocalLevelDirectory(string path)
        {
            return LevelLoader.OpenLocalLevelDirectory(path, ref m_levelDatas);
        }

        public void ToJson()
        {
            foreach (var itemAsset in GetCurrentSubLevel.ItemAssets) itemAsset.GetTransformToData();

            LevelLoader.ToJson(GetCurrentLevel);
        }

        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            return LevelLoader.Deserialize(json);
        }

        public void CreateLevel()
        {
            GetCurrentLevel = new LevelData();
            InitLevel();
        }

        public void OpenLevel(LevelData levelData)
        {
            GetCurrentLevel = levelData;
            SetSubLevelIndex(0, true);
        }

        public bool DeleteLevel(LevelData levelData)
        {
            return LevelLoader.DeleteLevel(levelData);
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


        private void RegisterEvent()
        {
            TargetItems.OnAddRange += SyncTargetObj;
            TargetItems.OnAdd += SyncTargetObj;
            TargetItems.OnClear += SyncTargetObj;
        }

        private void SetItemAssetActive(ObservableList<ItemDataBase> itemDatas, bool active, bool isReload = false)
        {
            foreach (var itemData in itemDatas) itemData.SetActiveEditor(active, isReload);
        }

        private void SyncTargetObj(List<ItemDataBase> list)
        {
            SyncTargetObj();
        }

        private void SyncTargetObj(ItemDataBase list)
        {
            SyncTargetObj();
        }

        private void SyncTargetObj()
        {
            TargetObjs = TargetItems.GetItemObjs();
        }
    }
}