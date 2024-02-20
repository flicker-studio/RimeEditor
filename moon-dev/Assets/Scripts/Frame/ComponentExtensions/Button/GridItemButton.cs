using System;
using Frame.Tool.Pool;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Frame.CompnentExtensions
{
    public abstract class GridItemButton
    {
        public GameObject GameObject => ButtonObj;

        protected EventButton _eventButton;

        protected readonly GameObject ButtonObj;

        protected GridItemButton
        (
            GameObject buttonPrefab,
            Action     click,
            Transform  parent,
            ScrollRect scrollRect
        )
        {
            ButtonObj    = Object.Instantiate(buttonPrefab, parent, true);
            _eventButton = new EventButton(ButtonObj, click, scrollRect);
        }

        public void SetSelected(bool value)
        {
            _eventButton.IsSelected = value;
        }

        public void Remove()
        {
            ObjectPool.Instance.GameObjectPool.Release(ButtonObj);
            _eventButton.RemoveEvents();
        }

        public void Invoke()
        {
            _eventButton?.Invoke();
        }

        public void SetActive(bool active)
        {
            ButtonObj.SetActive(active);
        }

        public bool GetActive()
        {
            return ButtonObj.activeInHierarchy;
        }
    }
}