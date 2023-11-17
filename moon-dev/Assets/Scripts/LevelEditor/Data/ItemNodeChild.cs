using System;
using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemNodeChild : ItemNode
{
    public ItemData ItemData{ get; private set; }


    public ItemNodeChild(ItemProduct itemProduct, Transform itemNodeTransform, Action<ItemNode> onSelect,ItemData targetItem,ScrollRect scrollView) 
        : base(itemProduct, itemNodeTransform, onSelect,scrollView)
    {
        ItemData = targetItem;
        ItemName = itemProduct.Name;
        m_arrowButton.gameObject.SetActive(false);
    }
}
