using Cysharp.Threading.Tasks;
using Data.ScriptableObject;
using Frame.StateMachine;
using LevelEditor.Command;
using Moon.Kernel.Service;
using Moon.Kernel.Utils;
using UnityEngine;

namespace LevelEditor
{
    public class Information : BaseInformation
    {
        public UIManager UIManager => m_uiManager;

        public LevelDataManager DataManager => m_dataManager;

        public PrefabManager PrefabManager => m_prefabManager;

        public CameraManager CameraManager => m_cameraManager;

        public OutlineManager OutlineManager => m_outlineManager;
        public InputManager InputManager => m_inputManager;

        public LevelAction LevelAction => m_levelAction;

        private UIManager m_uiManager;

        private LevelDataManager m_dataManager;

        private PrefabManager m_prefabManager;

        private CameraManager m_cameraManager;

        private InputManager m_inputManager;

        private OutlineManager m_outlineManager;

        private LevelAction m_levelAction;

        public async UniTask Init()
        {
            var levelEditorTransform = LevelEditorController.Instance.RootObject.transform as RectTransform;

            var prefab = await ResourcesService.LoadAssetAsync<PrefabFactory>("Assets/Settings/GlobalSettings/PrefabFactory.asset");
            var ui = await ResourcesService.LoadAssetAsync<UISetting>("Assets/Settings/GlobalSettings/LevelEditorUIProperty.asset");
            var cam = await ResourcesService.LoadAssetAsync<CameraSetting>("Assets/Settings/GlobalSettings/LevelEditorCameraProperty.asset");
            m_prefabManager = new PrefabManager(prefab);
            m_uiManager = new UIManager(levelEditorTransform, ui);
            m_inputManager = new InputManager();
            m_dataManager = new LevelDataManager();

            await m_dataManager.LoadLevelFiles();

            m_cameraManager = new CameraManager(cam);
            m_levelAction = new LevelAction();
            var mask = await ResourcesService.LoadAssetAsync<Material>("Assets/Materials/OutlineMask.mat");
            var fill = await ResourcesService.LoadAssetAsync<Material>("Assets/Materials/OutlineFill.mat");
            m_outlineManager = new OutlineManager(mask, fill, cam);
            DataManager.SyncLevelData += ResetCommand;
            DataManager.SyncLevelData += ResetOutline;
            DataManager.SyncLevelData += ResetCameraPos;
        }

        internal void EnableExcute()
        {
            DataManager.SetActiveEditors(true);
            OutlineManager.SetRenderObjects(DataManager.TargetObjs);
        }

        private void ResetCommand(SubLevelData subLevelData)
        {
            CommandInvoker.Clear();
        }

        private void ResetOutline(SubLevelData subLevelData)
        {
            OutlineManager.SetRenderObjects(DataManager.TargetObjs);
        }

        private void ResetCameraPos(SubLevelData subLevelData)
        {
            var itemObjs = subLevelData.ItemAssets.GetItemObjs();

            if (itemObjs.Count == 0)
            {
                return;
            }

            var targetPos = Vector3.zero;

            foreach (var itemObj in itemObjs) targetPos += itemObj.transform.position;

            targetPos /= itemObjs.Count;

            var oriPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2.0f, Screen.height / 2.0f,
                Mathf.Abs(Camera.main.transform.position.z)));

            var direction = targetPos - oriPos;

            var zLength = (CameraManager.CameraZMax +
                           CameraManager.CameraZMin) / 2;

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + direction.x
                , Camera.main.transform.position.y + direction.y
                , zLength);
        }
    }
}