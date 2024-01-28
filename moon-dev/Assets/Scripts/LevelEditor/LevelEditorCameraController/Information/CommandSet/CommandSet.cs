using System;

namespace LevelEditor
{
    public delegate void CommandExcute(ICommand command);

    public delegate void UndoExcute();

    public delegate void RedoExcute();

    public delegate void ClearExcute();

    public delegate void EnableExcute();
    
    public class CommandSet
    {
        public CommandExcute GetExcute { get; private set; }

        public UndoExcute GetUndo { get; private set; }

        public RedoExcute GetRedo { get; private set; }
        
        public ClearExcute Clear { get; private set; }

        public event Action UndoAdditiveEvent;

        public event Action RedoAdditiveEvent;

        public EnableExcute EnableExcute;

        public CommandSet(CommandExcute excute, UndoExcute undo, RedoExcute redo,ClearExcute clear)
        {
            GetUndo = ExcuteUndoAdditiveEvent;
            GetRedo = ExcuteRedoAdditiveEvent;
            GetExcute = excute;
            GetUndo += undo;
            GetRedo += redo;
            Clear = clear;
        }

        private void ExcuteUndoAdditiveEvent()
        {
            UndoAdditiveEvent?.Invoke();
        }

        private void ExcuteRedoAdditiveEvent()
        {
            RedoAdditiveEvent?.Invoke();
        }
    }
}
