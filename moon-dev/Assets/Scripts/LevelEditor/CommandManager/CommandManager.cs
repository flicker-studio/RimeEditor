using System.Collections.Generic;

namespace LevelEditor
{
    public class CommandManager
    {
        private Stack<LevelEditCommand> m_undoCommands = new Stack<LevelEditCommand>();

        private Stack<LevelEditCommand> m_redoCommands = new Stack<LevelEditCommand>();

        public CommandSet CommandSet { get; private set; }

        public CommandManager()
        {
            CommandSet = new CommandSet(Excute, Undo, Redo, Clear);
        }

        public void Excute(LevelEditCommand command)
        {
            command.Execute();
            m_undoCommands.Push(command);
            m_redoCommands.Clear();
        }

        public void Undo()
        {
            if (m_undoCommands.Count <= 0) return;

            LevelEditCommand command = m_undoCommands.Pop();
            m_redoCommands.Push(command);
            command.Undo();
        }

        public void Redo()
        {
            if (m_redoCommands.Count <= 0) return;

            LevelEditCommand command = m_redoCommands.Pop();
            m_undoCommands.Push(command);
            command.Execute();
        }

        public void Clear()
        {
            m_undoCommands.Clear();
            m_redoCommands.Clear();
        }
    }
}