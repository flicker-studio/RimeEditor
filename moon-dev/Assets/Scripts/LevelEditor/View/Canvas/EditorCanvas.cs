﻿using System;
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
        internal Action<List<ItemBase>> Update;
        private  InspectorPanel     _inspectorPanel;
        private  ActionPanel        _actionPanel;
        
        public EditorCanvas(UISetting setting)
        {
            var trans = EntranceController.RootObject.transform as RectTransform;
            _inspectorPanel = new InspectorPanel(trans, setting);
            _actionPanel    = new ActionPanel(trans);
        }
        
        ~EditorCanvas()
        {
            Dispose(false);
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
        
        private void Dispose(bool disposing)
        {
            if (disposing)
            {
            }
            
            _inspectorPanel = null;
        }
    }
}