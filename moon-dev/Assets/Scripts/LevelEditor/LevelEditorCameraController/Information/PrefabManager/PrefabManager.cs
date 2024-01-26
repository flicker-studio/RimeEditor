using Data.ScriptableObject;
using Moon.Kernel;
using UnityEngine;

namespace LevelEditor
{
    public class PrefabManager
    {
        public GameObject GetEmptyGameObject => m_prefabFactory.EMPTY_GAMEOBJECT;

        public GameObject GetItemNodeGameObject => m_prefabFactory.ITEM_NODE;

        public GameObject GetItemDetailGroup => m_prefabFactory.ITEM_DETAIL_GROUP;

        public GameObject GetItemLattice => m_prefabFactory.ITEM_LATTICE;

        public GameObject GetItemType => m_prefabFactory.ITEM_TYPE;

        public GameObject GetBoolItem => m_prefabFactory.BOOL_ITEM;

        public GameObject GetLevelItem => m_prefabFactory.LEVEL_DATA_BUTTON;

        private PrefabFactory m_prefabFactory;

        public PrefabManager()
        {
            m_prefabFactory = Explorer.TryGetSetting<PrefabFactory>();
        }
    }
}