using System.Collections.Generic;

namespace LevelEditor
{
    /// <summary>
    ///     The command invoker is responsible for the specific invocation of commands
    /// </summary>
    public class CommandInvoker
    {
        private readonly Stack<LevelEditCommand> m_undoCommands = new();

        private readonly Stack<LevelEditCommand> m_redoCommands = new();

        /// <summary>
        /// </summary>
        public CommandSet CommandSet { get; private set; }

        /// <summary>
        ///     Default constructor
        /// </summary>
        public CommandInvoker()
        {
            CommandSet = new CommandSet(Execute, Undo, Redo, Clear);
        }

        /// <summary>
        ///     Execute the command and press it into the cache stack
        /// </summary>
        /// <param name="command">Target command</param>
        public void Execute(LevelEditCommand command)
        {
            command.Execute();
            m_undoCommands.Push(command);
            m_redoCommands.Clear();
        }

        /// <summary>
        ///     Cancel the previous command
        /// </summary>
        public void Undo()
        {
            if (m_undoCommands.Count <= 0)
            {
                return;
            }

            var command = m_undoCommands.Pop();
            m_redoCommands.Push(command);
            command.Undo();
        }

        /// <summary>
        ///     Re-execute the revocation command
        /// </summary>
        public void Redo()
        {
            if (m_redoCommands.Count <= 0)
            {
                return;
            }

            var command = m_redoCommands.Pop();
            m_undoCommands.Push(command);
            command.Execute();
        }

        /// <summary>
        ///     Clear all command caches
        /// </summary>
        public void Clear()
        {
            m_undoCommands.Clear();
            m_redoCommands.Clear();
        }
    }
}