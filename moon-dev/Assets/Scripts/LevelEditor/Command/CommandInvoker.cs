using System.Collections.Generic;

namespace LevelEditor
{
    /// <summary>
    ///     The command invoker is responsible for the specific invocation of commands
    /// </summary>
    public class CommandInvoker
    {
        private static readonly Stack<ICommand> UndoCommands = new();

        private static readonly Stack<ICommand> RedoCommands = new();

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
        public static void Execute(ICommand command)
        {
            command.Execute();
            UndoCommands.Push(command);
            RedoCommands.Clear();
        }

        /// <summary>
        ///     Cancel the previous command
        /// </summary>
        public static void Undo()
        {
            if (UndoCommands.Count <= 0)
            {
                return;
            }

            var command = UndoCommands.Pop();
            RedoCommands.Push(command);
            command.Undo();
        }

        /// <summary>
        ///     Re-execute the revocation command
        /// </summary>
        public static void Redo()
        {
            if (RedoCommands.Count <= 0)
            {
                return;
            }

            var command = RedoCommands.Pop();
            UndoCommands.Push(command);
            command.Execute();
        }

        /// <summary>
        ///     Clear all command caches
        /// </summary>
        public static void Clear()
        {
            UndoCommands.Clear();
            RedoCommands.Clear();
        }
    }
}