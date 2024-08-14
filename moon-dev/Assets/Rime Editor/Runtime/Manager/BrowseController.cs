using System;
using System.Collections.Generic;
using System.Linq;
using Cysharp.Threading.Tasks;
using JetBrains.Annotations;
using LevelEditor;
using LevelEditor.Item;
using UnityEngine;

namespace RimeEditor.Runtime
{
    /// <summary>
    ///     Used to control level selection, editing, and switching.
    /// </summary>
    /// <inheritdoc />
    public class BrowseController : IManager
    {
        /// <summary>
        ///     All custom levels detected
        /// </summary>
        public readonly List<CustomLevel> CustomLevels = new();

        /// <summary>
        ///     The item is currently identified as selected
        /// </summary>
        public readonly List<ItemBase> SelectedItems = new();

        /// <summary>
        /// </summary>
        public Action<SubLevel> SyncLevelData;

        /// <summary>
        /// </summary>
        public List<GameObject> TargetObjs = new();

        /// <summary>
        ///     Default constructor
        /// </summary>
        public BrowseController()
        {
            // TargetItems.OnAddRange += SyncTargetObj;
            // TargetItems.OnAdd      += SyncTargetObj;
            // TargetItems.OnClear    += SyncTargetObj;
        }

        /// <summary>
        /// </summary>
        public List<SubLevel> SubLevels =>
            CurrentCustomLevel?.Data.SubLevelDataList;

        /// <summary>
        /// </summary>
        public int CustomLevelIndex { get; private set; }

        /// <summary>
        /// </summary>
        public int CurrentSubLevelIndex { get; private set; }

        /// <summary>
        ///     The currently selected custom level
        /// </summary>
        [CanBeNull]
        public CustomLevel CurrentCustomLevel =>
            CustomLevelIndex == -1 ? null : CustomLevels[CustomLevelIndex];

        /// <summary>
        ///     The currently selected sublevel in custom level
        /// </summary>
        public SubLevel CurrentSubLevel =>
            CurrentCustomLevel?.Data.SubLevelDataList[CurrentSubLevelIndex];

        /// <summary>
        /// </summary>
        [CanBeNull]
        public List<ItemBase> ItemAssets
        {
            get
            {
                if (CustomLevels.Count <= CustomLevelIndex) return null;
                var subLevels = CustomLevels[CustomLevelIndex].Data.SubLevelDataList;
                if (subLevels is null) return null;
                if (subLevels.Count <= CurrentSubLevelIndex) return null;
                return subLevels[CurrentSubLevelIndex].ItemAssets;
            }
        }

        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }

        public void Save(string levelName = null, string author = null, string introduction = null, string version = null, Texture2D cover = null)
        {
        }

        /// <summary>
        ///     Load all files on the disk to the level list.
        ///     When you call the method, the files on the disk are rechecked once
        /// </summary>
        public void Refresh()
        {
            SetActiveEditors(false);
            // m_levelDatas = await DataLoader.LoadLocal();
        }

        /// <summary>
        ///     Import an archive file to level list
        /// </summary>
        /// <param name="filePath">
        ///     The path to archive file
        /// </param>
        /// <returns>
        ///     If the import is successful, the corresponding CustomLevel is returned
        ///     Otherwise, an error will be thrown
        /// </returns>
        /// <exception cref="NotImplementedException"></exception>
        public CustomLevel Import(string filePath)
        {
            var data = DataLoader.LoadArchive(filePath);
            //return data.Result;
            throw new NotImplementedException();
        }

        /// <summary>
        ///     Create a custom level and save it on disk
        /// </summary>
        /// <returns>
        ///     Return to the newly created custom level
        /// </returns>
        public CustomLevel Create()
        {
            var temp_level = new CustomLevel();

            // m_levelDatas.Add(tempLevelData);
            // CurrentSubLevelIndex = 0;
            //SubLevels.Add(new SubLevel($"Level {SubLevels.Count}"));

            return temp_level;
        }

        /// <summary>
        ///     Open the currently selected level
        /// </summary>
        public void Open()
        {
            CustomLevelIndex = CustomLevels.IndexOf(level);
            SwitchSubLevel(0, true);
        }

        /// <summary>
        ///     Delete the currently selected from disk
        /// </summary>
        public void Delete()
        {
            //  return DataLoader.Delete(levelData);
        }

        [UsedImplicitly]
        private void ClearLevel()
        {
            foreach (var subLevelData in SubLevels)
            foreach (var itemAsset in subLevelData.ItemAssets)
                itemAsset.Inactive();
        }

        public void SetItemAssetActive(List<ItemBase> itemDatas, bool active, bool isReload = false)
        {
            foreach (var itemData in itemDatas) itemData.Active(active);
        }

        #region Sublevel Part

        public void SetActiveEditors(bool value)
        {
        }

        public SubLevel AddSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            SelectedItems.Clear();
            SubLevels.Add(new SubLevel());
            CurrentSubLevelIndex = SubLevels.Count - 1;
            SetItemAssetActive(ItemAssets, true);
            if (CurrentSubLevel is null) return default;
            SyncLevelData?.Invoke(CurrentSubLevel);
            return SubLevels.Last();
        }

        public List<SubLevel> SetSubLevels(List<SubLevel> levelDatas)
        {
            SubLevels.Clear();
            SubLevels.AddRange(levelDatas);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevels.Count - 1);
            if (CurrentSubLevel != null) SyncLevelData?.Invoke(CurrentSubLevel);
            return SubLevels;
        }

        /// <summary>
        ///     Deletes the selected sublevel
        /// </summary>
        public void DeleteSubLevel()
        {
            SetItemAssetActive(ItemAssets, false);
            SelectedItems.Clear();
            SubLevels.RemoveAt(CurrentSubLevelIndex);
            CurrentSubLevelIndex = Mathf.Clamp(CurrentSubLevelIndex, 0, SubLevels.Count - 1);
            SetItemAssetActive(ItemAssets, true);
            if (CurrentSubLevel is null) return;
            SyncLevelData?.Invoke(CurrentSubLevel);
        }

        /// <summary>
        ///     Switch to a specific sublevel
        /// </summary>
        /// <param name="index">Index of specific sublevel</param>
        /// <param name="isReload"></param>
        public void SwitchSubLevel(int index, bool isReload = false)
        {
            if (CurrentSubLevelIndex == index && !isReload) return;
            if (isReload)
                foreach (var sub_level in SubLevels)
                    SetItemAssetActive(sub_level.ItemAssets, false, true);
            else
                SetItemAssetActive(ItemAssets, false);

            SelectedItems.Clear();
            CurrentSubLevelIndex = Mathf.Clamp(index, 0, SubLevels.Count - 1);
            SetItemAssetActive(ItemAssets, true, isReload);
            if (CurrentSubLevel != null) SyncLevelData?.Invoke(CurrentSubLevel);
        }

        public List<SubLevel> ShowSubLevels()
        {
            var tempList = new List<SubLevel>();
            tempList.AddRange(SubLevels);
            return tempList;
        }

        #endregion
    }
}