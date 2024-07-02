using System;

namespace LevelEditor.View.Canvas
{
    /// <summary>
    ///     The interface of the UI canvas
    /// </summary>
    internal interface ICanvas : IDisposable
    {
        /// <summary>
        ///     Activates the current canvas
        /// </summary>
        public void Active();

        /// <summary>
        ///     Inactive current canvas
        /// </summary>
        public void Inactive();
    }
}