using System.Collections;
using Moon.Kernel;
using Moon.Kernel.Service;
using NUnit.Framework;
using UnityEngine.TestTools;

namespace Test.Kernel
{
    [Author("Mors")]
    [TestOf(nameof(Explorer))]
    internal class ExplorerTest
    {
        [UnityTest]
        public IEnumerator TryGetServicePasses()
        {
            Assert.IsInstanceOf<SceneService>(Explorer.TryGetService<SceneService>());
            yield return null;
        }
    }
}