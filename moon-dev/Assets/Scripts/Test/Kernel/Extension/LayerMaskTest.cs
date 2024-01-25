using Moon.Kernel.Extension;
using NUnit.Framework;
using UnityEngine;

namespace Test.Kernel.Extension
{
    [Author("AstoraGray"), TestOf(nameof(LayerMaskExtension))]
    internal class LayerMaskTest
    {
        [TestCase(0)]
        public void ContainsLayerTest(int layer)
        {
            LayerMask mask = LayerMask.GetMask(LayerMask.LayerToName(layer));
            Assert.AreEqual(true, mask.ContainsLayer(0));
        }
    }
}