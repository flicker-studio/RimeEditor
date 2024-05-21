using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.Command
{
    /// <summary>
    ///     Rotate command
    /// </summary>
    public sealed class Rotation : ICommand
    {
        private readonly List<Item>       _items;
        private readonly List<Quaternion> _newRot;
        private readonly List<Quaternion> _oldRot;

        /// <summary>
        ///     Command constructor
        /// </summary>
        /// <param name="items">The collection of Items being manipulated</param>
        /// <param name="newRot">New rotating quaternion</param>
        public Rotation
        (
            IReadOnlyCollection<Item> items,
            IEnumerable<Quaternion>   newRot
        )
        {
            var count = items.Count;
            
            _items  = new List<Item>(count);
            _newRot = new List<Quaternion>(count);
            _oldRot = new List<Quaternion>(count);

            _items.AddRange(items);
            _newRot.AddRange(newRot);

            for (var i = 0; i < _items.Count; i++) _oldRot.Add(_items[i].Transform.rotation);
        }

        /// <inheritdoc />
        public void Execute()
        {
            for (var i = 0; i < _items.Count; i++) _items[i].Transform.rotation = _newRot[i];
        }

        /// <inheritdoc />
        public void Undo()
        {
            for (var i = 0; i < _items.Count; i++) _items[i].Transform.rotation = _oldRot[i];
        }
    }
}