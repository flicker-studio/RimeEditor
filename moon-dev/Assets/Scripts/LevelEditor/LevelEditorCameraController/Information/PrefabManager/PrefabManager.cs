using System.Collections;
using System.Collections.Generic;
using Data.ScriptableObject;
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

        private PrefabFactory m_prefabFactory;

        public PrefabManager()
        {
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
        }
    }
}