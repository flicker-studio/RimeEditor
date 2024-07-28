using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace LevelEditor.Data.Serialization
{
    /// <summary>
    ///     Convert LevelInfo to and from Json strings
    /// </summary>
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

            obj.Add("Name",         info.Name);
            obj.Add("Author",       info.Author);
            obj.Add("Introduction", info.Introduction);
            obj.Add("ID",           info.ID);

            serializer.Serialize(writer, obj);
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var obj          = serializer.Deserialize<JObject>(reader);
            var name         = obj.Value<string>("Name");
            var author       = obj.Value<string>("Author");
            var introduction = obj.Value<string>("Introduction");
            var id           = obj.Value<string>("ID");

            return new LevelInfo(name, author, introduction, id);
        }
    }
}