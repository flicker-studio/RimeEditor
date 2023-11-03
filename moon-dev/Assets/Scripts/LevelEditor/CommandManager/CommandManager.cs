using System.Collections.Generic;

namespace LevelEditor
{
    public delegate void CommandExcute(Command command);
    public class CommandManager
    {
        private Stack<Command> m_undoCommands = new Stack<Command>();

        private Stack<Command> m_redoCommands = new Stack<Command>();
        
        public void Excute(Command command)
        {
            command.Execute();
            m_undoCommands.Push(command);
            m_redoCommands.Clear();
        }

        public void Undo()
        {
            if (m_undoCommands.Count <= 0) return;
            Command command = m_undoCommands.Pop();
            m_redoCommands.Push(command);
            command.Undo();
        }

        public void Redo()
        {
            if (m_redoCommands.Count <= 0) return;
            Command command = m_redoCommands.Pop();
            m_undoCommands.Push(command);
            command.Execute();
        }
    }

}