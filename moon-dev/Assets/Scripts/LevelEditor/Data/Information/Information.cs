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
        public UIManager        UIManager      => _uiManager;
        public LevelDataManager DataManager    => _dataManager;
        public PrefabManager    PrefabManager  => _prefabManager;
        public CameraManager    CameraManager  => _cameraManager;
        public OutlineManager   OutlineManager => _outlineManager;
        public InputManager     InputManager   => _inputManager;
        public LevelAction      LevelAction    => _levelAction;

        private UIManager        _uiManager;
        private LevelDataManager _dataManager;
        private PrefabManager    _prefabManager;
        private CameraManager    _cameraManager;
        private InputManager     _inputManager;
        private OutlineManager   _outlineManager;
        private LevelAction      _levelAction;

        public UISetting UI;

        public async UniTask Init()
        {
            var levelEditorTransform = Controller.RootObject.transform as RectTransform;

            var prefab = await ResourcesService.LoadAssetAsync<PrefabFactory>("Assets/Settings/GlobalSettings/PrefabFactory.asset");
            UI = await ResourcesService.LoadAssetAsync<UISetting>("Assets/Settings/GlobalSettings/LevelEditorUIProperty.asset");
            var cam = await ResourcesService.LoadAssetAsync<CameraSetting>("Assets/Settings/GlobalSettings/LevelEditorCameraProperty.asset");

            _prefabManager = new PrefabManager(prefab);
            _uiManager     = new UIManager(levelEditorTransform, UI);
            _inputManager  = new InputManager();
            _dataManager   = new LevelDataManager();

            await _dataManager.LoadLevelFiles();

            _cameraManager = new CameraManager(cam);
            _levelAction   = new LevelAction();
            var mask = await ResourcesService.LoadAssetAsync<Material>("Assets/Materials/OutlineMask.mat");
            var fill = await ResourcesService.LoadAssetAsync<Material>("Assets/Materials/OutlineFill.mat");
            _outlineManager           =  new OutlineManager(mask, fill, cam);
            DataManager.SyncLevelData += ResetCommand;
            DataManager.SyncLevelData += ResetOutline;
            DataManager.SyncLevelData += ResetCameraPos;
        }

        // internal void EnableExcute()
        // {
        //     // DataManager.SetActiveEditors(true);
        //     //  OutlineManager.SetRenderObjects(DataManager.TargetObjs);
        // }

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