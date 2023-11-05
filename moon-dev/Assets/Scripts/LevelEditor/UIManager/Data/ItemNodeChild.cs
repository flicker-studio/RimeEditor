using LevelEditor;
using UnityEngine;

public class ItemNodeChild : ItemNode
{
    public Transform GetItemNodeTransform { get; private set; }
    
    public GameObject GetTargetObj{ get; private set; }


    public ItemNodeChild(string itemName, Transform itemNodeTransform,GameObject targetObj, UIProperty.ItemNodeProperty itemNodeProperty, OnSelect onSelect) : base(itemName, itemNodeTransform, itemNodeProperty, onSelect)
    {
        GetItemNodeTransform = itemNodeTransform;
        GetTargetObj = targetObj;
        m_text.text = "  " + itemName;
    }
}
