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
        ScaleAxisButton,
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

        public RectTransform GetScaleRect => m_scaleRect;

        public RectTransform GetScaleXAxisRect => m_scaleXRect;
        
        public RectTransform GetScaleYAxisRect => m_scaleYRect;
    
        public UIProperty.SelectionProperty GetSelectionProperty => m_property.GetSelectionProperty;
    
        public UIProperty.RotationDragProperty GetRotationDragProperty => m_property.GetRotationDragProperty;

        public UIProperty.ScaleDragProperty GetScaleDragProperty => m_property.GetScaleDragProperty;
        public UIProperty.MouseCursorProperty GetMouseCursorProperty => m_property.GetMouseCursorProperty;
    
        public bool GetPositionInputX => m_positionInputX.GetInput;
    
        public bool GetPositionInputY => m_positionInputY.GetInput;
    
        public bool GetPositionInputXY => m_positionInputXY.GetInput;
        
        public bool GetScaleInputX => m_scaleInputX.GetInput;
        
        public bool GetScaleInputY => m_scaleInputY.GetInput;
        
        public bool GetScaleInputXY => m_scaleInputXY.GetInput;
    
        public bool GetPositionInputXDown => m_positionInputX.GetInputDown;
    
        public bool GetPositionInputYDown => m_positionInputY.GetInputDown;
    
        public bool GetPositionInputXYDown => m_positionInputXY.GetInputDown;
    
        public bool GetRotationInputZDown => m_rotationInputZ.GetInputDown;

        public bool GetScaleInputXDown => m_scaleInputX.GetInputDown;
        
        public bool GetScaleInputYDown => m_scaleInputY.GetInputDown;
        
        public bool GetScaleInputXYDown => m_scaleInputXY.GetInputDown;
        public ControlHandleAction GetControlHandleAction => m_controlHandleAction;
    
        private Image m_selectionImage;
        
        private RectTransform m_selectionRect;
        
        private RectTransform m_positionRect;
    
        private RectTransform m_rotationRect;

        private RectTransform m_scaleRect;

        private RectTransform m_scaleXRect;
        
        private RectTransform m_scaleYRect;
        
        private Button m_positionButtonX;
    
        private Button m_positionButtonY;
    
        private Button m_positionButtonXY;
    
        private Button m_rotationButtonZ;
        
        private Button m_scaleButtonXHead;
        
        private Button m_scaleButtonXBody;
    
        private Button m_scaleButtonYHead;
        
        private Button m_scaleButtonYBody;
    
        private Button m_scaleButtonXY;
        
        private InputProperty<bool> m_positionInputX;
        
        private InputProperty<bool> m_positionInputY;
        
        private InputProperty<bool> m_positionInputXY;
    
        private InputProperty<bool> m_rotationInputZ;
        
        private InputProperty<bool> m_scaleInputX;
        
        private InputProperty<bool> m_scaleInputY;
        
        private InputProperty<bool> m_scaleInputXY;
        
        private ControlHandleAction m_controlHandleAction = new ControlHandleAction();
    
        private UIProperty.ControlHandleUI m_property;
    
        private bool GetMouseLeftButton => InputManager.Instance.GetMouseLeftButton;
        
        public ControlHandlePanel(RectTransform levelEditorCanvasRect,UIProperty levelEditorUIProperty)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUIProperty);
            InitEvent();
        }
        
        private void InitComponent(RectTransform rect,UIProperty levelEditorUIProperty)
        {
            m_property = levelEditorUIProperty.GetControlHandleUI;
            UIProperty.ControlHandleUIName uiName = m_property.GetControlHandleUIName;
            m_selectionImage = rect.FindPath(uiName.SELECTION_UI_NAME).GetComponent<Image>();
            m_selectionRect = rect.FindPath(uiName.SELECTION_UI_NAME) as RectTransform;
            m_positionRect = rect.FindPath(uiName.POSITION_AXIS) as RectTransform;
            m_rotationRect = rect.FindPath(uiName.ROTATION_AXIS) as RectTransform;
            m_scaleRect = rect.FindPath(uiName.SCALE_AXIS) as RectTransform;
            m_scaleXRect = rect.FindPath(uiName.SCALE_AXIS_X) as RectTransform;
            m_scaleYRect = rect.FindPath(uiName.SCALE_AXIS_Y) as RectTransform;
            m_positionButtonX = rect.FindPath(uiName.POSITION_AXIS_X).GetComponent<Button>();
            m_positionButtonY = rect.FindPath(uiName.POSITION_AXIS_Y).GetComponent<Button>();
            m_positionButtonXY = rect.FindPath(uiName.POSITION_AXIS_XY).GetComponent<Button>();
            m_rotationButtonZ = rect.FindPath(uiName.ROTATION_AXIS).GetComponent<Button>();
            m_scaleButtonXBody = rect.FindPath(uiName.SCALE_AXIS_X_BODY).GetComponent<Button>();
            m_scaleButtonXHead = rect.FindPath(uiName.SCALE_AXIS_X_HEAD).GetComponent<Button>();
            m_scaleButtonYBody = rect.FindPath(uiName.SCALE_AXIS_Y_BODY).GetComponent<Button>();
            m_scaleButtonYHead = rect.FindPath(uiName.SCALE_AXIS_Y_HEAD).GetComponent<Button>();
            m_scaleButtonXY = rect.FindPath(uiName.SCALE_AXIS_XY).GetComponent<Button>();
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
            
            m_scaleButtonXHead.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_scaleInputX.SetInput = true;
                });
            
            m_scaleButtonYHead.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_scaleInputY.SetInput = true;
                });
            
            m_scaleButtonXBody.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_scaleInputX.SetInput = true;
                });
            
            m_scaleButtonYBody.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_scaleInputY.SetInput = true;
                });
            
            m_scaleButtonXY.AddTriggerEvent(EventTriggerType.PointerDown,
                data =>
                {
                    if (GetMouseLeftButton) m_scaleInputXY.SetInput = true;
                });
            
            m_positionButtonX.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_positionInputX.SetInput = false);
            
            m_positionButtonY.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_positionInputY.SetInput = false);
            
            m_positionButtonXY.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_positionInputXY.SetInput = false);
    
            m_rotationButtonZ.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_rotationInputZ.SetInput = false);
                        
            m_scaleButtonXHead.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_scaleInputX.SetInput = false);
            
            m_scaleButtonYHead.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_scaleInputY.SetInput = false);
            
            m_scaleButtonXBody.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_scaleInputX.SetInput = false);
            
            m_scaleButtonYBody.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_scaleInputY.SetInput = false);
            
            m_scaleButtonXY.AddTriggerEvent(EventTriggerType.PointerUp,
                data => m_scaleInputXY.SetInput = false);
            
        }
    }
}
