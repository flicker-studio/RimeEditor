#region

using System.Collections;
using System.Diagnostics.CodeAnalysis;
using Cysharp.Threading.Tasks;
using LevelEditor;
using Moon.Kernel;
using Moon.Kernel.Setting;
using UnityEngine.TestTools;

#endregion

namespace Test.Runtime.LevelEditor
{
    [SuppressMessage("ReSharper", "MissingXmlDoc")]
    public class LevelTest
    {
        [UnityTest]
        public IEnumerator TestA()
        {
            return UniTask.ToCoroutine
            (
                async () =>
                {
                    await Explorer.BootCompletionTask;

                    var setting = Explorer.TryGetSetting<GlobalSetting>();
                    var level   = new Level(setting);
                    await level.Create();
                    level.Delete();
                }
            );
        }
    }
}