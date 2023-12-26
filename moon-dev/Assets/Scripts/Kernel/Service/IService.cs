namespace Moon.Kernel.Service
{
    /// <summary>
    ///     System service interface. This interface allows users to create long-running, executable applications in .
    ///     Services start automatically when the game starts, can be paused and restarted.
    /// </summary>
    /// <remarks>Services are ideal for situations where long-running functionality is required.</remarks>
    public interface IService
    {
        public bool IsRunning { get; }
    }
}