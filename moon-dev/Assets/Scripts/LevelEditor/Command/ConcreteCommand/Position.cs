using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace LevelEditor
{
    /// <summary>
    ///     Position change command
    /// </summary>
    public class Position : ICommand
    {
        private readonly List<AbstractItem> _items;
        private readonly List<Vector3>      _newPosition;
        private readonly List<Vector3>      _oldPosition;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Position(List<AbstractItem> items, IEnumerable<Vector3> newPosition)
        {
            var count = items.Count();

            _items       = new List<AbstractItem>(count);
            _newPosition = new List<Vector3>(count);
            _oldPosition = new List<Vector3>(count);

            _items.AddRange(items);
            _newPosition.AddRange(newPosition);
            for (var i = 0; i < _items.Count; i++) _oldPosition[i] = _items[i].Transform.position;
        }

        /// <inheritdoc />
        public void Execute()
        {
            for (var i = 0; i < _items.Count; i++) _items[i].Transform.position = _newPosition[i];
        }

        /// <inheritdoc />
        public void Undo()
        {
            for (var i = 0; i < _items.Count; i++) _items[i].Transform.position = _oldPosition[i];
        }
    }
}