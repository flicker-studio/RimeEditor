using System.Collections;
using System.Collections.Generic;
using Data.ScriptableObject;
using Frame.StateMachine;
using Frame.Tool;
using UnityEngine;
using UnityEngine.UI;

public class LevelEditorCameraInformation : BaseInformation
{
    private Transform m_cameraTransform;

    private Camera m_camera;

    private LevelEditorProperty m_property;

    private RectTransform m_selectionUIRect;

    private Image m_selectionImage;

    private PrefabFactory m_prefabFactory;

    public List<GameObject> TargetList = new List<GameObject>();

    public Transform GetCameraTransform => m_cameraTransform;

    public RectTransform GetSelectionUIRect => m_selectionUIRect;

    public Image GetSelectionImage => m_selectionImage;

    public Camera GetCamera => m_camera;

    public GameObject GetEmptyGameObject => m_prefabFactory.EMPTY_GAMEOBJECT;

    public float GetCameraMaxZ => m_property.GetCameraMotionProperty.CAMERA_MAX_Z;
    
    public float GetCameraMinZ => m_property.GetCameraMotionProperty.CAMERA_MIN_Z;
    
    public float GetCameraZChangeSpeed => m_property.GetCameraMotionProperty.CAMERA_Z_CHANGE_SPEED;

    public OUTLINEMODE GetOutlineMode => m_property.GetOutlineProperty.OUTLINE_MODE;

    public Color GetOutlineColor => m_property.GetOutlineProperty.OUTLINE_COLOR;

    public float GetOutlineWidth => m_property.GetOutlineProperty.OUTLINE_WIDTH;

    public Vector2 GetSelectionMinSize => m_property.GetSelectionProperty.SELECTION_MIN_SIZE;

    public Color GetSelectionColor => m_property.GetSelectionProperty.SELECTION_COLOR;
    
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


    public LevelEditorCameraInformation(RectTransform selectionUIRect)
    {
        m_cameraTransform = Camera.main.transform;
        m_camera = m_cameraTransform.GetComponent<Camera>();
        m_selectionUIRect = selectionUIRect;
        m_selectionImage = selectionUIRect.GetComponent<Image>();
        m_property = Resources.Load<LevelEditorProperty>("GlobalSettings/LevelEditorCameraProperty");
        m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
    }
}
