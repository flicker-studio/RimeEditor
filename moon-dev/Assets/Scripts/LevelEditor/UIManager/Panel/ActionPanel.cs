using Frame.Static.Extensions;
using Struct;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ActionPanel
{
    private Button m_undoButton;

    private Button m_redoButton;

    private Button m_viewButton;

    private Button m_positionButton;

    private Button m_rotationButton;

    private Button m_scaleButton;

    private Button m_rectButton;
    
    private InputProperty<bool> m_undoInputProperty;

    private InputProperty<bool> m_redoInputProperty;

    private InputProperty<bool> m_viewInputProperty;

    private InputProperty<bool> m_positionInputProperty;

    private InputProperty<bool> m_rotationInputProperty;

    private InputProperty<bool> m_scaleInputProperty;

    private InputProperty<bool> m_rectInputProperty;

    public bool GetUndoInputDown => m_undoInputProperty.GetInputDown;

    public bool GetRedoInputDown => m_redoInputProperty.GetInputDown;

    public bool GetViewInputDown => m_viewInputProperty.GetInputDown;

    public bool GetPositionInputDown => m_positionInputProperty.GetInputDown;

    public bool GetRotationInputDown => m_rotationInputProperty.GetInputDown;

    public bool GetScaleInputDown => m_scaleInputProperty.GetInputDown;

    public bool GetRectInputDown => m_rectInputProperty.GetInputDown;

    public ActionPanel(RectTransform levelEditorCanvasRect,LevelEditorUIProperty levelEditorUIProperty)
    {
        InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
        InitEvent();
    }

    private void InitComponent(RectTransform levelEditorCanvasRect,LevelEditorUIProperty levelEditorUIProperty)
    {
        LevelEditorUIProperty.ActionPanelUIName property = levelEditorUIProperty.GetActionPanelUI.GetActionPanelUIName;
        m_undoButton = levelEditorCanvasRect.FindPath(property.UNDO_BUTTON).GetComponent<Button>();
        m_redoButton = levelEditorCanvasRect.FindPath(property.REDO_BUTTON).GetComponent<Button>();
        m_viewButton = levelEditorCanvasRect.FindPath(property.VIEW_BUTTON).GetComponent<Button>();
        m_positionButton = levelEditorCanvasRect.FindPath(property.POSITON_BUTTON).GetComponent<Button>();
        m_rotationButton = levelEditorCanvasRect.FindPath(property.ROTATION_BUTTON).GetComponent<Button>();
        m_scaleButton = levelEditorCanvasRect.FindPath(property.SCALE_BUTTON).GetComponent<Button>();
        m_rectButton = levelEditorCanvasRect.FindPath(property.RECT_BUTTON).GetComponent<Button>();
    }

    private void InitEvent()
    {
        m_undoButton.AddTriggerEvent(EventTriggerType.PointerClick,
            data => m_undoInputProperty.SetInput = true);
        m_redoButton.AddTriggerEvent(EventTriggerType.PointerClick,
            data => m_redoInputProperty.SetInput = true);
        m_viewButton.AddTriggerEvent(EventTriggerType.PointerClick, 
            data => m_viewInputProperty.SetInput = true);
        m_positionButton.AddTriggerEvent(EventTriggerType.PointerClick,
            data => m_positionInputProperty.SetInput = true);
        m_rotationButton.AddTriggerEvent(EventTriggerType.PointerClick,
            data => m_rotationInputProperty.SetInput = true);
        m_scaleButton.AddTriggerEvent(EventTriggerType.PointerClick,
            data => m_scaleInputProperty.SetInput = true);
        m_rectButton.AddTriggerEvent(EventTriggerType.PointerClick,
            data => m_redoInputProperty.SetInput = true);
    }
}
