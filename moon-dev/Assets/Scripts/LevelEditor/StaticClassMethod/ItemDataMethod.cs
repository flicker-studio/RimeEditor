using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public static class ItemDataMethod
    {
        public static ItemData CheckItemObj(this List<ItemData> itemDatas,GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
            {
                if (itemData.GetItemObjEditor == targetObj) return itemData;
            }

            return null;
        }

        public static List<ItemData> CheckItemObjs(this List<ItemData> itemDatas, List<GameObject> targetObjs)
        {
            List<ItemData> tempList = new List<ItemData>();
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

        public static List<GameObject> GetItemObjs(this List<ItemData> itemDatas)
        {
            List<GameObject> itemObjs = new List<GameObject>();
            foreach (var itemData in itemDatas)
            {
                itemObjs.Add(itemData.GetItemObjEditor);
            }

            return itemObjs;
        }
        
        public static ItemData CheckItemObj(this ObservableList<ItemData> itemDatas,GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
            {
                if (itemData.GetItemObjEditor == targetObj) return itemData;
            }

            return null;
        }

        public static List<ItemData> CheckItemObjs(this ObservableList<ItemData> itemDatas, List<GameObject> targetObjs)
        {
            List<ItemData> tempList = new List<ItemData>();
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

        public static List<GameObject> GetItemObjs(this ObservableList<ItemData> itemDatas)
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
