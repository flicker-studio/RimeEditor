using Newtonsoft.Json;
using NUnit.Framework;
using RimeEditor.Runtime;
using UnityEngine;
using Random = System.Random;

namespace RimeEditor.Tests.Runtime
{
    public class SerializationTest
    {
        private ItemData _itemData;

        [SetUp]
        public void SetUp()
        {
            var str         = string.Empty;
            var game_object = new GameObject();
            var random      = new Random();

            for (var i = 0; i < 10; i++)
            {
                var num = random.Next();
                str += ((char)(0x30 + (ushort)(num % 10))).ToString();
            }

            var tuple = (str, game_object.transform);
            _itemData = new ItemData(tuple.str, tuple.transform, null);
        }

        // A Test behaves as an ordinary method
        [Test]
        public void ItemDataSerializePasses()
        {
            var json     = JsonConvert.SerializeObject(_itemData);
            var new_data = JsonConvert.DeserializeObject<ItemData>(json);
            Assert.AreEqual(_itemData.ID, new_data.ID);
        }
    }
}