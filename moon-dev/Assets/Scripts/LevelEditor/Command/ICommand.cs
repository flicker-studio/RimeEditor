namespace LevelEditor.Command
{
    /// <summary>
    ///     Command Pattern base interface.
    /// </summary>
    internal interface ICommand
    {
        /// <summary>
        ///     Execute a command
        /// </summary>
        public void Execute();

        /// <summary>
        ///     Revoke a command that was executed
        /// </summary>
        public void Undo();
    }
}