using System.Collections;
using System.Collections.Generic;
using Frame.Tool.Pool;
using UnityEngine;

namespace LevelEditor
{
    public class ItemData
    {
        public GameObject GetItemObj => m_itemObj;

        public ItemProduct GetItemProduct => m_itemProduct;

        private Quaternion m_rotation;

        private Vector3 m_position;

        private Vector3 m_scale;
    
        private Vector3 GetScreenMiddlePoint =>
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));
        
        private GameObject m_itemObj;
        
        private ItemProduct m_itemProduct;

        public ItemData(ItemProduct itemProduct)
        {
            m_itemProduct = itemProduct;
            m_itemObj = ObjectPool.Instance.OnTake(m_itemProduct.ItemObject);
            TransformInit();
        }
        
        public void SetActive(bool active)
        {
            if (active)
            {
                SetTransformFromData();
                m_itemObj = ObjectPool.Instance.OnTake(m_itemObj,m_itemProduct.ItemObject);
            }
            else
            {
                GetTransformToData();
                ObjectPool.Instance.OnRelease(m_itemObj);
            }
        }

        private void TransformInit()
        {
            m_itemObj.transform.position = GetScreenMiddlePoint;
            m_itemObj.transform.rotation = Quaternion.identity;
            m_itemObj.transform.localScale = GetItemProduct.ItemObject.transform.localScale;
        }

        private void GetTransformToData()
        {
            m_position = m_itemObj.transform.position;
            m_rotation = m_itemObj.transform.rotation;
            m_scale = m_itemObj.transform.localScale;
        }

        private void SetTransformFromData()
        {
            m_itemObj.transform.position = m_position;
            m_itemObj.transform.rotation = m_rotation;
            m_itemObj.transform.localScale = m_scale;
        }
    }
}

