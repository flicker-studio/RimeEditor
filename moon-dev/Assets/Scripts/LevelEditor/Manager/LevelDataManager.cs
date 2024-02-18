using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using Moon.Kernel;
using Moon.Kernel.Setting;
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
        public SubLevelData? CurrentSubLevel
        {
            get
            {
                if (m_levelDatas.Count <= m_levelIndex) return null;
                var subLevels = m_levelDatas[m_levelIndex].SubLevelDatas;
                if (subLevels is null) return null;
                if (subLevels.Count <= CurrentSubLevelIndex) return null;
                return subLevels[CurrentSubLevelIndex];
            }
        }

        /// <summary>
        /// </summary>
        [CanBeNull]
        public List<AbstractItem> ItemAssets
        {
            get
            {
                if (m_levelDatas.Count <= m_levelIndex) return null;
                var subLevels = m_levelDatas[m_levelIndex].SubLevelDatas;
                if (subLevels is null) return null;
                if (subLevels.Count <= CurrentSubLevelIndex) return null;
                return subLevels[CurrentSubLevelIndex].ItemAssets;
            }
        }

        /// <summary>
        /// </summary>
        public readonly List<AbstractItem> TargetItems = new();

        /// <summary>
        /// </summary>
        public List<GameObject> TargetObjs = new();

        /// <summary>
        /// </summary>
        public Action<SubLevelData> SyncLevelData;

        private                  int                m_levelIndex;
        [UsedImplicitly] private List<LevelData>    m_levelDatas = new();
        private                  List<SubLevelData> SubLevelDatas => CurrentLevel.SubLevelDatas;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public LevelDataManager()
        {
            // TargetItems.OnAddRange += SyncTargetObj;
            //TargetItems.OnAdd      += SyncTargetObj;
            //TargetItems.OnClear    += SyncTargetObj;
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

        public SubLevelData AddSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDatas.Add(new SubLevelData($"Level {SubLevelDatas.Count}"));
            CurrentSubLevelIndex = SubLevelDatas.Count - 1;
            SetItemAssetActive(ItemAssets, true);
            if (CurrentSubLevel is null) return default;
            SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
            return SubLevelDatas.Last();
        }

        public List<SubLevelData> SetSubLevels(List<SubLevelData> levelDatas)
        {
            SubLevelDatas.Clear();
            SubLevelDatas.AddRange(levelDatas);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevelDatas.Count - 1);
            if (CurrentSubLevel != null) SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
            return SubLevelDatas;
        }

        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDatas.RemoveAt(CurrentSubLevelIndex);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true);
            if (CurrentSubLevel is null) return;
            SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
        }

        public void SetSubLevelIndex(int index, bool isReload = false)
        {
            if (CurrentSubLevelIndex == index && !isReload) return;
            if (isReload)
                foreach (var levelData in SubLevelDatas)
                    SetItemAssetActive(levelData.ItemAssets, false, true);
            else
                SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            CurrentSubLevelIndex = Mathf.Clamp(index, 0, SubLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true, isReload);
            if (CurrentSubLevel != null) SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
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
            var setting = Explorer.TryGetSetting<GlobalSetting>();
            m_levelDatas = await LevelDataLoader.LoadLevelDatas(setting);
        }

        public bool OpenLocalLevelDirectory(string path)
        {
            var setting = Explorer.TryGetSetting<GlobalSetting>();
            LevelDataLoader.LoadArchive(setting, path).Forget();
            return true;
        }

        public void ToJson()
        {
            if (CurrentSubLevel != null)
                foreach (var itemAsset in ((SubLevelData)CurrentSubLevel).ItemAssets)
                {
                    //itemAsset.GetTransformToData();
                }

            LevelDataLoader.ToJson(CurrentLevel);
        }

        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            return LevelDataLoader.Deserialize(json);
        }

        public void CreateLevel()
        {
            var tempLevelData = new LevelData();

            // m_levelDatas.Add(tempLevelData);
            //TODO: ?
            CurrentSubLevelIndex = 0;
            SubLevelDatas.Add(new SubLevelData($"Level {SubLevelDatas.Count}"));
        }

        public void OpenLevel(LevelData levelData)
        {
            m_levelIndex = m_levelDatas.IndexOf(levelData);
            SetSubLevelIndex(0, true);
        }

        public bool DeleteLevel(LevelData levelData)
        {
            return LevelDataLoader.Delete(levelData);
        }

        [UsedImplicitly]
        private void ClearLevel()
        {
            foreach (var subLevelData in SubLevelDatas)
            foreach (var itemAsset in subLevelData.ItemAssets)
                itemAsset.Inactive();
        }

        private void SetItemAssetActive(List<AbstractItem> itemDatas, bool active, bool isReload = false)
        {
            foreach (var itemData in itemDatas) itemData.Active(active);
        }

      

        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }
    }
}