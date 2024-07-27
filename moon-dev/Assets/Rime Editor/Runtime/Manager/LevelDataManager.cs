using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using LevelEditor.Item;
using RimeEditor.Runtime;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Manage the loading of resources and datas
    /// </summary>
    /// <inheritdoc />
    public class LevelDataManager : IManager
    {
        /// <summary>
        /// </summary>
        public readonly List<ItemBase> TargetItems = new();

        [UsedImplicitly] private List<LevelData> m_levelDatas = new();

        private int m_levelIndex;

        /// <summary>
        /// </summary>
        public Action<SubLevel> SyncLevelData;

        /// <summary>
        /// </summary>
        public List<GameObject> TargetObjs = new();

        /// <summary>
        ///     Default constructor
        /// </summary>
        public LevelDataManager()
        {
            // TargetItems.OnAddRange += SyncTargetObj;
            //TargetItems.OnAdd      += SyncTargetObj;
            //TargetItems.OnClear    += SyncTargetObj;
        }

        /// <summary>
        /// </summary>
        public List<LevelData> LevelDatas => m_levelDatas;

        /// <summary>
        /// </summary>
        public LevelData CurrentLevel => m_levelDatas[m_levelIndex];

        /// <summary>
        /// </summary>
        public int CurrentSubLevelIndex { get; private set; }

        /// <summary>
        /// </summary>
        [CanBeNull]
        public SubLevel? CurrentSubLevel
        {
            get
            {
                if (m_levelDatas.Count <= m_levelIndex) return null;
                var subLevels = m_levelDatas[m_levelIndex].SubLevelDataList;
                if (subLevels is null) return null;
                if (subLevels.Count <= CurrentSubLevelIndex) return null;
                return subLevels[CurrentSubLevelIndex];
            }
        }

        /// <summary>
        /// </summary>
        [CanBeNull]
        public List<ItemBase> ItemAssets
        {
            get
            {
                if (m_levelDatas.Count <= m_levelIndex) return null;
                var subLevels = m_levelDatas[m_levelIndex].SubLevelDataList;
                if (subLevels is null) return null;
                if (subLevels.Count <= CurrentSubLevelIndex) return null;
                return subLevels[CurrentSubLevelIndex].ItemAssets;
            }
        }

        private List<SubLevel> SubLevelDataList => CurrentLevel.SubLevelDataList;

        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }

        public void Save(string levelName = null, string author = null, string introduction = null, string version = null, Texture2D cover = null)
        {
            m_levelDatas[m_levelIndex].Update(levelName, author, introduction, version, cover);
        }

        public void SetActiveEditors(bool value)
        {
            if (ItemAssets == null) return;
            foreach (var itemAsset in ItemAssets) itemAsset.Active(value);
        }

        public SubLevel AddSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDataList.Add(new SubLevel($"Level {SubLevelDataList.Count}"));
            CurrentSubLevelIndex = SubLevelDataList.Count - 1;
            SetItemAssetActive(ItemAssets, true);
            if (CurrentSubLevel is null) return default;
            SyncLevelData?.Invoke((SubLevel)CurrentSubLevel);
            return SubLevelDataList.Last();
        }

        public List<SubLevel> SetSubLevels(List<SubLevel> levelDatas)
        {
            SubLevelDataList.Clear();
            SubLevelDataList.AddRange(levelDatas);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevelDataList.Count - 1);
            if (CurrentSubLevel != null) SyncLevelData?.Invoke((SubLevel)CurrentSubLevel);
            return SubLevelDataList;
        }

        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDataList.RemoveAt(CurrentSubLevelIndex);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevelDataList.Count - 1);
            SetItemAssetActive(ItemAssets, true);
            if (CurrentSubLevel is null) return;
            SyncLevelData?.Invoke((SubLevel)CurrentSubLevel);
        }

        public void SetSubLevelIndex(int index, bool isReload = false)
        {
            if (CurrentSubLevelIndex == index && !isReload) return;
            if (isReload)
                foreach (var levelData in SubLevelDataList)
                    SetItemAssetActive(levelData.ItemAssets, false, true);
            else
                SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            CurrentSubLevelIndex = Mathf.Clamp(index, 0, SubLevelDataList.Count - 1);
            SetItemAssetActive(ItemAssets, true, isReload);
            if (CurrentSubLevel != null) SyncLevelData?.Invoke((SubLevel)CurrentSubLevel);
        }

        public List<SubLevel> ShowSubLevels()
        {
            var tempList = new List<SubLevel>();
            tempList.AddRange(SubLevelDataList);
            return tempList;
        }

        public async UniTask LoadLevelFiles()
        {
            SetActiveEditors(false);
            // m_levelDatas = await DataLoader.LoadLocal();
        }

        /// <summary>
        ///     Open a local zip file to load the level
        /// </summary>
        /// <param name="zip_path">The path to the directory</param>
        /// <returns></returns>
        public LevelData OpenLocalLevel(string zip_path)
        {
            var data = DataLoader.LoadArchive(zip_path);
            //return data.Result;
            throw new NotImplementedException();
        }

        public void ToJson()
        {
            if (CurrentSubLevel != null)
                foreach (var itemAsset in ((SubLevel)CurrentSubLevel).ItemAssets)
                {
                    //itemAsset.GetTransformToData();
                }

            DataLoader.ToJson(CurrentLevel);
        }

        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            return DataLoader.Deserialize(json);
        }

        public void CreateLevel()
        {
            var tempLevelData = new LevelData();

            // m_levelDatas.Add(tempLevelData);
            //TODO: ?
            CurrentSubLevelIndex = 0;
            SubLevelDataList.Add(new SubLevel($"Level {SubLevelDataList.Count}"));
        }

        public void OpenLevel(LevelData levelData)
        {
            m_levelIndex = m_levelDatas.IndexOf(levelData);
            SetSubLevelIndex(0, true);
        }

        public bool DeleteLevel(LevelData levelData)
        {
            return DataLoader.Delete(levelData);
        }

        [UsedImplicitly]
        private void ClearLevel()
        {
            foreach (var subLevelData in SubLevelDataList)
            foreach (var itemAsset in subLevelData.ItemAssets)
                itemAsset.Inactive();
        }

        private void SetItemAssetActive(List<ItemBase> itemDatas, bool active, bool isReload = false)
        {
            foreach (var itemData in itemDatas) itemData.Active(active);
        }
    }
}