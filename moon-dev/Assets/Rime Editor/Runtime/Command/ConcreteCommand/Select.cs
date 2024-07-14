using System.Collections.Generic;
using System.Linq;
using LevelEditor.Item;
using LevelEditor.Manager;

namespace LevelEditor.Command
{
    /// <summary>
    ///     Select the Item command
    /// </summary>
    public class Select : ICommand
    {
        private readonly List<ItemBase> _lastList = new();
        private readonly List<ItemBase> _nextList = new();
        private readonly OutlineManager _outlinePainter;
        private readonly List<ItemBase> _targetList;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public Select(List<ItemBase> targetList, List<ItemBase> nextList, OutlineManager outlinePainter)
        {
            _outlinePainter = outlinePainter;
            _targetList     = targetList.ToList();
            _lastList.AddRange(targetList);
            _nextList.AddRange(nextList);
        }

        /// <inheritdoc />
        public void Execute()
        {
            _targetList.Clear();
            _targetList.AddRange(_nextList);
            _outlinePainter.SetRenderObjects(_targetList.GetItemObjs());
        }

        /// <inheritdoc />
        public void Undo()
        {
            _targetList.Clear();
            _targetList.AddRange(_lastList);
            _outlinePainter.SetRenderObjects(_targetList.GetItemObjs());
        }
    }
}