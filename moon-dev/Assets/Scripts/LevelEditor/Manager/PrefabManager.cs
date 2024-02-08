using Cysharp.Threading.Tasks;
using Data.ScriptableObject;
using UnityEngine;

namespace LevelEditor
{
    public class PrefabManager : IManager
    {
        public GameObject GetEmptyGameObject => m_prefabFactory.EMPTY_GAMEOBJECT;

        public GameObject GetItemNodeGameObject => m_prefabFactory.ITEM_NODE;

        public GameObject GetItemDetailGroup => m_prefabFactory.ITEM_DETAIL_GROUP;

        public GameObject GetItemLattice => m_prefabFactory.ITEM_LATTICE;

        public GameObject GetItemType => m_prefabFactory.ITEM_TYPE;

        public GameObject GetBoolItem => m_prefabFactory.BOOL_ITEM;

        public GameObject GetLevelItem => m_prefabFactory.LEVEL_DATA_BUTTON;

        private readonly PrefabFactory m_prefabFactory;

        public PrefabManager(PrefabFactory prefabFactory)
        {
            m_prefabFactory = prefabFactory;
        }

        public UniTask Initialization()
        {
            return UniTask.CompletedTask;
        }
    }
}