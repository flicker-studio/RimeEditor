using System;
using UnityEngine;

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
        /// <inheritdoc />
        public void Handle(Context context)
        {
            if (context.State is ReadyState)
            {
                throw new Exception($"Unable to transfer to {context.State}.");
            }

            IState.Transition(this, context);
        }

        /// <inheritdoc />
        public void OnEnter()
        {
            Debug.Log($"{this} enter.");
        }

        /// <inheritdoc />
        public void OnUpdate()
        {
        }

        /// <inheritdoc />
        public void OnExit()
        {
            Debug.Log($"{this} exit.");
        }
    }
}