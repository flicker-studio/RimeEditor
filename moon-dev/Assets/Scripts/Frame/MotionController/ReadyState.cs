using System;

namespace Frame.StateMachine
{
    /// <inheritdoc />
    /// <summary>
    ///     The initial state of the state machine
    /// </summary>
    /// <remarks>
    ///     Cannot switch to itself
    /// </remarks>
    public class ReadyState : IState
    {
        public void Handle(Context context)
        {
            if (context.State is ReadyState)
            {
                throw new Exception($"Unable to transfer to {context.State}.");
            }

            context.Transition(this);
        }

        /// <inheritdoc />
        public void OnEnter()
        {
            throw new NotImplementedException();
        }

        /// <inheritdoc />
        public void OnExit()
        {
            throw new NotImplementedException();
        }
    }
}