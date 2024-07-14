using Cysharp.Threading.Tasks;
using LevelEditor.Command;
using LevelEditor.Item;
using LevelEditor.Manager;
using UnityEngine;

namespace LevelEditor
{
    // TODO:Split into multiple profiles
    public class Information
    {
        public UISetting UI;

        public UIManager UIManager { get; private set; }

        public LevelDataManager DataManager { get; private set; }

        public PrefabManager PrefabManager { get; private set; }

        public CameraManager CameraManager { get; private set; }

        public OutlineManager OutlineManager { get; private set; }

        public InputManager InputManager { get; private set; }

        public LevelAction LevelAction { get; private set; }

        public async UniTask Init(LevelEditorSetting setting)
        {
            var levelEditorTransform = RimeEditor.Runtime.RimeEditor.Instance.transform as RectTransform;

            var prefab = setting.PrefabSetting;
            UI = setting.UISetting;
            var cam = setting.CameraSetting;
            PrefabManager = new PrefabManager(prefab);
            UIManager     = new UIManager(levelEditorTransform, UI);
            InputManager  = new InputManager();
            DataManager   = new LevelDataManager();

            await DataManager.LoadLevelFiles();

            CameraManager = new CameraManager(cam);
            LevelAction   = new LevelAction();
            var mask = setting.OutlineSetting.OutlineMask;
            var fill = setting.OutlineSetting.OutlineMask;
            OutlineManager            =  new OutlineManager(mask, fill, cam);
            DataManager.SyncLevelData += ResetCommand;
            DataManager.SyncLevelData += ResetOutline;
            DataManager.SyncLevelData += ResetCameraPos;
        }

        // internal void EnableExcute()
        // {
        //     // DataManager.SetActiveEditors(true);
        //     //  OutlineManager.SetRenderObjects(DataManager.TargetObjs);
        // }

        private void ResetCommand(SubLevel subLevel)
        {
            CommandInvoker.Clear();
        }

        private void ResetOutline(SubLevel subLevel)
        {
            OutlineManager.SetRenderObjects(DataManager.TargetObjs);
        }

        private void ResetCameraPos(SubLevel subLevel)
        {
            var itemObjs = subLevel.ItemAssets.GetItemObjs();

            if (itemObjs.Count == 0) return;

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