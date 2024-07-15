using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using UnityEngine;

namespace RimeEditor.Runtime
{
    /// <summary>
    ///     Custom deserialization transformations
    /// </summary>
    public class ItemDataConverter : JsonConverter
    {
        /// <inheritdoc />
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            // converter check,may not useless
            if (value is not ItemData src) throw new Exception();

            var json_object = new JObject
                              {
                                  { "ID", src.ID },
                                  { "Transform", JToken.FromObject(src.Transform) },
                                  { "User Data", src.UserData }
                              };
            writer.WriteValue(json_object.ToString());
        }

        /// <inheritdoc />
        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var token     = JToken.ReadFrom(reader);
            var id        = token["ID"].ToObject<string>();
            var transform = token["Transform"].ToObject<Transform>();
            var user_data = token["User Data"].ToObject<string>();
            return new ItemData(id, transform, user_data);
        }

        /// <inheritdoc />
        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(ItemData);
        }
    }
}