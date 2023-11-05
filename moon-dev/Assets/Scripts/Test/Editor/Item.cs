using System.Collections;
using NUnit.Framework;
using UnityEngine;
using UnityEngine.TestTools;

namespace Test.Editor
{
    /// <summary>
    ///     An easy item test
    /// </summary>
    public class Item
    {
        /// <summary>
        ///     A Test behaves as an ordinary method
        /// </summary>
        [Test]
        public void SimplePasses()
        {
            // var itemData = new ItemData(new GameObject(), true, true);
            // Debug.Log(JsonUtility.ToJson(itemData, true));
            // // Use the Assert class to test conditions
            // Assert.IsNotNull(itemData);
        }

        /// <summary>
        ///     A UnityTest behaves like a coroutine in Play Mode.
        ///     In Edit Mode you can use <c>yield return null;</c> to skip a frame.
        /// </summary>
        [UnityTest]
        public IEnumerator EnumeratorPasses()
        {
            // Use the Assert class to test conditions.
            // Use yield to skip a frame.
            yield return null;
        }
    }
}