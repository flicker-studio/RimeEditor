using UnityEngine;

public enum ITEMTYPEENUM
{
    Platform,
    Mechanism
}
[CreateAssetMenu(menuName = "Create/ItemProduct",fileName = "NewItem",order = 1)]
public class ItemProduct : ScriptableObject
{
    [field:SerializeField]
    public string Name { get; private set; }
    [field:SerializeField]
    public Sprite ItemIcon { get; set; }
    [field:SerializeField]
    public ITEMTYPEENUM ItemType { get; private set; }
    [field:SerializeField]
    public GameObject ItemObject { get; private set; }
    public GameObject ItemNode { get; private set; }

    private void OnEnable()
    {
        ItemNode = Resources.Load<GameObject>("Prefabs/ItemNode");
    }

    public ItemProduct(string name,Sprite itemIcon,ITEMTYPEENUM itemType,GameObject itemObject)
    {
        ItemIcon = itemIcon;
        ItemObject = itemObject;
        ItemType = itemType;
        Name = name;
    }
}
