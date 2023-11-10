using LevelEditor;
using UnityEngine;

public class ItemNodeChild : ItemNode
{
    public ItemData ItemData{ get; private set; }


    public ItemNodeChild(ItemProduct itemProduct, Transform itemNodeTransform, OnSelect onSelect,ItemData targetItem) 
        : base(itemProduct, itemNodeTransform, onSelect)
    {
        ItemData = targetItem;
        ItemName = itemProduct.Name;
    }
}
