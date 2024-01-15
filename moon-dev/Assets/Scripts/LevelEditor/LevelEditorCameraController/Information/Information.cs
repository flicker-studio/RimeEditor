using System.Collections.Generic;
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
            InitEvent();
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

        private void InitEvent()
        {
            m_dataManager.SyncLevelData += ResetCommand;
            m_dataManager.SyncLevelData += ResetOutline;
            m_dataManager.SyncLevelData += ResetCameraPos;
            m_commandSet.EnableExcute += EnableExcute;
        }

        private void EnableExcute()
        {
            m_dataManager.SetActiveEditors(true);
            m_cameraManager.GetOutlinePainter.SetTargetObj = m_dataManager.TargetObjs;
        }

        private void ResetCommand(SubLevelData subLevelData)
        {
            m_commandSet.Clear?.Invoke();
        }
        
        private void ResetOutline(SubLevelData subLevelData)
        {
            GetCamera.GetOutlinePainter.SetTargetObj = GetData.TargetObjs;
        }

        private void ResetCameraPos(SubLevelData subLevelData)
        {
            List<GameObject> itemObjs = subLevelData.ItemAssets.GetItemObjs();
            if (itemObjs.Count == 0)
            {
                return;
            }
            Vector3 targetPos = Vector3.zero;
            foreach (var itemObj in itemObjs)
            {
                targetPos += itemObj.transform.position;
            }

            targetPos /= itemObjs.Count;

            Vector3 oriPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));

            Vector3 direction = targetPos - oriPos;
            
            float zLength = (GetCamera.GetProperty.GetCameraMotionProperty.CAMERA_MAX_Z +
                             GetCamera.GetProperty.GetCameraMotionProperty.CAMERA_MIN_Z) / 2;

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + direction.x
                ,Camera.main.transform.position.y + direction.y
                    ,zLength);
        }
    }
}
