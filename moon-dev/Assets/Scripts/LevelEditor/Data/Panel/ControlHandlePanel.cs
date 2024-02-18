using Moon.Kernel.Extension;
using Moon.Kernel.Struct;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using RectTransform = UnityEngine.RectTransform;

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

        public bool UseGrid;
    }

    public class ControlHandlePanel
    {
        #region Some public get methods.

        public Image                          GetSelectionImage                 => m_selectionImage;
        public RectTransform                  GetGridRect                       => m_gridRect;
        public RectTransform                  GetSelectionRect                  => m_selectionRect;
        public RectTransform                  GetPositionRect                   => m_positionRect;
        public RectTransform                  GetRotationRect                   => m_rotationRect;
        public RectTransform                  GetScaleRect                      => m_scaleRect;
        public RectTransform                  GetScaleXAxisRect                 => m_scaleXRect;
        public RectTransform                  GetScaleYAxisRect                 => m_scaleYRect;
        public RectTransform                  GetRectRect                       => m_rectRect;
        public UISetting.SelectionProperty    GetSelectionProperty              => m_property.GetSelectionProperty;
        public UISetting.RotationDragProperty GetRotationDragProperty           => m_property.GetRotationDragProperty;
        public UISetting.ScaleDragProperty    GetScaleDragProperty              => m_property.GetScaleDragProperty;
        public UISetting.MouseCursorProperty  GetMouseCursorProperty            => m_property.GetMouseCursorProperty;
        public UISetting.GridSnappingProperty GetGridSnappingProperty           => m_property.GetGridSnappingProperty;
        public bool                           GetPositionInputX                 => m_positionInputX.GetInput;
        public bool                           GetPositionInputY                 => m_positionInputY.GetInput;
        public bool                           GetPositionInputXY                => m_positionInputXY.GetInput;
        public bool                           GetScaleInputX                    => m_scaleInputX.GetInput;
        public bool                           GetScaleInputY                    => m_scaleInputY.GetInput;
        public bool                           GetScaleInputXY                   => m_scaleInputXY.GetInput;
        public bool                           GetRectTopRightCornerInput        => m_rectTopRightCornerInput.GetInput;
        public bool                           GetRectTopLeftCornerInput         => m_rectTopLeftCornerInput.GetInput;
        public bool                           GetRectBottomRightCornerInput     => m_rectBottomRightCornerInput.GetInput;
        public bool                           GetRectBottomLeftCornerInput      => m_rectBottomLeftCornerInput.GetInput;
        public bool                           GetRectTopEdgeInput               => m_rectTopEdgeInput.GetInput;
        public bool                           GetRectRightEdgeInput             => m_rectRightEdgeInput.GetInput;
        public bool                           GetRectBottomEdgeInput            => m_rectBottomEdgeInput.GetInput;
        public bool                           GetRectLeftEdgeInput              => m_rectLeftEdgeInput.GetInput;
        public bool                           GetRectCenterInput                => m_rectCenterInput.GetInput;
        public bool                           GetPositionInputXDown             => m_positionInputX.GetInputDown;
        public bool                           GetPositionInputYDown             => m_positionInputY.GetInputDown;
        public bool                           GetPositionInputXYDown            => m_positionInputXY.GetInputDown;
        public bool                           GetRotationInputZDown             => m_rotationInputZ.GetInputDown;
        public bool                           GetScaleInputXDown                => m_scaleInputX.GetInputDown;
        public bool                           GetScaleInputYDown                => m_scaleInputY.GetInputDown;
        public bool                           GetScaleInputXYDown               => m_scaleInputXY.GetInputDown;
        public bool                           GetRectTopRightCornerInputDown    => m_rectTopRightCornerInput.GetInputDown;
        public bool                           GetRectTopLeftCornerInputDown     => m_rectTopLeftCornerInput.GetInputDown;
        public bool                           GetRectBottomRightCornerInputDown => m_rectBottomRightCornerInput.GetInputDown;
        public bool                           GetRectBottomLeftCornerInputDown  => m_rectBottomLeftCornerInput.GetInputDown;
        public bool                           GetRectTopEdgeInputDown           => m_rectTopEdgeInput.GetInputDown;
        public bool                           GetRectRightEdgeInputDown         => m_rectRightEdgeInput.GetInputDown;
        public bool                           GetRectBottomEdgeInputDown        => m_rectBottomEdgeInput.GetInputDown;
        public bool                           GetRectLeftEdgeInputDown          => m_rectLeftEdgeInput.GetInputDown;
        public bool                           GetRectCenterInputDown            => m_rectCenterInput.GetInputDown;
        public ControlHandleAction            GetControlHandleAction            => m_controlHandleAction;

        #endregion

        #region Selection's rect transform components.

        private RectTransform m_selectionRect;

        #endregion

        #region Position's rect transform components.

        private RectTransform m_positionRect;

        #endregion

        #region Rotation's rect transform components.

        private RectTransform m_rotationRect;

        #endregion

        #region Scale's rect transform components;

        private RectTransform m_scaleRect;

        private RectTransform m_scaleXRect;

        private RectTransform m_scaleYRect;

        #endregion

        #region Rect's rect transform components.

        private RectTransform m_rectRect;

        private RectTransform m_rectTopRightCornerRect;

        private RectTransform m_rectTopLeftCornerRect;

        private RectTransform m_rectBottomRightCornerRect;

        private RectTransform m_rectBottomLeftCornerRect;

        private RectTransform m_rectTopEdgeRect;

        private RectTransform m_rectRightEdgeRect;

        private RectTransform m_rectBottomEdgeRect;

        private RectTransform m_rectLeftEdgeRect;

        private RectTransform m_rectCenterRect;

        #endregion

        #region Position's button components.

        private Button m_positionButtonX;

        private Button m_positionButtonY;

        private Button m_positionButtonXY;

        #endregion

        #region Rotation's button components.

        private Button m_rotationButtonZ;

        #endregion

        #region Scale's button components.

        private Button m_scaleButtonXHead;

        private Button m_scaleButtonXBody;

        private Button m_scaleButtonYHead;

        private Button m_scaleButtonYBody;

        private Button m_scaleButtonXY;

        #endregion

        #region Rect's button components.

        private Button m_rectTopRightCornerButton;

        private Button m_rectTopLeftCornerButton;

        private Button m_rectBottomRightCornerButton;

        private Button m_rectBottomLeftCornerButton;

        private Button m_rectTopEdgeButton;

        private Button m_rectRightEdgeButton;

        private Button m_rectLeftEdgeButton;

        private Button m_rectBottomEdgeButton;

        private Button m_rectCenterButton;

        #endregion

        private RectTransform m_gridRect;

        private Image m_selectionImage;

        private Input<bool> m_positionInputX;

        private Input<bool> m_positionInputY;

        private Input<bool> m_positionInputXY;

        private Input<bool> m_rotationInputZ;

        private Input<bool> m_scaleInputX;

        private Input<bool> m_scaleInputY;

        private Input<bool> m_scaleInputXY;

        private Input<bool> m_rectTopRightCornerInput;

        private Input<bool> m_rectTopLeftCornerInput;

        private Input<bool> m_rectBottomRightCornerInput;

        private Input<bool> m_rectBottomLeftCornerInput;

        private Input<bool> m_rectTopEdgeInput;

        private Input<bool> m_rectRightEdgeInput;

        private Input<bool> m_rectBottomEdgeInput;

        private Input<bool> m_rectLeftEdgeInput;

        private Input<bool> m_rectCenterInput;

        private readonly ControlHandleAction m_controlHandleAction = new();

        private GridPainter m_gridPainter;

        private UISetting.ControlHandleUI m_property;

        private bool GetMouseLeftButton => Frame.Tool.InputManager.Instance.GetMouseLeftButton;

        public ControlHandlePanel(RectTransform levelEditorCanvasRect, UISetting levelEditorUISetting)
        {
            InitComponent(levelEditorCanvasRect, levelEditorUISetting);
            InitEvent();
        }

        private void InitComponent(RectTransform rect, UISetting levelEditorUISetting)
        {
            m_property = levelEditorUISetting.GetControlHandleUI;
            var uiName = m_property.GetControlHandleUIName;
            m_selectionImage              = rect.FindPath(uiName.SELECTION_UI_NAME).GetComponent<Image>();
            m_gridRect                    = rect.FindPath(uiName.GRID_UI_NAME) as RectTransform;
            m_selectionRect               = rect.FindPath(uiName.SELECTION_UI_NAME) as RectTransform;
            m_positionRect                = rect.FindPath(uiName.POSITION_AXIS) as RectTransform;
            m_rotationRect                = rect.FindPath(uiName.ROTATION_AXIS) as RectTransform;
            m_scaleRect                   = rect.FindPath(uiName.SCALE_AXIS) as RectTransform;
            m_scaleXRect                  = rect.FindPath(uiName.SCALE_AXIS_X) as RectTransform;
            m_scaleYRect                  = rect.FindPath(uiName.SCALE_AXIS_Y) as RectTransform;
            m_rectRect                    = rect.FindPath(uiName.RECT_AXIS) as RectTransform;
            m_rectTopLeftCornerRect       = rect.FindPath(uiName.RECT_TOP_LEFT_CORNER) as RectTransform;
            m_rectTopRightCornerRect      = rect.FindPath(uiName.RECT_TOP_RIGHT_CORNER) as RectTransform;
            m_rectBottomLeftCornerRect    = rect.FindPath(uiName.RECT_BOTTOM_LEFT_CORNER) as RectTransform;
            m_rectBottomRightCornerRect   = rect.FindPath(uiName.RECT_BOTTOM_RIGHT_CORNER) as RectTransform;
            m_rectTopEdgeRect             = rect.FindPath(uiName.RECT_TOP_EDGE) as RectTransform;
            m_rectRightEdgeRect           = rect.FindPath(uiName.RECT_RIGHT_EDGE) as RectTransform;
            m_rectLeftEdgeRect            = rect.FindPath(uiName.RECT_LEFT_EDGE) as RectTransform;
            m_rectCenterRect              = rect.FindPath(uiName.RECT_CENTER) as RectTransform;
            m_rectBottomEdgeRect          = rect.FindPath(uiName.RECT_BOTTOM_EDGE) as RectTransform;
            m_positionButtonX             = rect.FindPath(uiName.POSITION_AXIS_X).GetComponent<Button>();
            m_positionButtonY             = rect.FindPath(uiName.POSITION_AXIS_Y).GetComponent<Button>();
            m_positionButtonXY            = rect.FindPath(uiName.POSITION_AXIS_XY).GetComponent<Button>();
            m_rotationButtonZ             = rect.FindPath(uiName.ROTATION_AXIS).GetComponent<Button>();
            m_scaleButtonXBody            = rect.FindPath(uiName.SCALE_AXIS_X_BODY).GetComponent<Button>();
            m_scaleButtonXHead            = rect.FindPath(uiName.SCALE_AXIS_X_HEAD).GetComponent<Button>();
            m_scaleButtonYBody            = rect.FindPath(uiName.SCALE_AXIS_Y_BODY).GetComponent<Button>();
            m_scaleButtonYHead            = rect.FindPath(uiName.SCALE_AXIS_Y_HEAD).GetComponent<Button>();
            m_scaleButtonXY               = rect.FindPath(uiName.SCALE_AXIS_XY).GetComponent<Button>();
            m_rectTopLeftCornerButton     = m_rectTopLeftCornerRect.GetComponent<Button>();
            m_rectTopRightCornerButton    = m_rectTopRightCornerRect.GetComponent<Button>();
            m_rectBottomLeftCornerButton  = m_rectBottomLeftCornerRect.GetComponent<Button>();
            m_rectBottomRightCornerButton = m_rectBottomRightCornerRect.GetComponent<Button>();
            m_rectTopEdgeButton           = m_rectTopEdgeRect.GetComponent<Button>();
            m_rectRightEdgeButton         = m_rectRightEdgeRect.GetComponent<Button>();
            m_rectLeftEdgeButton          = m_rectLeftEdgeRect.GetComponent<Button>();
            m_rectBottomEdgeButton        = m_rectBottomEdgeRect.GetComponent<Button>();
            m_rectCenterButton            = m_rectCenterRect.GetComponent<Button>();
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

            m_rectTopRightCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                       data =>
                                                       {
                                                           if (GetMouseLeftButton) m_rectTopRightCornerInput.SetInput = true;
                                                       });

            m_rectTopLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                      data =>
                                                      {
                                                          if (GetMouseLeftButton) m_rectTopLeftCornerInput.SetInput = true;
                                                      });

            m_rectBottomLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                         data =>
                                                         {
                                                             if (GetMouseLeftButton) m_rectBottomLeftCornerInput.SetInput = true;
                                                         });

            m_rectBottomRightCornerButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                          data =>
                                                          {
                                                              if (GetMouseLeftButton) m_rectBottomRightCornerInput.SetInput = true;
                                                          });

            m_rectTopEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                data =>
                                                {
                                                    if (GetMouseLeftButton) m_rectTopEdgeInput.SetInput = true;
                                                });

            m_rectRightEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                  data =>
                                                  {
                                                      if (GetMouseLeftButton) m_rectRightEdgeInput.SetInput = true;
                                                  });

            m_rectBottomEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                   data =>
                                                   {
                                                       if (GetMouseLeftButton) m_rectBottomEdgeInput.SetInput = true;
                                                   });

            m_rectLeftEdgeButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                                 data =>
                                                 {
                                                     if (GetMouseLeftButton) m_rectLeftEdgeInput.SetInput = true;
                                                 });

            m_rectCenterButton.AddTriggerEvent(EventTriggerType.PointerDown,
                                               data =>
                                               {
                                                   if (GetMouseLeftButton) m_rectCenterInput.SetInput = true;
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

            m_rectTopRightCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                       data => m_rectTopRightCornerInput.SetInput = false);

            m_rectTopLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                      data => m_rectTopLeftCornerInput.SetInput = false);

            m_rectBottomLeftCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                         data => m_rectBottomLeftCornerInput.SetInput = false);

            m_rectBottomRightCornerButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                          data => m_rectBottomRightCornerInput.SetInput = false);

            m_rectTopEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                data => m_rectTopEdgeInput.SetInput = false);

            m_rectRightEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                  data => m_rectRightEdgeInput.SetInput = false);

            m_rectBottomEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                   data => m_rectBottomEdgeInput.SetInput = false);

            m_rectLeftEdgeButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                                 data => m_rectLeftEdgeInput.SetInput = false);

            m_rectCenterButton.AddTriggerEvent(EventTriggerType.PointerUp,
                                               data => m_rectCenterInput.SetInput = false);
        }
    }
}