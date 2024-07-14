using Cysharp.Threading.Tasks;
using LevelEditor.Settings;
using UnityEngine;

namespace LevelEditor
{
    public class PrefabManager : IManager
    {
        private readonly PrefabSetting _mPrefabSetting;

        public PrefabManager(PrefabSetting prefabSetting)
        {
            _mPrefabSetting = prefabSetting;
        }

        public GameObject GetEmptyGameObject => _mPrefabSetting.EMPTY_GAMEOBJECT;

        public GameObject GetItemNodeGameObject => _mPrefabSetting.ITEM_NODE;

        public GameObject GetItemDetailGroup => _mPrefabSetting.ITEM_DETAIL_GROUP;

        public GameObject GetItemLattice => _mPrefabSetting.ITEM_LATTICE;

        public GameObject GetItemType => _mPrefabSetting.ITEM_TYPE;

        public GameObject GetBoolItem => _mPrefabSetting.BOOL_ITEM;

        public GameObject GetLevelItem => _mPrefabSetting.LEVEL_DATA_BUTTON;

        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }
    }
}