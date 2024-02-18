using System.Collections.Generic;

namespace LevelEditor
{
    public class Delete : ICommand
    {
        private readonly List<AbstractItem> _targetItem;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Delete(List<AbstractItem> targetItem)
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