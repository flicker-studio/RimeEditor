using Moon.Kernel.Extension;
using Moon.Kernel.Struct;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelEditor
{
    public class ActionPanel
    {
        private readonly Button m_undoButton;

        private readonly Button m_redoButton;

        private readonly Button m_viewButton;

        private readonly Button m_positionButton;

        private readonly Button m_rotationButton;

        private readonly Button m_scaleButton;

        private readonly Button m_rectButton;

        private Input<bool> m_undoInput;

        private Input<bool> m_redoInput;

        private Input<bool> m_viewInput;

        private Input<bool> m_positionInput;

        private Input<bool> m_rotationInput;

        private Input<bool> m_scaleInput;

        private Input<bool> m_rectInput;

        public bool GetUndoInputDown => m_undoInput.GetInputDown;

        public bool GetRedoInputDown => m_redoInput.GetInputDown;

        public bool GetViewInputDown => m_viewInput.GetInputDown;

        public bool GetPositionInputDown => m_positionInput.GetInputDown;

        public bool GetRotationInputDown => m_rotationInput.GetInputDown;

        public bool GetScaleInputDown => m_scaleInput.GetInputDown;

        public bool GetRectInputDown => m_rectInput.GetInputDown;

        public ActionPanel(Transform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            var property = levelEditorUISetting.GetActionPanelUI.GetActionPanelUIName;
            m_undoButton     = levelEditorCanvasRect.FindPath(property.UNDO_BUTTON).GetComponent<Button>();
            m_redoButton     = levelEditorCanvasRect.FindPath(property.REDO_BUTTON).GetComponent<Button>();
            m_viewButton     = levelEditorCanvasRect.FindPath(property.VIEW_BUTTON).GetComponent<Button>();
            m_positionButton = levelEditorCanvasRect.FindPath(property.POSITON_BUTTON).GetComponent<Button>();
            m_rotationButton = levelEditorCanvasRect.FindPath(property.ROTATION_BUTTON).GetComponent<Button>();
            m_scaleButton    = levelEditorCanvasRect.FindPath(property.SCALE_BUTTON).GetComponent<Button>();
            m_rectButton     = levelEditorCanvasRect.FindPath(property.RECT_BUTTON).GetComponent<Button>();

            m_undoButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_undoInput.SetInput = true);

            m_redoButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_redoInput.SetInput = true);

            m_viewButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_viewInput.SetInput = true);

            m_positionButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_positionInput.SetInput = true);

            m_rotationButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_rotationInput.SetInput = true);

            m_scaleButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_scaleInput.SetInput = true);

            m_rectButton.AddTriggerEvent(EventTriggerType.PointerClick,
                data => m_rectInput.SetInput = true);
        }
    }
}