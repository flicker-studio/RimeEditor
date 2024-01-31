using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Manage the loading of resources and datas
    /// </summary>
    /// <inheritdoc />
    public class DataManager : IManager
    {
        /// <summary>
        /// </summary>
        public List<LevelData> LevelDatas => m_levelDatas;

        /// <summary>
        /// </summary>
        public LevelData CurrentLevel => m_levelData;

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
                var subLevels = m_levelData.SubLevelDatas;

                if (subLevels is null)
                {
                    return null;
                }

                if (subLevels.Count <= CurrentSubLevelIndex)
                {
                    return null;
                }

                return subLevels[CurrentSubLevelIndex];
            }
        }

        /// <summary>
        /// </summary>
        [CanBeNull]
        public ObservableList<ItemDataBase> ItemAssets
        {
            get
            {
                var subLevels = m_levelData.SubLevelDatas;

                if (subLevels is null)
                {
                    return null;
                }

                if (subLevels.Count <= CurrentSubLevelIndex)
                {
                    return null;
                }

                return subLevels[CurrentSubLevelIndex].ItemAssets;
            }
        }

        /// <summary>
        /// </summary>
        public readonly ObservableList<ItemDataBase> TargetItems = new();

        /// <summary>
        /// </summary>
        public List<GameObject> TargetObjs = new();

        /// <summary>
        /// </summary>
        public Action<SubLevelData> SyncLevelData;

        private LevelData m_levelData;

        [UsedImplicitly]
        private List<LevelData> m_levelDatas = new();

        private List<SubLevelData> SubLevelDatas => CurrentLevel.SubLevelDatas;


        /// <summary>
        ///     Default constructor
        /// </summary>
        public DataManager()
        {
            RegisterEvent();
        }

        public void Save(string levelName = null, string author = null, string introduction = null, string version = null, Texture2D cover = null)
        {
            m_levelData.Update(levelName, author, introduction, version, cover);
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
            CurrentSubLevelIndex = SubLevelDatas.Count - 1;
            SetItemAssetActive(ItemAssets, true);

            if (CurrentSubLevel is null)
            {
                return default;
            }

            SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
            return SubLevelDatas.Last();
        }

        public List<SubLevelData> SetSubLevels(List<SubLevelData> levelDatas)
        {
            SubLevelDatas.Clear();
            SubLevelDatas.AddRange(levelDatas);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevelDatas.Count - 1);

            if (CurrentSubLevel != null)
            {
                SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
            }

            return SubLevelDatas;
        }

        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            TargetItems.Clear();
            SubLevelDatas.RemoveAt(CurrentSubLevelIndex);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true);

            if (CurrentSubLevel is null)
            {
                return;
            }

            SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
        }

        public void SetSubLevelIndex(int index, bool isReload = false)
        {
            if (CurrentSubLevelIndex == index && !isReload)
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
            CurrentSubLevelIndex = Mathf.Clamp(index, 0, SubLevelDatas.Count - 1);
            SetItemAssetActive(ItemAssets, true, isReload);

            if (CurrentSubLevel != null)
            {
                SyncLevelData?.Invoke((SubLevelData)CurrentSubLevel);
            }
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
            if (CurrentSubLevel != null)
            {
                foreach (var itemAsset in ((SubLevelData)CurrentSubLevel).ItemAssets)
                    itemAsset.GetTransformToData();
            }

            LevelLoader.ToJson(CurrentLevel);
        }

        public LevelData FromJson(string json)
        {
            SetActiveEditors(false);
            return LevelLoader.Deserialize(json);
        }

        public void CreateLevel()
        {
            m_levelData = new LevelData();
            InitLevel();
        }

        public void OpenLevel(LevelData levelData)
        {
            m_levelData = levelData;
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
            CurrentSubLevelIndex = 0;
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