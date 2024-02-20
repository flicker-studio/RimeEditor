using System;
using System.Collections.Generic;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor.Data
{
    /// <summary>
    ///     Display items in the inspection panel
    /// </summary>
    public sealed class ItemView : EventButton
    {
        /// <summary>
        ///     The name displayed by the Item
        /// </summary>
        public string Name => _name;

        /// <summary>
        /// </summary>
        public ItemType Type => _type;

        /// <summary>
        /// </summary>
        public Transform Transform => _item.GameObject.transform;

        public AbstractItem Item => _item;

        private readonly TextMeshProUGUI _textMesh;
        private readonly Button          _arrowButton;
        private readonly AbstractItem    _item;
        private          string          _name;
        private readonly ItemType        _type;
        private readonly List<ItemView>  _childList = new();
        private          bool            _isShow    = true;

        /// <summary>
        /// </summary>
        /// <param name="item"></param>
        /// <param name="onSelect"></param>
        /// <param name="scrollView"></param>
        public ItemView(AbstractItem item, Action onSelect, ScrollRect scrollView) :
            base(item.GameObject, onSelect , scrollView)
        {
            _item        = item;
            _textMesh    = GameObject.Find("DescribeText").GetComponent<TextMeshProUGUI>();
            _arrowButton = GameObject.Find("Arrow").GetComponent<Button>();
            _type        = item.Type;
        }

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
        /// <param name="targetChild"></param>
        public void AddChild(ItemView targetChild)
        {
            _childList.Add(targetChild);
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
            _item.Delete();
            RemoveEvents();
        }

        public void Select()
        {
        }
    }
}