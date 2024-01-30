using System;
using System.Linq;
using Frame.Static.Global;
using LevelEditor;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;
using Newtonsoft.Json.Linq;
using UnityEngine;

public class ItemDataConverter : CustomCreationConverter<ItemData>
{
    private ItemDataType m_itemDataType;

    private ItemProduct m_itemProduct;

    private string m_itemRootPath => GlobalSetting.CriticalPath.ITEM_FILE_PATH;

    public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
    {
        var jobj = JObject.ReadFrom(reader);
        m_itemDataType = jobj["ItemDataType"].ToObject<ItemDataType>();

        m_itemProduct = ItemProductAnalysis(jobj["ProductName"].ToString(),
            jobj["ProductType"].ToString());

        return base.ReadJson(jobj.CreateReader(), objectType, existingValue, serializer);
    }

    public override ItemData Create(Type objectType)
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
        ItemProduct itemProduct = Resources.LoadAll<ItemProduct>
                (m_itemRootPath + '\\' + itemProductType)
            .ToList().First((value) =>
            {
                if (value.Name == itemProductName)
                {
                    return value;
                }

                return false;
            });

        return itemProduct;
    }
}