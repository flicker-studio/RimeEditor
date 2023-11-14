using Frame.StateMachine;
using UnityEngine;

namespace LevelEditor
{
    public class Information : BaseInformation
    {
        public UIManager GetUI => m_uiManager;

        public DataManager GetData => m_dataManager;

        public PrefabManager GetPrefab => m_prefabManager;

        public CameraManager GetCamera => m_cameraManager;

        public InputController GetInput => m_inputController;

        public CommandSet GetCommandSet => m_commandSet;
        

        private InputController m_inputController;

        private UIManager m_uiManager;

        private DataManager m_dataManager;

        private PrefabManager m_prefabManager;

        private CameraManager m_cameraManager;
    
        private CommandSet m_commandSet;
    
        public Information(RectTransform levelEditorTransform,CommandSet commandSet)
        {
            InitComponent(levelEditorTransform,commandSet);
        }

        private void InitComponent(RectTransform levelEditorTransform,CommandSet commandSet)
        {
            m_commandSet = commandSet;
            m_prefabManager = new PrefabManager();
            m_uiManager = new UIManager(levelEditorTransform);
            m_inputController = new InputController();
            m_dataManager = new DataManager();
            m_cameraManager = new CameraManager();
        }
    }
}
