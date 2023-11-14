using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public delegate void CommandExcute(Command command);

    public delegate void UndoExcute();

    public delegate void RedoExcute();
    
    public class CommandSet
    {
        public CommandExcute GetExcute { get; private set; }

        public UndoExcute GetUndo { get; private set; }

        public RedoExcute GetRedo { get; private set; }

        public CommandSet(CommandExcute excute, UndoExcute undo, RedoExcute redo)
        {
            GetExcute = excute;
            GetUndo = undo;
            GetRedo = redo;
        }
    }
}
