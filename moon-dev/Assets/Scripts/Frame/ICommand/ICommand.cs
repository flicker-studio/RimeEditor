using System;

namespace Frame.Tool
{
    public interface ICommand
    {
        /// <summary>
        /// Execute a command
        /// </summary>
        public void Execute();
    }
}