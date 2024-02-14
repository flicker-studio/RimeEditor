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
        public IState State => m_state;

        private IState m_state;


        /// <summary>
        ///     Constructs the context and sets the current state to <inheritdoc cref="ReadyState" />
        /// </summary>
        public Context()
        {
            m_state = new ReadyState();
        }


        /// <summary>
        ///     Set status
        /// </summary>
        /// <param name="state">
        ///     Target state
        /// </param>
        public void Transition(IState state)
        {
            m_state.OnExit();
            m_state = state;
            m_state.OnEnter();
        }
    }
}