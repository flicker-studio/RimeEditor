using System;

namespace LevelEditor
{
    public class CommandSet
    {
        public Action GetRedo => RedoAdditiveEvent;
        public event Action RedoAdditiveEvent;

        public Action EnableExcute;

        public CommandSet(Action undo, Action redo)
        {
            RedoAdditiveEvent += redo;
        }
    }
}