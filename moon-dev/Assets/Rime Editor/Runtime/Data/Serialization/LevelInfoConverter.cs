using System;
using System.Security.Cryptography;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace LevelEditor.Data.Serialization
{
    public class LevelInfoConverter : JsonConverter
    {
        /// <inheritdoc />
        public override bool CanRead => true;

        /// <inheritdoc />
        public override bool CanWrite => true;

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return typeof(LevelInfo) == objectType;
        }

        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            var obj = new JObject();

            if (value is not LevelInfo info) throw new Exception("Serialization failed.");

            var sha256 = new SHA256Managed();
            obj.Add("Name",         info.Name);
            obj.Add("Author",       info.Author);
            obj.Add("Introduction", info.Introduction);
            var bytes = sha256.ComputeHash(info.Cover.EncodeToPNG());
            obj.Add("Cover", BitConverter.ToString(bytes));
            serializer.Serialize(writer, obj);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj          = serializer.Deserialize<JObject>(reader);
            var name         = obj.Value<string>("Name");
            var author       = obj.Value<string>("Author");
            var introduction = obj.Value<string>("Introduction");
            var cover        = obj.Value<string>("Cover");
            return new LevelInfo(name, author, introduction);
        }
    }
}