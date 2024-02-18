namespace Frame.StateMachine
{
    /// <summary>
    ///     Environmental context of mealy machine
    /// </summary>
    public class Context
    {
        /// <summary>
        ///     Current status
        /// </summary>
        public IState State => _state;

        private IState _state;

        /// <summary>
        ///     Constructs the context and sets the current state to <inheritdoc cref="ReadyState" />
        /// </summary>
        public Context()
        {
            _state = new ReadyState();
        }

        /// <summary>
        ///     Set status
        /// </summary>
        /// <param name="state">
        ///     Target state
        /// </param>
        internal void Transition(IState state)
        {
            _state.OnExit();
            _state = state;
            _state.OnEnter();
        }
    }
}