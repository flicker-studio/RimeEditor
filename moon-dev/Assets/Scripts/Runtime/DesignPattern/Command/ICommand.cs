namespace Moon.Runtime.DesignPattern
{
    /// <summary>
    ///     Command Pattern base interface.
    /// </summary>
    public interface ICommand
    {
        /// <summary>
        /// Execute a command
        /// </summary>
        public void Execute();
    }
}