namespace Frame.StateMachine
{
    /// <summary>
    ///     Mealy machine status interface
    /// </summary>
    public interface IState
    {
        /// <summary>
        ///     Switch state and execute
        /// </summary>
        /// <param name="context">Current state machine context</param>
        public void Handle(Context context);

        /// <summary>
        ///     Method executed after state machine enter
        /// </summary>
        public void OnEnter();

        /// <summary>
        ///     Method to execute before exiting
        /// </summary>
        public void OnExit();
    }
}