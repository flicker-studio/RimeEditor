using System;
using Frame.Tool.Pool;
using Moon.Kernel.Extension;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    [JsonConverter(typeof(ItemDataConverter))]
    public abstract class ItemDataBase
    {
        [JsonProperty("ItemDataType", Order = 1)]
        public abstract ItemDataType ItemDataType { get; }

        [JsonIgnore] public GameObject GetItemObjPlay => m_itemObjPlay;
        [JsonIgnore] public GameObject GetItemObjEditor { get; private set; }

        [JsonIgnore] public ItemProduct GetItemProduct => m_itemProduct;

        [JsonIgnore]
        private Vector3 GetScreenMiddlePoint
        {
            get
            {
                var mainCamera = Camera.main;

                if (mainCamera is null)
                {
                    throw new NullReferenceException("Unable to get the main camera");
                }

                var pos = new Vector3
                (
                    Screen.width / 2.0f,
                    Screen.height / 2.0f,
                    Mathf.Abs(mainCamera.transform.position.z)
                );

                return mainCamera.ScreenToWorldPoint(pos);
            }
        }

        [JsonProperty("Rotation", Order = 2)] protected Quaternion m_rotation;

        [JsonProperty("Position", Order = 3)] private Vector3 m_position;

        [JsonProperty("Scale", Order = 4)] private Vector3 m_scale;


        [JsonIgnore] protected GameObject m_itemObjPlay;

        [JsonIgnore] protected ItemProduct m_itemProduct;

        [JsonProperty("ProductName", Order = 5)]
        private string m_productName;

        [JsonProperty("ProductType", Order = 6)]
        private ITEMTYPEENUM m_productType;

        protected ItemDataBase(ItemProduct itemProduct, bool fromJson = false)
        {
            m_itemProduct = itemProduct;
            m_productName = m_itemProduct.Name;
            m_productType = m_itemProduct.ItemType;

            if (fromJson)
            {
                return;
            }

            GetItemObjEditor = ObjectPool.Instance.OnTake(m_itemProduct.ItemObject);
            TransformInit();
        }

        public abstract ItemDataBase Copy(ItemDataBase saveData);

        public void SetActiveEditor(bool active, bool isReload = false)
        {
            if (active)
            {
                SetTransformFromData();
                GetItemObjEditor = ObjectPool.Instance.OnTake(GetItemObjEditor, m_itemProduct.ItemObject);
            }
            else
            {
                if (!isReload)
                {
                    GetTransformToData();
                }

                ObjectPool.Instance.OnRelease(GetItemObjEditor);
            }
        }

        public virtual void SetActivePlay(bool active)
        {
            if (active)
            {
                m_itemObjPlay = ObjectPool.Instance.OnTake(m_itemObjPlay, m_itemProduct.ItemObject);
                m_itemObjPlay.transform.SetTransformValue(m_position, m_rotation, m_scale);
            }
            else
            {
                ObjectPool.Instance.OnRelease(m_itemObjPlay);
            }
        }

        public void GetTransformToData()
        {
            if (GetItemObjEditor == null)
            {
                GetItemObjEditor = ObjectPool.Instance.OnTake(m_itemProduct.ItemObject);
            }

            m_position = GetItemObjEditor.transform.position;
            m_rotation = GetItemObjEditor.transform.rotation;
            m_scale = GetItemObjEditor.transform.localScale;
        }

        public void SetTransformFromData()
        {
            if (GetItemObjEditor == null)
            {
                GetItemObjEditor = ObjectPool.Instance.OnTake(m_itemProduct.ItemObject);
            }

            GetItemObjEditor.transform.position = m_position;
            GetItemObjEditor.transform.rotation = m_rotation;
            GetItemObjEditor.transform.localScale = m_scale;
        }

        public (Vector3, Quaternion, Vector3) GetTransformFromData()
        {
            return (m_position, m_rotation, m_scale);
        }

        private void TransformInit()
        {
            GetItemObjEditor.transform.position = GetScreenMiddlePoint;
            GetItemObjEditor.transform.rotation = Quaternion.identity;
            GetItemObjEditor.transform.localScale = GetItemProduct.ItemObject.transform.localScale;
        }
    }
}