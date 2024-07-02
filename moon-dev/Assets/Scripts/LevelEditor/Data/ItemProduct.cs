using LevelEditor.Data;
using Newtonsoft.Json;
using UnityEngine;

[CreateAssetMenu(menuName = "Create/ItemProduct", fileName = "NewItem", order = 1), JsonObject(MemberSerialization.OptIn)]
public class ItemProduct : ScriptableObject
{
    [field: SerializeField, JsonProperty("Name", Order = 1)]
    public string Name { get; private set; }

    [field: SerializeField, JsonIgnore] public Sprite ItemIcon { get; set; }

    [field: SerializeField, JsonProperty("Type", Order = 2)]
    public ItemType ItemType { get; private set; }

    [field: SerializeField, JsonIgnore] public GameObject ItemObject { get; private set; }

    [JsonIgnore] public GameObject ItemNode { get; private set; }

    private void OnEnable()
    {
        ItemNode = Resources.Load<GameObject>("Prefabs/ItemNode");
    }

    public ItemProduct(string name, Sprite itemIcon, ItemType itemType, GameObject itemObject)
    {
        ItemIcon = itemIcon;
        ItemObject = itemObject;
        ItemType = itemType;
        Name = name;
    }
}