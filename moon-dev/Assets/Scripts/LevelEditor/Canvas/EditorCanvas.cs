﻿using System;
using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.Canvas
{
    internal sealed class EditorCanvas : ICanvas
    {
        internal Action<List<AbstractItem>> Update;
        private  InspectorPanel             _inspectorPanel;

        public EditorCanvas(UISetting setting)
        {
            var trans = Controller.RootObject.transform as RectTransform;
            _inspectorPanel = new InspectorPanel(trans, setting);
        }

        ~EditorCanvas()
        {
            Dispose(false);
        }

        public void Active()
        {
            _inspectorPanel.Add();
            Update += _inspectorPanel.AsyncRefresh;
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

        private void Dispose(bool disposing)
        {
            if (disposing)
            {
            }

            _inspectorPanel = null;
        }
    }
}