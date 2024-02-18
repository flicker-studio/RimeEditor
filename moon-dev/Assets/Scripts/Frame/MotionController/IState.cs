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
        /// <param name="context">
        ///     Current state machine context
        /// </param>
        /// <remarks>
        ///     The default implementation of this method is the toggle state.
        ///     If you want to override this code, refer to the following example.
        /// </remarks>
        /// <example>
        ///     <code>
        ///     public void Handle(Context context)
        ///     {
        ///         if (context.State is ReadyState)
        ///         {
        ///             throw new Exception($"Unable to transfer to {context.State}.");
        ///         }
        ///         IState.Transition(this, context);
        ///     }
        ///     </code>
        /// </example>
        public void Handle(Context context)
        {
            Transition(this, context);
        }

        /// <summary>
        ///     Method executed after state machine enter
        /// </summary>
        public void OnEnter();

        /// <summary>
        ///     Method to execute before exiting
        /// </summary>
        public void OnExit();

        /// <summary>
        ///     The default implementation of the Handle method
        /// </summary>
        /// <param name="state">
        ///     It should be <see langword="this" />
        /// </param>
        /// <param name="context">
        ///     Current state machine context
        /// </param>
        protected static void Transition(IState state, Context context)
        {
            context.Transition(state);
        }
    }
}