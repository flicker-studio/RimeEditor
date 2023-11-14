using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class DataManager
    {
        public ObservableList<ItemData> TargetItems = new ObservableList<ItemData>();

        public List<GameObject> TargetObjs = new List<GameObject>();

        public ObservableList<ItemData> ItemAssets = new ObservableList<ItemData>();

        public DataManager()
        {
            InitEvent();
        }
        
        private void InitEvent()
        {
            TargetItems.OnAddRange += SyncTargetObj;
            TargetItems.OnAdd += SyncTargetObj;
            TargetItems.OnClear += SyncTargetObj;
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
