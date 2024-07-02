namespace LevelEditor.Command
{
    /// <inheritdoc />
    internal interface ICommand : Moon.Runtime.DesignPattern.ICommand
    {
        /// <summary>
        ///     Revoke a command that was executed
        /// </summary>
        public void Undo();
    }
}