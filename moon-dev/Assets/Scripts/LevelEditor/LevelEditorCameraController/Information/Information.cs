using System.Threading.Tasks;
using Cysharp.Threading.Tasks;
using Data.ScriptableObject;
using Frame.StateMachine;
using Moon.Kernel.Service;
using UnityEngine;

namespace LevelEditor
{
    public class Information : BaseInformation
    {
        public UIManager GetUI { get; private set; }

        public DataManager GetData { get; private set; }

        public PrefabManager GetPrefab { get; private set; }

        public CameraManager GetCamera { get; private set; }

        public InputManager GetInput { get; private set; }

        public CommandSet GetCommandSet { get; private set; }

        public LevelAction GetLevelAction { get; private set; }


        public async UniTask Init(RectTransform levelEditorTransform, CommandSet commandSet)
        {
            await InitComponent(levelEditorTransform, commandSet);
            InitEvent();
        }

        private async UniTask InitComponent(RectTransform levelEditorTransform, CommandSet commandSet)
        {
            GetCommandSet = commandSet;
            var prefab = await ResourcesService.LoadAssetAsync<PrefabFactory>("Assets/Settings/GlobalSettings/PrefabFactory.asset");
            var ui = await ResourcesService.LoadAssetAsync<UIProperty>("Assets/Settings/GlobalSettings/LevelEditorUIProperty.asset");
            var cam = await ResourcesService.LoadAssetAsync<CameraProperty>("Assets/Settings/GlobalSettings/LevelEditorCameraProperty.asset");
            GetPrefab = new PrefabManager(prefab);
            GetUI = new UIManager(levelEditorTransform, ui);
            GetInput = new InputManager();
            GetData = new DataManager();
            GetCamera = new CameraManager(cam);
            GetLevelAction = new LevelAction();
        }

        private void InitEvent()
        {
            GetData.SyncLevelData += ResetCommand;
            GetData.SyncLevelData += ResetOutline;
            GetData.SyncLevelData += ResetCameraPos;
            GetCommandSet.EnableExcute += EnableExcute;
        }

        private void EnableExcute()
        {
            GetData.SetActiveEditors(true);
            GetCamera.SetTargetObject = GetData.TargetObjs;
        }

        private void ResetCommand(SubLevelData subLevelData)
        {
            GetCommandSet.Clear?.Invoke();
        }

        private void ResetOutline(SubLevelData subLevelData)
        {
            GetCamera.SetTargetObject = GetData.TargetObjs;
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

            var oriPos = Camera.main.ScreenToWorldPoint(new Vector3(Screen.width / 2, Screen.height / 2,
                Mathf.Abs(Camera.main.transform.position.z)));

            var direction = targetPos - oriPos;

            var zLength = (GetCamera.CameraZMax +
                           GetCamera.CameraZMin) / 2;

            Camera.main.transform.position = new Vector3(Camera.main.transform.position.x + direction.x
                , Camera.main.transform.position.y + direction.y
                , zLength);
        }
    }
}