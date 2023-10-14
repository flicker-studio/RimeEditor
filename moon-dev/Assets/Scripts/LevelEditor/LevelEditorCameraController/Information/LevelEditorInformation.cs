using System.Collections.Generic;
using Data.ScriptableObject;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;

public class LevelEditorInformation : BaseInformation
{
    public List<GameObject> TargetList = new List<GameObject>();

    public LevelEditorUIManager GetUI => m_uiManager;

    public LevelEditorInputController GetInput => m_inputController;

    public GameObject GetEmptyGameObject => m_prefabFactory.EMPTY_GAMEOBJECT;

    public LevelEditorCameraProperty GetCameraProperty => m_cameraProperty;

    public LevelEditorCommandExcute GetLevelEditorCommandExcute => m_commandExcute;
    
    public Vector3 GetMousePosition => Mouse.current.position.ReadValue();
    
    public Vector3 GetMouseWorldPoint =>
        Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Mathf.Abs(Camera.main.transform.position.z)));

    private LevelEditorCameraProperty m_cameraProperty;

    private LevelEditorUIProperty m_uiProperty;

    private LevelEditorInputController m_inputController;

    private LevelEditorUIManager m_uiManager;

    private PrefabFactory m_prefabFactory;
    
    private LevelEditorCommandExcute m_commandExcute;
    
    public LevelEditorInformation(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        InitComponent(levelEditorTransform,levelEditorCommandExcute);
    }

    private void InitComponent(RectTransform levelEditorTransform,LevelEditorCommandExcute levelEditorCommandExcute)
    {
        m_commandExcute = levelEditorCommandExcute;
        m_cameraProperty = Resources.Load<LevelEditorCameraProperty>("GlobalSettings/LevelEditorCameraProperty");
        m_uiProperty = Resources.Load<LevelEditorUIProperty>("GlobalSettings/LevelEditorUIProperty");
        m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
        m_uiManager = new LevelEditorUIManager(levelEditorTransform,m_uiProperty);
        m_inputController = new LevelEditorInputController();
    }
}
