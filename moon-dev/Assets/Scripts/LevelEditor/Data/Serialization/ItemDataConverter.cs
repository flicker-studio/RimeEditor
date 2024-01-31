using System;
using System.Linq;
using Frame.Static.Global;
using LevelEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEngine;

/// <summary>
///     Custom deserialization transformations
/// </summary>
public class ItemDataConverter : CustomCreationConverter<ItemDataBase>
{
    private ItemDataType m_itemDataType;

    private ItemProduct m_itemProduct;
    //TODO:需加载SO
    // private static string ItemRootPath => GlobalSetting.CriticalPath.ITEM_FILE_PATH;

    /// <inheritdoc />
    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var token = JToken.ReadFrom(reader);
        m_itemDataType = token["ItemDataType"].ToObject<ItemDataType>();

        m_itemProduct = ItemProductAnalysis(token["ProductName"].ToString(),
            token["ProductType"].ToString());

        return base.ReadJson(token.CreateReader(), objectType, existingValue, serializer);
    }

    /// <inheritdoc />
    public override ItemDataBase Create(Type objectType)
    {
        switch (m_itemDataType)
        {
            case ItemDataType.Entrance:
                return new EntranceData(m_itemProduct, true);
            case ItemDataType.Platform:
                return new PlatformData(m_itemProduct, true);
            case ItemDataType.Exit:
                return new ExitData(m_itemProduct, true);
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