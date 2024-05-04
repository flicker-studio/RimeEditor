using LevelEditor.Command;
using Moon.Kernel.Extension;
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
        private readonly Button _undoButton;
        private readonly Button _redoButton;
        private          Button _viewButton;
        private          Button _positionButton;
        private          Button _rotationButton;
        private          Button _scaleButton;
        private          Button _rectButton;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public ActionPanel(Transform rect, UISetting setting)
        {
            var property = setting.GetActionPanelUI.GetActionPanelUIName;
            _undoButton     = rect.FindPath(property.UNDO_BUTTON).GetComponent<Button>();
            _redoButton     = rect.FindPath(property.REDO_BUTTON).GetComponent<Button>();
            _viewButton     = rect.FindPath(property.VIEW_BUTTON).GetComponent<Button>();
            _positionButton = rect.FindPath(property.POSITON_BUTTON).GetComponent<Button>();
            _rotationButton = rect.FindPath(property.ROTATION_BUTTON).GetComponent<Button>();
            _scaleButton    = rect.FindPath(property.SCALE_BUTTON).GetComponent<Button>();
            _rectButton     = rect.FindPath(property.RECT_BUTTON).GetComponent<Button>();

            _undoButton.onClick.AddListener(CommandInvoker.Undo);
            _redoButton.onClick.AddListener(CommandInvoker.Redo);
        }
    }
}