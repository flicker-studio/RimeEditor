using System.Collections.Generic;
using Data.ScriptableObject;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public enum LEVELEDITORACTIONTYPE
{
    UndoButton,
    RedoButton,
    ViewButton,
    PositionAxisButton,
    RotationAxisButton,
    ScaleButton,
    RectButton
}

public class LevelEditorAction
{
    public LEVELEDITORACTIONTYPE LevelEditorActionType = LEVELEDITORACTIONTYPE.PositionAxisButton;
}

public class LevelEditorCameraInformation : BaseInformation
{
    private Transform m_cameraTransform;

    private Camera m_camera;

    private LevelEditorProperty m_property;

    private LevelEditorInputController m_inputController;

    private RectTransform m_selectionUIRect;

    private Image m_selectionImage;

    private PrefabFactory m_prefabFactory;

    public List<GameObject> TargetList = new List<GameObject>();
    
    public RectTransform GetPositionAxisRectTransform => m_inputController.GetPositionAxisRectTransform;

    public RectTransform GetRotationAxisRectTransform => m_inputController.GetRotationAxisRectTransform;

    public LevelEditorInputController GetInput => m_inputController;

    public Transform GetCameraTransform => m_cameraTransform;

    public RectTransform GetSelectionUIRect => m_selectionUIRect;

    public Image GetSelectionImage => m_selectionImage;

    public Camera GetCamera => m_camera;
    
    private LevelEditorCommandExcute m_commandExcute;

    private LEVELEDITORACTIONTYPE m_levelEditorActionType;
    
    
    public Vector3 GetMousePosition => Mouse.current.position.ReadValue();
    
    public Vector3 GetMouseWorldPoint =>
        Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Mathf.Abs(GetCameraTransform.position.z)));

    private LevelEditorAction m_levelEditorAction = new LevelEditorAction();
    
    public LevelEditorAction GetLevelEditorAction => m_levelEditorAction;

    public GameObject GetEmptyGameObject => m_prefabFactory.EMPTY_GAMEOBJECT;

    public LevelEditorProperty.UIProperty GetUIProperty => m_property.GetUIProperty;

    public float GetRotationSpeed => m_property.GetRotationDragProperty.ROTATION_SPEED;

    public float GetCameraMaxZ => m_property.GetCameraMotionProperty.CAMERA_MAX_Z;
    
    public float GetCameraMinZ => m_property.GetCameraMotionProperty.CAMERA_MIN_Z;
    
    public float GetCameraZChangeSpeed => m_property.GetCameraMotionProperty.CAMERA_Z_CHANGE_SPEED;

    public OUTLINEMODE GetOutlineMode => m_property.GetOutlineProperty.OUTLINE_MODE;

    public Color GetOutlineColor => m_property.GetOutlineProperty.OUTLINE_COLOR;

    public float GetOutlineWidth => m_property.GetOutlineProperty.OUTLINE_WIDTH;

    public Vector2 GetSelectionMinSize => m_property.GetSelectionProperty.SELECTION_MIN_SIZE;

    public Color GetSelectionColor => m_property.GetSelectionProperty.SELECTION_COLOR;

    public LevelEditorCommandExcute GetLevelEditorCommandExcute => m_commandExcute;
    
    public LevelEditorCameraInformation(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        InitComponent(levelEditorTransform,levelEditorCommandExcute);
    }

    private void InitComponent(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        m_commandExcute = levelEditorCommandExcute;
        m_property = Resources.Load<LevelEditorProperty>("GlobalSettings/LevelEditorCameraProperty");
        m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
        m_cameraTransform = Camera.main.transform;
        m_camera = m_cameraTransform.GetComponent<Camera>();
        m_selectionUIRect = levelEditorTransform.Find(GetUIProperty.SELECTION_UI_NAME) as RectTransform;
        m_selectionImage = m_selectionUIRect.GetComponent<Image>();
        m_inputController = new LevelEditorInputController(levelEditorTransform, m_property);
    }
}
