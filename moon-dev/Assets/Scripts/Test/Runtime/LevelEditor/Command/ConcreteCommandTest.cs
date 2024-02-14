using System.Collections;
using Cysharp.Threading.Tasks;
using LevelEditor;
using LevelEditor.Command;
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
                    var command = new ItemScaleCommand(m_gameObject, Vector3.down, Vector3.one * 3);
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