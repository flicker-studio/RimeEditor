using System;

namespace LevelEditor
{
    public class CommandSet
    {
        public Action GetUndo => UndoAdditiveEvent;

        public Action GetRedo => RedoAdditiveEvent;

        public event Action UndoAdditiveEvent;

        public event Action RedoAdditiveEvent;

        public Action EnableExcute;

        public CommandSet(Action undo, Action redo)
        {
            UndoAdditiveEvent += undo;
            RedoAdditiveEvent += redo;
        }
    }
}