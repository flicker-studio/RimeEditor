using System.Collections;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using LevelEditor;
using LevelEditor.Command;
using LevelEditor.Item;
using Moon.Kernel;
using Moon.Kernel.Service;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;
using SCM = Moon.Kernel.Service.ServiceControlManager;

namespace Test.Runtime.LevelEditor.Command
{
    public class ConcreteCommandTest
    {
        private GameObject m_gameObject;

        [UnitySetUp]
        internal IEnumerator Setup()
        {
            return UniTask.ToCoroutine
                (
                 async () =>
                 {
                     await Explorer.BootCompletionTask;

                     var sceneService = SCM.TryGetService<SceneService>();
                     SceneManager.MergeScenes(sceneService.ActiveScene, sceneService.PersistenceScene);
                     var sce = await sceneService.TryLoadActiveScene("Test Scene 1");
                     m_gameObject                    = GameObject.CreatePrimitive(PrimitiveType.Cube);
                     m_gameObject.transform.position = Vector3.zero;
                 }
                );
        }

        [UnityTest]
        internal IEnumerator ItemScaleCommandPass()
        {
            return UniTask.ToCoroutine
                (
                 async () =>
                 {
                     var itemList  = new List<ItemBase>(1);
                     var scaleList = new List<Vector3>(1);
                     itemList.Add(new Platform());
                     scaleList.Add(Vector3.down);
                     var command = new Scale(itemList, scaleList);
                     CommandInvoker.Execute(command);
                     await UniTask.Yield();
                     CommandInvoker.Undo();
                     await UniTask.Yield();
                     CommandInvoker.Redo();
                     await UniTask.Yield();
                 }
                );
        }
    }
}