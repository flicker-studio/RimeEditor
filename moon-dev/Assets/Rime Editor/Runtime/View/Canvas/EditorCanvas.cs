using System;
using System.Collections.Generic;
using LevelEditor.Item;
using UnityEngine;

namespace LevelEditor.View.Canvas
{
    /// <summary>
    ///     Canvas in the editing state.
    /// </summary>
    internal sealed class EditorCanvas : ICanvas
    {
        private  ActionPanel            _actionPanel;
        private  InspectorPanel         _inspectorPanel;
        internal Action<List<ItemBase>> Update;

        public EditorCanvas(UISetting setting)
        {
            var trans = RimeEditor.Runtime.RimeEditor.Instance.gameObject.transform as RectTransform;
            _inspectorPanel = new InspectorPanel(trans, setting);
            _actionPanel    = new ActionPanel(trans);
        }

        public void Active()
        {
            _inspectorPanel.Add();
            Update += _inspectorPanel.TransformBind;
        }

        public void Inactive()
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            Dispose(false);
            GC.SuppressFinalize(this);
        }

        ~EditorCanvas()
        {
            Dispose(false);
        }

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            _inspectorPanel = null;
        }
    }
}