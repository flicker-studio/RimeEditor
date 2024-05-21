using Moon.Kernel.Extension;
using Moon.Kernel.Struct;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

namespace LevelEditor
{
    public enum Controlhandleactiontype
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
        public Controlhandleactiontype ControlHandleActionType = Controlhandleactiontype.PositionAxisButton;

        public bool UseGrid;
    }

    public class ControlHandlePanel
    {
        #region Some public get methods.

        public Image                          GetSelectionImage       => _selectionImage;
        public RectTransform                  GetGridRect             => _gridRect;
        public RectTransform                  GetSelectionRect        => _selectionRect;
        public RectTransform                  GetPositionRect         => _positionRect;
        public RectTransform                  GetRotationRect         => _rotationRect;
        public RectTransform                  GetScaleRect            => _scaleRect;
        public RectTransform                  GetScaleXAxisRect       => _scaleXRect;
        public RectTransform                  GetScaleYAxisRect       => _scaleYRect;
        public RectTransform                  GetRectRect             => _rectRect;
        public UISetting.SelectionProperty    GetSelectionProperty    => _property.GetSelectionProperty;
        public UISetting.RotationDragProperty GetRotationDragProperty => _property.GetRotationDragProperty;
        public UISetting.ScaleDragProperty    GetScaleDragProperty    => _property.GetScaleDragProperty;
        public UISetting.MouseCursorProperty  GetMouseCursorProperty  => _property.GetMouseCursorProperty;
        public UISetting.GridSnappingProperty GetGridSnappingProperty => _property.GetGridSnappingProperty;

        public bool GetPositionInputX                 => _positionInputX.GetInput;
        public bool GetPositionInputY                 => _positionInputY.GetInput;
        public bool GetPositionInputXY                => _positionInputXY.GetInput;
        public bool GetScaleInputX                    => _scaleInputX.GetInput;
        public bool GetScaleInputY                    => _scaleInputY.GetInput;
        public bool GetScaleInputXY                   => _scaleInputXY.GetInput;
        public bool GetRectTopRightCornerInput        => _rectTopRightCornerInput.GetInput;
        public bool GetRectTopLeftCornerInput         => _rectTopLeftCornerInput.GetInput;
        public bool GetRectBottomRightCornerInput     => _rectBottomRightCornerInput.GetInput;
        public bool GetRectBottomLeftCornerInput      => _rectBottomLeftCornerInput.GetInput;
        public bool GetRectTopEdgeInput               => _rectTopEdgeInput.GetInput;
        public bool GetRectRightEdgeInput             => _rectRightEdgeInput.GetInput;
        public bool GetRectBottomEdgeInput            => _rectBottomEdgeInput.GetInput;
        public bool GetRectLeftEdgeInput              => _rectLeftEdgeInput.GetInput;
        public bool GetRectCenterInput                => _rectCenterInput.GetInput;
        public bool GetPositionInputXDown             => _positionInputX.GetInputDown;
        public bool GetPositionInputYDown             => _positionInputY.GetInputDown;
        public bool GetPositionInputXYDown            => _positionInputXY.GetInputDown;
        public bool GetRotationInputZDown             => _rotationInputZ.GetInputDown;
        public bool GetScaleInputXDown                => _scaleInputX.GetInputDown;
        public bool GetScaleInputYDown                => _scaleInputY.GetInputDown;
        public bool GetScaleInputXYDown               => _scaleInputXY.GetInputDown;
        public bool GetRectTopRightCornerInputDown    => _rectTopRightCornerInput.GetInputDown;
        public bool GetRectTopLeftCornerInputDown     => _rectTopLeftCornerInput.GetInputDown;
        public bool GetRectBottomRightCornerInputDown => _rectBottomRightCornerInput.GetInputDown;
        public bool GetRectBottomLeftCornerInputDown  => _rectBottomLeftCornerInput.GetInputDown;
        public bool GetRectTopEdgeInputDown           => _rectTopEdgeInput.GetInputDown;
        public bool GetRectRightEdgeInputDown         => _rectRightEdgeInput.GetInputDown;
        public bool GetRectBottomEdgeInputDown        => _rectBottomEdgeInput.GetInputDown;
        public bool GetRectLeftEdgeInputDown          => _rectLeftEdgeInput.GetInputDown;
        public bool GetRectCenterInputDown            => _rectCenterInput.GetInputDown;

        public ControlHandleAction GetControlHandleAction => _controlHandleAction;

        #endregion

        #region Selection's rect transform components.

        private RectTransform _selectionRect;

        #endregion

        #region Position's rect transform components.

        private RectTransform _positionRect;

        #endregion

        #region Rotation's rect transform components.

        private RectTransform _rotationRect;

        #endregion

        #region Scale's rect transform components;

        private RectTransform _scaleRect;

        private RectTransform _scaleXRect;

        private RectTransform _scaleYRect;

        #endregion

        #region Rect's rect transform components.

        private RectTransform _rectRect;

        private RectTransform _rectTopRightCornerRect;

        private RectTransform _rectTopLeftCornerRect;

        private RectTransform _rectBottomRightCornerRect;

        private RectTransform _rectBottomLeftCornerRect;

        private RectTransform _rectTopEdgeRect;

        private RectTransform _rectRightEdgeRect;

        private RectTransform _rectBottomEdgeRect;

        private RectTransform _rectLeftEdgeRect;

        private RectTransform _rectCenterRect;

        #endregion

        #region Position's button components.

        private Button _positionButtonX;

        private Button _positionButtonY;

        private Button _positionButtonXY;

        #endregion

        #region Rotation's button components.

        private Button _rotationButtonZ;

        #endregion

        #region Scale's button components.

        private Button _scaleButtonXHead;

        private Button _scaleButtonXBody;

        private Button _scaleButtonYHead;

        private Button _scaleButtonYBody;

        private Button _scaleButtonXY;

        #endregion

        #region Rect's button components.

        private Button _rectTopRightCornerButton;

        private Button _rectTopLeftCornerButton;

        private Button _rectBottomRightCornerButton;

        private Button _rectBottomLeftCornerButton;

        private Button _rectTopEdgeButton;

        private Button _rectRightEdgeButton;

        private Button _rectLeftEdgeButton;

        private Button _rectBottomEdgeButton;

        private Button _rectCenterButton;

        #endregion

        private RectTransform _gridRect;

        private Image _selectionImage;

        private Input<bool> _positionInputX;

        private Input<bool> _positionInputY;

        private Input<bool> _positionInputXY;

        private Input<bool> _rotationInputZ;

        private Input<bool> _scaleInputX;

        private Input<bool> _scaleInputY;

        private Input<bool> _scaleInputXY;

        private Input<bool> _rectTopRightCornerInput;

        private Input<bool> _rectTopLeftCornerInput;

        private Input<bool> _rectBottomRightCornerInput;

        private Input<bool> _rectBottomLeftCornerInput;

        private Input<bool> _rectTopEdgeInput;

        private Input<bool> _rectRightEdgeInput;

        private Input<bool> _rectBottomEdgeInput;

        private Input<bool> _rectLeftEdgeInput;

        private Input<bool> _rectCenterInput;

        private readonly ControlHandleAction _controlHandleAction = new();

        private GridPainter _gridPainter;

        private UISetting.ControlHandleUI _property;
        
        private bool GetMouseLeftButton => Moon.Runtime.InputManager.Instance.GetMouseLeftButton;

        public ControlHandlePanel(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
            InitEvent();
        }

        private void InitComponent(RectTransform rect, UISetting levelEditorUISetting)
        {
            _property = levelEditorUISetting.GetControlHandleUI;
            var uiName = _property.GetControlHandleUIName;
            _selectionImage              = rect.FindPath(uiName.SELECTION_UI_NAME).GetComponent<Image>();
            _gridRect                    = rect.FindPath(uiName.GRID_UI_NAME) as RectTransform;
            _selectionRect               = rect.FindPath(uiName.SELECTION_UI_NAME) as RectTransform;
            _positionRect                = rect.FindPath(uiName.POSITION_AXIS) as RectTransform;
            _rotationRect                = rect.FindPath(uiName.ROTATION_AXIS) as RectTransform;
            _scaleRect                   = rect.FindPath(uiName.SCALE_AXIS) as RectTransform;
            _scaleXRect                  = rect.FindPath(uiName.SCALE_AXIS_X) as RectTransform;
            _scaleYRect                  = rect.FindPath(uiName.SCALE_AXIS_Y) as RectTransform;
            _rectRect                    = rect.FindPath(uiName.RECT_AXIS) as RectTransform;
            _rectTopLeftCornerRect       = rect.FindPath(uiName.RECT_TOP_LEFT_CORNER) as RectTransform;
            _rectTopRightCornerRect      = rect.FindPath(uiName.RECT_TOP_RIGHT_CORNER) as RectTransform;
            _rectBottomLeftCornerRect    = rect.FindPath(uiName.RECT_BOTTOM_LEFT_CORNER) as RectTransform;
            _rectBottomRightCornerRect   = rect.FindPath(uiName.RECT_BOTTOM_RIGHT_CORNER) as RectTransform;
            _rectTopEdgeRect             = rect.FindPath(uiName.RECT_TOP_EDGE) as RectTransform;
            _rectRightEdgeRect           = rect.FindPath(uiName.RECT_RIGHT_EDGE) as RectTransform;
            _rectLeftEdgeRect            = rect.FindPath(uiName.RECT_LEFT_EDGE) as RectTransform;
            _rectCenterRect              = rect.FindPath(uiName.RECT_CENTER) as RectTransform;
            _rectBottomEdgeRect          = rect.FindPath(uiName.RECT_BOTTOM_EDGE) as RectTransform;
            _positionButtonX             = rect.FindPath(uiName.POSITION_AXIS_X).GetComponent<Button>();
            _positionButtonY             = rect.FindPath(uiName.POSITION_AXIS_Y).GetComponent<Button>();
            _positionButtonXY            = rect.FindPath(uiName.POSITION_AXIS_XY).GetComponent<Button>();
            _rotationButtonZ             = rect.FindPath(uiName.ROTATION_AXIS).GetComponent<Button>();
            _scaleButtonXBody            = rect.FindPath(uiName.SCALE_AXIS_X_BODY).GetComponent<Button>();
            _scaleButtonXHead            = rect.FindPath(uiName.SCALE_AXIS_X_HEAD).GetComponent<Button>();
            _scaleButtonYBody            = rect.FindPath(uiName.SCALE_AXIS_Y_BODY).GetComponent<Button>();
            _scaleButtonYHead            = rect.FindPath(uiName.SCALE_AXIS_Y_HEAD).GetComponent<Button>();
            _scaleButtonXY               = rect.FindPath(uiName.SCALE_AXIS_XY).GetComponent<Button>();
            _rectTopLeftCornerButton     = _rectTopLeftCornerRect.GetComponent<Button>();
            _rectTopRightCornerButton    = _rectTopRightCornerRect.GetComponent<Button>();
            _rectBottomLeftCornerButton  = _rectBottomLeftCornerRect.GetComponent<Button>();
            _rectBottomRightCornerButton = _rectBottomRightCornerRect.GetComponent<Button>();
            _rectTopEdgeButton           = _rectTopEdgeRect.GetComponent<Button>();
            _rectRightEdgeButton         = _rectRightEdgeRect.GetComponent<Button>();
            _rectLeftEdgeButton          = _rectLeftEdgeRect.GetComponent<Button>();
            _rectBottomEdgeButton        = _rectBottomEdgeRect.GetComponent<Button>();
            _rectCenterButton            = _rectCenterRect.GetComponent<Button>();
        }

        private void InitEvent()
        {
            _positionButtonX.AddTriggerEvent(EventTriggerType.PointerDown,
                                             data =>
                                             {
                                                 if (GetMouseLeftButton) _positionInputX.SetInput = true;
                                             });

            _positionButtonY.AddTriggerEvent(EventTriggerType.PointerDown,
                                             data =>
                                             {
                                                 if (GetMouseLeftButton) _positionInputY.SetInput = true;
                                             });

            _positionButtonXY.AddTriggerEvent(EventTriggerType.PointerDown,
                                              data =>
                                              {
                                                  if (GetMouseLeftButton) _positionInputXY.SetInput = true;
                                              });

            _rotationButtonZ.AddTriggerEvent(EventTriggerType.PointerDown,
                                             data =>
                                             {
                                                 if (GetMouseLeftButton) _rotationInputZ.SetInput = true;
                                             });

            _scaleButtonXHead.AddTriggerEvent(EventTriggerType.PointerDown,
                                              data =>
                                              {
                                                  if (GetMouseLeftButton) _scaleInputX.SetInput = true;
                                              });

            _scaleButtonYHead.AddTriggerEvent(EventTriggerType.PointerDown,
                                              data =>
                                              {
                                                  if (GetMouseLeftButton) _scaleInputY.SetInput = true;
                                              });

            _scaleButtonXBody.AddTriggerEvent(EventTriggerType.PointerDown,
                                              data =>
                                              {
                                                  if (GetMouseLeftButton) _scaleInputX.SetInput = true;
                                              });

            _scaleButtonYBody.AddTriggerEvent(EventTriggerType.PointerDown,
                                              data =>
                                              {
                                                  if (GetMouseLeftButton) _scaleInputY.SetInput = true;
                                              });

            _scaleButtonXY.AddTriggerEvent(EventTriggerType.PointerDown,
                                           data =>
                                           {
                                               if (GetMouseLeftButton) _scaleInputXY.SetInput = true;
                                           });

            _rectTopRightCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                      data =>
                                                      {
                                                          if (GetMouseLeftButton) _rectTopRightCornerInput.SetInput = true;
                                                      });

            _rectTopLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                     data =>
                                                     {
                                                         if (GetMouseLeftButton) _rectTopLeftCornerInput.SetInput = true;
                                                     });

            _rectBottomLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                        data =>
                                                        {
                                                            if (GetMouseLeftButton) _rectBottomLeftCornerInput.SetInput = true;
                                                        });

            _rectBottomRightCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                         data =>
                                                         {
                                                             if (GetMouseLeftButton) _rectBottomRightCornerInput.SetInput = true;
                                                         });

            _rectTopEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                               data =>
                                               {
                                                   if (GetMouseLeftButton) _rectTopEdgeInput.SetInput = true;
                                               });

            _rectRightEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                 data =>
                                                 {
                                                     if (GetMouseLeftButton) _rectRightEdgeInput.SetInput = true;
                                                 });

            _rectBottomEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                  data =>
                                                  {
                                                      if (GetMouseLeftButton) _rectBottomEdgeInput.SetInput = true;
                                                  });

            _rectLeftEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                data =>
                                                {
                                                    if (GetMouseLeftButton) _rectLeftEdgeInput.SetInput = true;
                                                });

            _rectCenterButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                              data =>
                                              {
                                                  if (GetMouseLeftButton) _rectCenterInput.SetInput = true;
                                              });

            _positionButtonX.AddTriggerEvent(EventTriggerType.PointerUp,
                                             data => _positionInputX.SetInput = false);

            _positionButtonY.AddTriggerEvent(EventTriggerType.PointerUp,
                                             data => _positionInputY.SetInput = false);

            _positionButtonXY.AddTriggerEvent(EventTriggerType.PointerUp,
                                              data => _positionInputXY.SetInput = false);

            _rotationButtonZ.AddTriggerEvent(EventTriggerType.PointerUp,
                                             data => _rotationInputZ.SetInput = false);

            _scaleButtonXHead.AddTriggerEvent(EventTriggerType.PointerUp,
                                              data => _scaleInputX.SetInput = false);

            _scaleButtonYHead.AddTriggerEvent(EventTriggerType.PointerUp,
                                              data => _scaleInputY.SetInput = false);

            _scaleButtonXBody.AddTriggerEvent(EventTriggerType.PointerUp,
                                              data => _scaleInputX.SetInput = false);

            _scaleButtonYBody.AddTriggerEvent(EventTriggerType.PointerUp,
                                              data => _scaleInputY.SetInput = false);

            _scaleButtonXY.AddTriggerEvent(EventTriggerType.PointerUp,
                                           data => _scaleInputXY.SetInput = false);

            _rectTopRightCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                      data => _rectTopRightCornerInput.SetInput = false);

            _rectTopLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                     data => _rectTopLeftCornerInput.SetInput = false);

            _rectBottomLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                        data => _rectBottomLeftCornerInput.SetInput = false);

            _rectBottomRightCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                         data => _rectBottomRightCornerInput.SetInput = false);

            _rectTopEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                               data => _rectTopEdgeInput.SetInput = false);

            _rectRightEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                 data => _rectRightEdgeInput.SetInput = false);

            _rectBottomEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                  data => _rectBottomEdgeInput.SetInput = false);

            _rectLeftEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                data => _rectLeftEdgeInput.SetInput = false);

            _rectCenterButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                              data => _rectCenterInput.SetInput = false);
        }
    }
}