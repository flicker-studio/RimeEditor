using System.Collections.Generic;
using LevelEditor.Item;

namespace LevelEditor.Command
{
    public class Delete : ICommand
    {
        private readonly List<ItemBase> _targetItem;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Delete(List<ItemBase> targetItem)
        {
            _targetItem = targetItem;
        }

        /// <inheritdoc />
        public void Execute()
        {
            foreach (var targetAsset in _targetItem)
            {
                targetAsset.Inactive();
            }
        }

        /// <inheritdoc />
        public void Undo()
        {
            foreach (var targetAsset in _targetItem)
            {
                targetAsset.Active();
            }
        }
    }
}