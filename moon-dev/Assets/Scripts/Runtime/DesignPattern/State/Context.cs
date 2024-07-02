using System;
using JetBrains.Annotations;

namespace Moon.Runtime.DesignPattern
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
        
        /// <summary>
        ///     The update hosting for the state machine may be empty.
        /// </summary>
        [CanBeNull] public Action Update { get; private set; }

        /// <summary>
        ///     Current status
        /// </summary>
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
            Update -= _state.OnUpdate;
            _state.OnExit();
            _state = state;
            _state.OnEnter();
            Update += _state.OnUpdate;
        }
    }
}