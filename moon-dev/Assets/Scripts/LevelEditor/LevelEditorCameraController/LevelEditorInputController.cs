using System.Collections;
using System.Collections.Generic;
using Frame.Tool;
using Struct;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class LevelEditorInputController
{
    private enum BUTTONTYPE
    {
        PositionAxisXButton,
        PositionAxisYButton,
        PositionAxisXYButton,
        RotationAxisZButton,
        UndoButton,
        RedoButton,
        ViewButton,
        PositionButton,
        RotationButton,
        ScaleButton,
        RectButton
    }
    
    private InputProperty<bool> m_positionAxisXButton;
    
    private InputProperty<bool> m_positionAxisYButton;
    
    private InputProperty<bool> m_positionAxisXYButton;

    private InputProperty<bool> m_rotationAxisZButton;

    private InputProperty<bool> m_undoButton;

    private InputProperty<bool> m_redoButton;

    private InputProperty<bool> m_viewButton;

    private InputProperty<bool> m_positionButton;

    private InputProperty<bool> m_rotationButton;

    private InputProperty<bool> m_scaleButton;

    private InputProperty<bool> m_rectButton;
    
    private RectTransform m_undoButtonRectTransform;

    private RectTransform m_redoButtonRectTransform;

    private RectTransform m_viewButtonRectTransform;

    private RectTransform m_positionButtonRectTransform;

    private RectTransform m_rotationButtonRectTransform;

    private RectTransform m_scaleButtonRectTransform;

    private RectTransform m_rectButtonRectTransform;
    
    private RectTransform m_positionAxisRectTransform;

    private RectTransform m_rotationAxisRectTransform;

    private RectTransform m_actionPanelRectTransform;

    private RectTransform m_topPanelRectTransform;

    public RectTransform GetPositionAxisRectTransform => m_positionAxisRectTransform;

    public RectTransform GetRotationAxisRectTransform => m_rotationAxisRectTransform;
    
    public bool GetPositionAxisXButtonDown => m_positionAxisXButton.GetInputDown;
    
    public bool GetPositionAxisYButtonDown => m_positionAxisYButton.GetInputDown;
    
    public bool GetPositionAxisXYButtonDown => m_positionAxisXYButton.GetInputDown;

    public bool GetRotationAxisZButtonDown => m_rotationAxisZButton.GetInputDown;

    public bool GetUndoButtonDown => m_undoButton.GetInputDown;

    public bool GetRedoButtonDown => m_redoButton.GetInputDown;

    public bool GetViewButtonDown => m_viewButton.GetInputDown;

    public bool GetPositionButtonDown => m_positionButton.GetInputDown;

    public bool GetRotationButtonDown => m_rotationButton.GetInputDown;

    public bool GetScaleButtonDown => m_scaleButton.GetInputDown;

    public bool GetRectButtonDown => m_scaleButton.GetInputDown;
    
    public bool GetPositionAxisXButton => m_positionAxisXButton.GetInput;
    
    public bool GetPositionAxisYButton => m_positionAxisYButton.GetInput;
    
    public bool GetPositionAxisXYButton => m_positionAxisXYButton.GetInput;

    public bool GetRotationAxisZButton => m_rotationAxisZButton.GetInput;
    
    public bool GetMouseLeftButton => InputManager.Instance.GetMouseLeftButton;
    
    public bool GetMouseLeftButtonDown => InputManager.Instance.GetMouseLeftButtonDown;
    
    public bool GetMouseLeftButtonUp => InputManager.Instance.GetMouseLeftButtonUp;
    
    public bool GetMouseRightButton => InputManager.Instance.GetMouseRightButton;
    
    public bool GetMouseRightButtonDown => InputManager.Instance.GetMouseRightButtonDown;
    
    public bool GetMouseRightButtonUp => InputManager.Instance.GetMouseRightButtonUp;
    
    public bool GetMouseMiddleButton => InputManager.Instance.GetMouseMiddleButton;

    public float GetMouseSroll => InputManager.Instance.GetMouseScroll;
    
    public bool GetMouseSrollDown => InputManager.Instance.GetMouseScrollDown;
    
    public bool GetMouseSrollUp => InputManager.Instance.GetMouseScrollUp;
    
    public bool GetMouseMiddleButtonDown => InputManager.Instance.GetMouseMiddleButtonDown;
    
    public bool GetMouseMiddleButtonUp => InputManager.Instance.GetMouseMiddleButtonUp;

    public bool GetShiftButton => InputManager.Instance.GetShiftButton;

    public bool GetCtrlButton => InputManager.Instance.GetCtrlButton;

    public LevelEditorInputController(RectTransform levelEditorTransform,LevelEditorProperty property)
    {
        InitButton(levelEditorTransform, property);
    }
    
    private void InitButton(RectTransform levelEditorTransform,LevelEditorProperty property)
    {
        LevelEditorProperty.UIProperty GetUIProperty = property.GetUIProperty;
        m_topPanelRectTransform =  levelEditorTransform.Find(GetUIProperty.TOP_PANEL) as RectTransform;
        m_positionAxisRectTransform = levelEditorTransform.Find(GetUIProperty.POSITION_AXIS) as RectTransform;
        m_rotationAxisRectTransform = levelEditorTransform.Find(GetUIProperty.ROTATION_AXIS) as RectTransform;
        m_actionPanelRectTransform = m_topPanelRectTransform.Find(GetUIProperty.ACTION_PANEL) as RectTransform;
        m_undoButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.UNDO_BUTTON) as RectTransform;
        m_redoButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.REDO_BUTTON) as RectTransform;
        m_viewButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.VIEW_BUTTON) as RectTransform;
        m_positionButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.POSITON_BUTTON) as RectTransform;
        m_rotationButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.ROTATION_BUTTON) as RectTransform;
        m_scaleButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.SCALE_BUTTON) as RectTransform;
        m_rectButtonRectTransform = m_actionPanelRectTransform.Find(GetUIProperty.RECT_BUTTON) as RectTransform;
        
        m_positionAxisRectTransform.Find(GetUIProperty.POSITION_AXIS_X).GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerDown, 
                data => SetButtonValue(BUTTONTYPE.PositionAxisXButton,true));
        m_positionAxisRectTransform.Find(GetUIProperty.POSITION_AXIS_Y).GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerDown, 
                data => SetButtonValue(BUTTONTYPE.PositionAxisYButton,true));
        m_positionAxisRectTransform.Find(GetUIProperty.POSITION_AXIS_XY).GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerDown, 
                data => SetButtonValue(BUTTONTYPE.PositionAxisXYButton,true));
        m_rotationAxisRectTransform.GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerDown, 
                data => SetButtonValue(BUTTONTYPE.RotationAxisZButton,true));
        m_undoButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.UndoButton, true));
        m_redoButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.RedoButton, true));
        m_viewButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.ViewButton, true));
        m_positionButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.PositionButton, true));
        m_rotationButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.RotationButton, true));
        m_scaleButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.ScaleButton, true));
        m_rectButtonRectTransform.GetComponent<Button>().AddTriggersEvents(EventTriggerType.PointerClick,
            data => SetButtonValue(BUTTONTYPE.RectButton, true));
        
        m_positionAxisRectTransform.Find(GetUIProperty.POSITION_AXIS_X).GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerUp, 
                data => SetButtonValue(BUTTONTYPE.PositionAxisXButton,false));
        m_positionAxisRectTransform.Find(GetUIProperty.POSITION_AXIS_Y).GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerUp, 
                data => SetButtonValue(BUTTONTYPE.PositionAxisYButton,false));
        m_positionAxisRectTransform.Find(GetUIProperty.POSITION_AXIS_XY).GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerUp, 
                data => SetButtonValue(BUTTONTYPE.PositionAxisXYButton,false));
        m_rotationAxisRectTransform.GetComponent<Button>()
            .AddTriggersEvents(EventTriggerType.PointerUp, 
                data => SetButtonValue(BUTTONTYPE.RotationAxisZButton,false));
    }
    
    private void SetButtonValue(BUTTONTYPE buttontype,bool value)
    {
        switch (buttontype)
        {
            case BUTTONTYPE.PositionAxisXButton:
                m_positionAxisXButton.SetInput = value;
                break;
            case BUTTONTYPE.PositionAxisYButton:
                m_positionAxisYButton.SetInput = value;
                break;
            case BUTTONTYPE.PositionAxisXYButton:
                m_positionAxisXYButton.SetInput = value;
                break;
            case BUTTONTYPE.RotationAxisZButton:
                m_rotationAxisZButton.SetInput = value;
                break;
            case BUTTONTYPE.PositionButton:
                m_positionButton.SetInput = value;
                break;
            case BUTTONTYPE.RotationButton:
                m_rotationButton.SetInput = value;
                break;
            case BUTTONTYPE.UndoButton:
                m_undoButton.SetInput = value;
                break;
            case BUTTONTYPE.RedoButton:
                m_redoButton.SetInput = value;
                break;
            case BUTTONTYPE.RectButton:
                m_rectButton.SetInput = value;
                break;
            case BUTTONTYPE.ScaleButton:
                m_scaleButton.SetInput = value;
                break;
            case BUTTONTYPE.ViewButton:
                m_viewButton.SetInput = value;
                break;
            default:
                return;
        }
    }
}
