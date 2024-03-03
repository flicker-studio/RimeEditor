using System.Collections.Generic;
using Frame.StateMachine;
using LevelEditor.Canvas;

namespace LevelEditor.State
{
    /// <summary>
    ///     The state-machine is switched to edit-mode
    /// </summary>
    public sealed class EditorState : IState
    {
        private readonly EditorCanvas       _canvas;
        private readonly List<AbstractItem> _items = new();

        /// <summary>
        ///     Default constructor
        /// </summary>
        public EditorState(UISetting setting)
        {
            _canvas = new EditorCanvas(setting);
        }

        /// <inheritdoc />
        public void OnEnter()
        {
            _canvas.Active();

            _items.Add(new Platform());
        }

        /// <inheritdoc />
        public void OnUpdate()
        {
            _canvas.Update(_items);
        }

        /// <inheritdoc />
        public void OnExit()
        {
            _canvas.Inactive();
        }
    }
}