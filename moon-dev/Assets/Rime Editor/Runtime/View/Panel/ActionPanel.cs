using System;
using LevelEditor.Command;
using LevelEditor.Extension;
using UnityEngine;
using UnityEngine.UI;

namespace LevelEditor
{
    /// <summary>
    ///     Operator panel section
    /// </summary>
    /// <remarks>
    ///     TODO: Removal is imminent
    /// </remarks>
    internal sealed class ActionPanel
    {
        private readonly Button _positionButton;
        private readonly Button _rectButton;
        private readonly Button _redoButton;
        private readonly Button _rotationButton;
        private readonly Button _scaleButton;
        private readonly Button _undoButton;
        private readonly Button _viewButton;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public ActionPanel(Transform parent)
        {
            var property = RimeEditor.Runtime.RimeEditor.Configure.UI.GetActionPanelUI.GetActionPanelUIName;
            _undoButton     = parent.FindPath(property.UNDO_BUTTON).GetComponent<Button>();
            _redoButton     = parent.FindPath(property.REDO_BUTTON).GetComponent<Button>();
            _viewButton     = parent.FindPath(property.VIEW_BUTTON).GetComponent<Button>();
            _positionButton = parent.FindPath(property.POSITON_BUTTON).GetComponent<Button>();
            _rotationButton = parent.FindPath(property.ROTATION_BUTTON).GetComponent<Button>();
            _scaleButton    = parent.FindPath(property.SCALE_BUTTON).GetComponent<Button>();
            _rectButton     = parent.FindPath(property.RECT_BUTTON).GetComponent<Button>();

            _undoButton.onClick.AddListener(CommandInvoker.Undo);
            _redoButton.onClick.AddListener(CommandInvoker.Redo);
            _viewButton.onClick.AddListener(View);
        }

        ~ActionPanel()
        {
            _undoButton.onClick.RemoveListener(CommandInvoker.Undo);
            _redoButton.onClick.RemoveListener(CommandInvoker.Redo);
            _viewButton.onClick.RemoveListener(View);
        }

        private void View()
        {
            throw new NotImplementedException();
        }
    }
}