using Moon.Kernel.Extension;
using NUnit.Framework;

namespace Test.Kernel.Extension
{
    [Author("AstoraGray"), TestOf(nameof(LayerMask))]
    internal class LayerMaskTest
    {
        [TestCase(0)]
        public void ContainsLayerTest(int layer)
        {
            UnityEngine.LayerMask mask = UnityEngine.LayerMask.GetMask(UnityEngine.LayerMask.LayerToName(layer));
            Assert.AreEqual(true, mask.ContainsLayer(0));
            Assert.AreEqual(false, mask.ContainsLayer(1));
            Assert.AreEqual(false, mask.ContainsLayer(2));
        }
    }
}