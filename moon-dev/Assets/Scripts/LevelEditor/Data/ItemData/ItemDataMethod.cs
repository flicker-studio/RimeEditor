using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public static class ItemDataMethod
    {
        public static ItemDataBase CheckItemObj(this List<ItemDataBase> itemDatas, GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
            {
                if (itemData.GetItemObjEditor == targetObj) return itemData;
            }

            return null;
        }

        public static List<ItemDataBase> CheckItemObjs(this List<ItemDataBase> itemDatas, List<GameObject> targetObjs)
        {
            var tempList = new List<ItemDataBase>();

            foreach (var targetObj in targetObjs)
            {
                foreach (var itemData in itemDatas)
                {
                    if (itemData.GetItemObjEditor == targetObj)
                    {
                        tempList.Add(itemData);
                        break;
                    }
                }
            }

            return tempList;
        }

        public static List<GameObject> GetItemObjs(this List<ItemDataBase> itemDatas)
        {
            List<GameObject> itemObjs = new List<GameObject>();

            foreach (var itemData in itemDatas)
            {
                itemObjs.Add(itemData.GetItemObjEditor);
            }

            return itemObjs;
        }

        public static ItemDataBase CheckItemObj(this ObservableList<ItemDataBase> itemDatas, GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
            {
                if (itemData.GetItemObjEditor == targetObj) return itemData;
            }

            return null;
        }

        public static List<ItemDataBase> CheckItemObjs(this ObservableList<ItemDataBase> itemDatas, List<GameObject> targetObjs)
        {
            var tempList = new List<ItemDataBase>();

            foreach (var targetObj in targetObjs)
            {
                foreach (var itemData in itemDatas)
                {
                    if (itemData.GetItemObjEditor == targetObj)
                    {
                        tempList.Add(itemData);
                        break;
                    }
                }
            }

            return tempList;
        }

        public static List<GameObject> GetItemObjs(this ObservableList<ItemDataBase> itemDatas)
        {
            List<GameObject> itemObjs = new List<GameObject>();

            foreach (var itemData in itemDatas)
            {
                itemObjs.Add(itemData.GetItemObjEditor);
            }

            return itemObjs;
        }
    }
}