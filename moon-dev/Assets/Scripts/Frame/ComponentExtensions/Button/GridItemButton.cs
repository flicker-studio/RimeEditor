using System;
using Frame.Tool.Pool;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.Data
{
    public abstract class GridItemButton
    {
        public bool SetSelected
        {
            set
            {
                m_eventButton.IsSelected = value;
            }
        }

        public GameObject GetButtonObj
        {
            get
            {
                return m_buttonObj;
            }
        }
        
        protected EventButton<GridItemButton> m_eventButton;

        protected GameObject m_buttonObj;

        public GridItemButton(GameObject buttonPrefab,Action<GridItemButton> onSelect,Transform parent,ScrollRect scrollRect)
        {
            m_buttonObj = ObjectPool.Instance.OnTake(buttonPrefab);
            m_buttonObj.transform.SetParent(parent);
            m_eventButton = new EventButton<GridItemButton>(this, m_buttonObj.transform, onSelect,scrollRect);
        }

        public void Remove()
        {
            ObjectPool.Instance.OnRelease(m_buttonObj);
            m_eventButton.RemoveEvents();
        }

        public void Invoke()
        {
            m_eventButton?.Invoke();
        }

        public void SetActive(bool active)
        {
            m_buttonObj.SetActive(active);
        }

        public bool GetActive()
        {
            return m_buttonObj.activeInHierarchy;
        }
    }

}