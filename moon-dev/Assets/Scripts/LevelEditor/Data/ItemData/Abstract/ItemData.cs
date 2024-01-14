using Frame.Static.Extensions;
using Frame.Tool.Pool;
using Newtonsoft.Json;
using UnityEngine;

namespace LevelEditor
{
    public enum ItemDataType
    {
        Entrance,
        Exit,
        Platform
    }
    
    [JsonConverter(typeof(ItemDataConverter))]
    public abstract class ItemData : ICopy
    {
        public abstract ItemData Copy(ItemData saveData);
        [JsonProperty("ItemDataType",Order = 1)]
        public abstract ItemDataType ItemDataType { get; }
        [JsonIgnore]
        public GameObject GetItemObjPlay => m_itemObjPlay;
        [JsonIgnore]
        public GameObject GetItemObjEditor => m_itemObjEditor;
        [JsonIgnore]
        public ItemProduct GetItemProduct => m_itemProduct;
        [JsonProperty("Rotation",Order = 2)]
        protected Quaternion m_rotation;
        [JsonProperty("Position",Order = 3)]
        private Vector3 m_position;
        [JsonProperty("Scale",Order = 4)]
        private Vector3 m_scale;
        [JsonIgnore]
        private Vector3 GetScreenMiddlePoint =>
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));
        [JsonIgnore]
        private GameObject m_itemObjEditor;
        [JsonIgnore]
        protected GameObject m_itemObjPlay;
        [JsonProperty("Product",Order = 5)]
        protected ItemProduct m_itemProduct;

        public ItemData(ItemProduct itemProduct)
        {
            m_itemProduct = itemProduct;
            m_itemObjEditor = ObjectPool.Instance.OnTake(m_itemProduct.ItemObject);
            TransformInit();
        }

        public void SetActiveEditor(bool active, bool isReload = false)
        {
            if (active)
            {
                SetTransformFromData();
                m_itemObjEditor = ObjectPool.Instance.OnTake(m_itemObjEditor,m_itemProduct.ItemObject);
            }
            else
            {
                if(!isReload) GetTransformToData();
                ObjectPool.Instance.OnRelease(m_itemObjEditor);
            }
        }

        public virtual void SetActivePlay(bool active)
        {
            if (active)
            {
                m_itemObjPlay = ObjectPool.Instance.OnTake(m_itemObjPlay,m_itemProduct.ItemObject);
                m_itemObjPlay.transform.SetTransformValue(m_position,m_rotation,m_scale);
            }
            else
            {
                ObjectPool.Instance.OnRelease(m_itemObjPlay);
            }
        }

        private void TransformInit()
        {
            m_itemObjEditor.transform.position = GetScreenMiddlePoint;
            m_itemObjEditor.transform.rotation = Quaternion.identity;
            m_itemObjEditor.transform.localScale = GetItemProduct.ItemObject.transform.localScale;
        }

        public void GetTransformToData()
        {
            m_position = m_itemObjEditor.transform.position;
            m_rotation = m_itemObjEditor.transform.rotation;
            m_scale = m_itemObjEditor.transform.localScale;
        }

        public void SetTransformFromData()
        {
            m_itemObjEditor.transform.position = m_position;
            m_itemObjEditor.transform.rotation = m_rotation;
            m_itemObjEditor.transform.localScale = m_scale;
        }

        public (Vector3, Quaternion, Vector3) GetTransformFromData()
        {
            return (m_position, m_rotation, m_scale);
        }
    }
}

