using System;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Frame.CompnentExtensions
{
    public abstract class ListEntry : IDisposable
    {
        public GameObject GameObject => ButtonObj;

        protected EventButton _eventButton;

        protected readonly GameObject ButtonObj;

        protected ListEntry
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
            Object.Destroy(ButtonObj);
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

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            Object.Destroy(ButtonObj);
        }
    }
}