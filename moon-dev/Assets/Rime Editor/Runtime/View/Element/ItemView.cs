using System;
using System.Collections.Generic;
using Frame.CompnentExtensions;
using LevelEditor.Controller;
using LevelEditor.Data;
using LevelEditor.Item;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor.View.Element
{
    /// <summary>
    ///     Display items in the inspection panel
    /// </summary>
    public sealed class ItemView : EventButton
    {
        private readonly Button         _arrowButton;
        private readonly List<ItemView> _childList = new();
        private readonly ItemBase       _itemBase;

        private readonly TextMeshProUGUI _textMesh;
        private readonly ItemType        _type;
        private          bool            _isShow = true;
        private          string          _name;

        /// <summary>
        /// </summary>
        /// <param name="itemBase"></param>
        /// <param name="onSelect"></param>
        /// <param name="scrollView"></param>
        public ItemView(ItemBase itemBase, Action onSelect, ScrollRect scrollView) :
            base(itemBase.GameObject, onSelect, scrollView)
        {
            _itemBase    = itemBase;
            _textMesh    = GameObject.Find("DescribeText").GetComponent<TextMeshProUGUI>();
            _arrowButton = GameObject.Find("Arrow").GetComponent<Button>();
            _type        = itemBase.Type;
        }

        /// <summary>
        ///     The name displayed by the Item
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// </summary>
        public ItemType Type => _type;

        /// <summary>
        /// </summary>
        public Transform Transform => _itemBase.GameObject.transform;

        public ItemBase ItemBase => _itemBase;

        /// <summary>
        ///     Get all child objects recursively
        /// </summary>
        /// <returns>
        ///     All child objects of this item
        /// </returns>
        public List<ItemView> GetAllChild()
        {
            var tempList = new List<ItemView>();
            foreach (var view in _childList) tempList.AddRange(view.GetAllChild());
            return tempList;
        }

        /// <summary>
        ///     Show all child objects
        /// </summary>
        public void ShowChild()
        {
            _isShow = true;
            foreach (var child in _childList) child.Transform.gameObject.SetActive(true);

            _arrowButton.transform.rotation = Quaternion.Euler(0, 0, 90);
        }

        /// <summary>
        /// </summary>
        private void HideChild()
        {
            _isShow = false;
            foreach (var child in _childList) child.Transform.gameObject.SetActive(false);

            _arrowButton.transform.rotation = Quaternion.Euler(0, 0, 0);
        }

        /// <summary>
        /// </summary>
        /// <param name="childView"></param>
        public void AddChild(ItemView childView)
        {
            _childList.Add(childView);
            if (_childList.Count > 0)
                _name = $"{Enum.GetName(typeof(ItemType), Type)}({_childList.Count})";
            else
                _name = $"{Enum.GetName(typeof(ItemType), Type)}";
        }

        /// <summary>
        /// </summary>
        /// <param name="itemViewChild"></param>
        public void RemoveChild(ItemView itemViewChild)
        {
            _childList.Remove(itemViewChild);
            if (_childList.Count > 0)
                _name = $"{Enum.GetName(typeof(ItemType), Type)}({_childList.Count})";
            else
                _name = $"{Enum.GetName(typeof(ItemType), Type)}";
        }

        /// <summary>
        ///     Remove this item view and delete bound item
        /// </summary>
        public void Remove()
        {
            if (_childList.Count > 0)
                foreach (var view in _childList)
                    view.Remove();

            _childList.Clear();
            RemoveEvents();
            ItemController.RemoveItem(ItemBase);
        }

        public void Select()
        {
        }

        public void Active()
        {
        }

        public void Inactive()
        {
        }
    }
}