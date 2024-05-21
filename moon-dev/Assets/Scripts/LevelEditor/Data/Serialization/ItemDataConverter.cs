using System;
using LevelEditor.Data;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;

/// <summary>
///     Custom deserialization transformations
/// </summary>
public class ItemDataConverter : CustomCreationConverter<LevelEditor.Item>
{
    private ItemType m_itemDataType;

    private ItemProduct m_itemProduct;
    //TODO:需加载SO
    // private static string ItemRootPath => GlobalSetting.CriticalPath.ITEM_FILE_PATH;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.ReadFrom(reader);
        m_itemDataType = token["ItemDataType"].ToObject<ItemType>();

        m_itemProduct = ItemProductAnalysis(token["ProductName"].ToString(),
                                            token["ProductType"].ToString());

        return base.ReadJson(token.CreateReader(), objectType, existingValue, serializer);
    }

    /// <inheritdoc />
    public override LevelEditor.Item Create(Type objectType)
    {
        switch (m_itemDataType)
        {
            case ItemType.ENTRANCE:
            // return new Entrance(m_itemProduct, true);
            case ItemType.PLATFORM:
            // return new Platform(m_itemProduct, true);
            case ItemType.EXIT:
            //return new Exit(m_itemProduct, true);
            default:
                return null;
        }
    }

    private ItemProduct ItemProductAnalysis(string itemProductName, string itemProductType)
    {
        //TODO:需加载SO
        throw new Exception("需加载SO");
        // var itemProduct = Resources.LoadAll<ItemProduct>(ItemRootPath + '\\' + itemProductType).ToList()
        //     .First(value => value.Name == itemProductName ? value : false);

        // return itemProduct;
    }
}