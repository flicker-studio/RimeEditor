using System.Collections.Generic;

namespace LevelEditor.Command
{
    public class Delete : ICommand
    {
        private readonly List<Item> _targetItem;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Delete(List<Item> targetItem)
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