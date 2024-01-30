using System;

namespace LevelEditor
{
    public class CommandSet
    {
        public Action GetUndo => UndoAdditiveEvent;

        public Action GetRedo => RedoAdditiveEvent;

        public Action Clear { get; private set; }

        public event Action UndoAdditiveEvent;

        public event Action RedoAdditiveEvent;

        public Action EnableExcute;

        public CommandSet(Action<ICommand> excute, Action undo, Action redo, Action clear)
        {
            Clear = clear;
            UndoAdditiveEvent += undo;
            RedoAdditiveEvent += redo;
        }
    }
}