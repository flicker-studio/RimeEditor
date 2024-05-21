using System.Collections.Generic;
using System.Runtime.CompilerServices;

[assembly: InternalsVisibleTo("Test.Runtime")]

namespace LevelEditor.Command
{
    /// <summary>
    ///     The command invoker is responsible for the specific invocation of commands
    /// </summary>
    internal static class CommandInvoker
    {
        private static readonly Stack<ICommand> UndoCommands = new();
        
        private static readonly Stack<ICommand> RedoCommands = new();
        
        /// <summary>
        ///     Called after the Undo is executed
        /// </summary>
        public static event System.Action UndoAdditiveEvent;
        
        /// <summary>
        ///     Called after the Redo is executed
        /// </summary>
        public static event System.Action RedoAdditiveEvent;
        
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
            
            UndoAdditiveEvent?.Invoke();
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
            
            RedoAdditiveEvent?.Invoke();
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