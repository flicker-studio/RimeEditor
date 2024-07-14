using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.Item
{
    public static class ItemMethod
    {
        public static ItemBase CheckItemObj(this List<ItemBase> itemDatas, GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
                if (itemData.GameObject == targetObj)
                    return itemData;

            return null;
        }

        public static List<ItemBase> CheckItemObjs(this List<ItemBase> itemDatas, List<GameObject> targetObjs)
        {
            var tempList = new List<ItemBase>();

            foreach (var targetObj in targetObjs)
            foreach (var itemData in itemDatas)
                if (itemData.GameObject == targetObj)
                {
                    tempList.Add(itemData);
                    break;
                }

            return tempList;
        }

        public static List<GameObject> GetItemObjs(this List<ItemBase> itemDatas)
        {
            var itemObjs = new List<GameObject>();

            foreach (var itemData in itemDatas) itemObjs.Add(itemData.GameObject);

            return itemObjs;
        }

        /// <summary>
        /// </summary>
        /// <param name="items"></param>
        /// <returns></returns>
        public static (List<Vector3>, List<Quaternion>, List<Vector3>) GetTransforms(this List<ItemBase> items)
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

        public static ItemBase CheckItemObj(this ObservableList<ItemBase> itemDatas, GameObject targetObj)
        {
            foreach (var itemData in itemDatas)
                if (itemData.GameObject == targetObj)
                    return itemData;

            return null;
        }

        public static List<ItemBase> CheckItemObjs(this ObservableList<ItemBase> itemDatas, List<GameObject> targetObjs)
        {
            var tempList = new List<ItemBase>();

            foreach (var targetObj in targetObjs)
            foreach (var itemData in itemDatas)
                if (itemData.GameObject == targetObj)
                {
                    tempList.Add(itemData);
                    break;
                }

            return tempList;
        }

        public static List<GameObject> GetItemObjs(this ObservableList<ItemBase> itemDatas)
        {
            var itemObjs = new List<GameObject>();

            foreach (var itemData in itemDatas) itemObjs.Add(itemData.GameObject);

            return itemObjs;
        }
    }
}