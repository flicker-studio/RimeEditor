namespace LevelEditor
{
    /// <summary>
    ///     Command mode interface
    /// </summary>
    public interface ICommand : Frame.Tool.ICommand
    {
        /// <summary>
        ///     Revoke an command that was executed
        /// </summary>
        public void Undo();
    }
}