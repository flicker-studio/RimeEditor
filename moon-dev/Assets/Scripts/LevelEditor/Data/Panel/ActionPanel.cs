using LevelEditor.Command;
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
        private Button                _undoButton;
        private Button                _redoButton;
        private Button                _viewButton;
        private Button                _positionButton;
        private Button                _rotationButton;
        private Button                _scaleButton;
        private Button                _rectButton;
        private HorizontalLayoutGroup _group;

        /// <summary>
        ///     Default constructor
        /// </summary>
        public ActionPanel(GameObject gameObject)
        {
            _group = gameObject.GetComponent<HorizontalLayoutGroup>();

            // var property = levelEditorUISetting.GetActionPanelUI.GetActionPanelUIName;
            // _undoButton     = levelEditorCanvasRect.FindPath(property.UNDO_BUTTON).GetComponent<Button>();
            // _redoButton     = levelEditorCanvasRect.FindPath(property.REDO_BUTTON).GetComponent<Button>();
            // _viewButton     = levelEditorCanvasRect.FindPath(property.VIEW_BUTTON).GetComponent<Button>();
            // _positionButton = levelEditorCanvasRect.FindPath(property.POSITON_BUTTON).GetComponent<Button>();
            // _rotationButton = levelEditorCanvasRect.FindPath(property.ROTATION_BUTTON).GetComponent<Button>();
            // _scaleButton    = levelEditorCanvasRect.FindPath(property.SCALE_BUTTON).GetComponent<Button>();
            // _rectButton     = levelEditorCanvasRect.FindPath(property.RECT_BUTTON).GetComponent<Button>();
            //
            _undoButton.onClick.AddListener(CommandInvoker.Undo);
            _redoButton.onClick.AddListener(CommandInvoker.Redo);
        }
    }
}