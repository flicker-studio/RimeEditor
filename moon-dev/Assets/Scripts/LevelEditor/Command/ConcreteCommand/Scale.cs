using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    ///     Commands to manipulate the scaling of items
    /// </summary>
    public class Scale : ICommand
    {
        private readonly List<AbstractItem> _items;
        private readonly List<Vector3>      _newScale;
        private readonly List<Vector3>      _oldScale;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Scale(List<AbstractItem> items, IEnumerable<Vector3> newScale)
        {
            var count = items.Count;

            _items    = new List<AbstractItem>(count);
            _newScale = new List<Vector3>(count);
            _oldScale = new List<Vector3>(count);

            _items.AddRange(items);
            _newScale.AddRange(newScale);
            for (var i = 0; i < count; i++) _oldScale.Add(_items[i].Transform.position);
        }

        /// <inheritdoc />
        public void Execute()
        {
            for (var i = 0; i < _items.Count; i++) _items[i].Transform.localScale = _newScale[i];
        }

        /// <inheritdoc />
        public void Undo()
        {
            for (var i = 0; i < _items.Count; i++) _items[i].Transform.localScale = _oldScale[i];
        }
    }
}