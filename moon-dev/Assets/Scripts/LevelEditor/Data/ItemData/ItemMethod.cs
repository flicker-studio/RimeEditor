using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public static class ItemMethod
    {
        public static AbstractItem CheckItemObj(this List<AbstractItem> itemDatas, GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
            {
                if (itemData.GameObject == targetObj) return itemData;
            }

            return null;
        }

        public static List<AbstractItem> CheckItemObjs(this List<AbstractItem> itemDatas, List<GameObject> targetObjs)
        {
            var tempList = new List<AbstractItem>();

            foreach (var targetObj in targetObjs)
            {
                foreach (var itemData in itemDatas)
                {
                    if (itemData.GameObject == targetObj)
                    {
                        tempList.Add(itemData);
                        break;
                    }
                }
            }

            return tempList;
        }

        public static List<GameObject> GetItemObjs(this List<AbstractItem> itemDatas)
        {
            List<GameObject> itemObjs = new List<GameObject>();

            foreach (var itemData in itemDatas)
            {
                itemObjs.Add(itemData.GameObject);
            }

            return itemObjs;
        }

        /// <summary>
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static (List<Vector3>, List<Quaternion>, List<Vector3>) GetTransforms(this List<AbstractItem> items)
        {
            var positionList = new List<Vector3>();
            var rotationList = new List<Quaternion>();
            var scaleList    = new List<Vector3>();

            foreach (var obj in items.GetItemObjs())
            {
                positionList.Add(obj.transform.position);
                rotationList.Add(obj.transform.rotation);
                scaleList.Add(obj.transform.localScale);
            }

            return (positionList, rotationList, scaleList);
        }

        public static AbstractItem CheckItemObj(this ObservableList<AbstractItem> itemDatas, GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
            {
                if (itemData.GameObject == targetObj) return itemData;
            }

            return null;
        }

        public static List<AbstractItem> CheckItemObjs(this ObservableList<AbstractItem> itemDatas, List<GameObject> targetObjs)
        {
            var tempList = new List<AbstractItem>();

            foreach (var targetObj in targetObjs)
            {
                foreach (var itemData in itemDatas)
                {
                    if (itemData.GameObject == targetObj)
                    {
                        tempList.Add(itemData);
                        break;
                    }
                }
            }

            return tempList;
        }

        
        public static List<GameObject> GetItemObjs(this ObservableList<AbstractItem> itemDatas)
        {
            List<GameObject> itemObjs = new List<GameObject>();

            foreach (var itemData in itemDatas)
            {
                itemObjs.Add(itemData.GameObject);
            }

            return itemObjs;
        }
    }
}