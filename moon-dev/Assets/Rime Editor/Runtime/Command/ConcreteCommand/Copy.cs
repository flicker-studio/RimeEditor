using System.Collections.Generic;
using LevelEditor.Item;
using LevelEditor.Manager;

namespace LevelEditor.Command
{
    /// <summary>
    ///     Copy command
    /// </summary>
    public class Copy : ICommand
    {
        private readonly List<ItemBase> _copyItem;
        private readonly List<ItemBase> _sourceItem;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Copy
        (
            List<ItemBase> sourceItem,
            OutlineManager outlinePainter
        )
        {
            var count = sourceItem.Count;
            _sourceItem = new List<ItemBase>(count);
            _copyItem   = new List<ItemBase>(count);
            _sourceItem.AddRange(sourceItem);
        }

        /// <inheritdoc />
        public void Execute()
        {
            foreach (var item in _sourceItem)
            {
                var copy = ItemFactory.Copy(item);
                copy.Active();
                _copyItem.Add(copy);
            }
        }

        /// <inheritdoc />
        public void Undo()
        {
            foreach (var item in _copyItem) item.Inactive();
        }
    }
}