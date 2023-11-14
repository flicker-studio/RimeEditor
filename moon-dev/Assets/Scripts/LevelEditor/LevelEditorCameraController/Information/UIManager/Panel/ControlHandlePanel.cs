using Frame.Static.Extensions;
using Frame.Tool;
using Struct;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace LevelEditor
{
    public enum CONTROLHANDLEACTIONTYPE
    {
        UndoButton,
        RedoButton,
        ViewButton,
        PositionAxisButton,
        RotationAxisButton,
        ScaleButton,
        RectButton
    }
    
    public class ControlHandleAction
    {
        public CONTROLHANDLEACTIONTYPE ControlHandleActionType = CONTROLHANDLEACTIONTYPE.PositionAxisButton;
    }
    
    public class ControlHandlePanel
    {
        public Image GetSelectionImage => m_selectionImage;
        
        public RectTransform GetSelectionRect => m_selectionRect;
        
        public RectTransform GetPositionRect => m_positionRect;
    
        public RectTransform GetRotationRect => m_rotationRect;
    
        public UIProperty.SelectionProperty GetSelectionProperty => m_property.GetSelectionProperty;
    
        public UIProperty.RotationDragProperty GetRotationDragProperty => m_property.GetRotationDragProperty;

        public UIProperty.MouseCursorProperty GetMouseCursorProperty => m_property.GetMouseCursorProperty;
    
        public bool GetPositionInputX => m_positionInputX.GetInput;
    
        public bool GetPositionInputY => m_positionInputY.GetInput;
    
        public bool GetPositionInputXY => m_positionInputXY.GetInput;
    
        public bool GetRotationInputZ => m_rotationInputZ.GetInput;
    
        public bool GetPositionInputXDown => m_positionInputX.GetInputDown;
    
        public bool GetPositionInputYDown => m_positionInputY.GetInputDown;
    
        public bool GetPositionInputXYDown => m_positionInputXY.GetInputDown;
    
        public bool GetRotationInputZDown => m_rotationInputZ.GetInputDown;
        
        public ControlHandleAction GetControlHandleAction => m_controlHandleAction;
    
        private Image m_selectionImage;
        
        private RectTransform m_selectionRect;
        
        private RectTransform m_positionRect;
    
        private RectTransform m_rotationRect;
        
        private Button m_positionButtonX;
    
        private Button m_positionButtonY;
    
        private Button m_positionButtonXY;
    
        private Button m_rotationButtonZ;
        
        private InputProperty<bool> m_positionInputX;
        
        private InputProperty<bool> m_positionInputY;
        
        private InputProperty<bool> m_positionInputXY;
    
        private InputProperty<bool> m_rotationInputZ;
        
        private ControlHandleAction m_controlHandleAction = new ControlHandleAction();
    
        private UIProperty.ControlHandleUI m_property;
    
        private bool GetMouseLeftButton => InputManager.Instance.GetMouseLeftButton;
        
        public ControlHandlePanel(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
            InitEvent();
        }
        
        private void InitComponent(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            m_property = levelEditorUIProperty.GetControlHandleUI;
            UIProperty.ControlHandleUIName uiName = m_property.GetControlHandleUIName;
            m_selectionImage = levelEditorCanvasRect.FindPath(uiName.SELECTION_UI_NAME).GetComponent<Image>();
            m_selectionRect = levelEditorCanvasRect.FindPath(uiName.SELECTION_UI_NAME) as RectTransform;
            m_positionRect = levelEditorCanvasRect.FindPath(uiName.POSITION_AXIS) as RectTransform;
            m_rotationRect = levelEditorCanvasRect.FindPath(uiName.ROTATION_AXIS) as RectTransform;
            m_positionButtonX = levelEditorCanvasRect.FindPath(uiName.POSITION_AXIS_X).GetComponent<Button>();
            m_positionButtonY = levelEditorCanvasRect.FindPath(uiName.POSITION_AXIS_Y).GetComponent<Button>();
            m_positionButtonXY = levelEditorCanvasRect.FindPath(uiName.POSITION_AXIS_XY).GetComponent<Button>();
            m_rotationButtonZ = levelEditorCanvasRect.FindPath(uiName.ROTATION_AXIS).GetComponent<Button>();
        }
        
        private void InitEvent()
        {
            m_positionButtonX.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_positionInputX.SetInput = true;
                });
            
            m_positionButtonY.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_positionInputY.SetInput = true;
                });
            
            m_positionButtonXY.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_positionInputXY.SetInput = true;
                });
    
            m_rotationButtonZ.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_rotationInputZ.SetInput = true;
                });
            
            m_positionButtonX.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_positionInputX.SetInput = false);
            
            m_positionButtonY.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_positionInputY.SetInput = false);
            
            m_positionButtonXY.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_positionInputXY.SetInput = false);
    
            m_rotationButtonZ.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_rotationInputZ.SetInput = false);
        }
    }
}
