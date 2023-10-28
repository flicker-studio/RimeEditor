using System.Collections.Generic;
using Data.ScriptableObject;
using Frame.StateMachine;
using Frame.Static.Extensions;
using UnityEngine;
using UnityEngine.InputSystem;

namespace LevelEditor
{
    public class Information : BaseInformation
    {
        public List<GameObject> TargetList = new List<GameObject>();

        public UIManager GetUI => m_uiManager;

        public InputController GetInput => m_inputController;

        public GameObject GetEmptyGameObject => m_prefabFactory.EMPTY_GAMEOBJECT;

        public CameraProperty GetCameraProperty => m_cameraProperty;

        public CommandExcute GetLevelEditorCommandExcute => m_commandExcute;
    
        public Vector3 GetMousePosition => Mouse.current.position.ReadValue();
    
        public Vector3 GetMouseWorldPoint =>
            Camera.main.ScreenToWorldPoint(GetMousePosition.NewZ(Mathf.Abs(Camera.main.transform.position.z)));

        private CameraProperty m_cameraProperty;

        private UIProperty m_uiProperty;

        private InputController m_inputController;

        private UIManager m_uiManager;

        private PrefabFactory m_prefabFactory;
    
        private CommandExcute m_commandExcute;
    
        public Information(RectTransform levelEditorTransform,CommandExcute levelEditorCommandExcute)
        {
            InitComponent(levelEditorTransform,levelEditorCommandExcute);
        }

        private void InitComponent(RectTransform levelEditorTransform,CommandExcute levelEditorCommandExcute)
        {
            m_commandExcute = levelEditorCommandExcute;
            m_cameraProperty = Resources.Load<CameraProperty>("GlobalSettings/LevelEditorCameraProperty");
            m_uiProperty = Resources.Load<UIProperty>("GlobalSettings/LevelEditorUIProperty");
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_uiManager = new UIManager(levelEditorTransform,m_uiProperty);
            m_inputController = new InputController();
        }
    }
}
