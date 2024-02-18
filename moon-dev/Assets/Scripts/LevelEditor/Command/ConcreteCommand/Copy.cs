using System.Collections.Generic;
using Moon.Kernel.Utils;

namespace LevelEditor
{
    /// <summary>
    ///     Copy command
    /// </summary>
    public class Copy : ICommand
    {
        private readonly List<AbstractItem> _copyItem;
        private readonly List<AbstractItem> _sourceItem;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Copy
        (
            List<AbstractItem> sourceItem,
            OutlineManager     outlinePainter
        )
        {
            var count = sourceItem.Count;
            _sourceItem = new List<AbstractItem>(count);
            _copyItem   = new List<AbstractItem>(count);
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
            foreach (var item in _copyItem)
            {
                item.Inactive();
            }
        }
    }
}