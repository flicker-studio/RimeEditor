using System;
using Frame.Data;
using Frame.Tool.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public abstract class ItemNode
{
    public string ItemName
    {
        get
        {
            return m_itemName;
        }
        set
        {
            m_itemName = value;
            if (this as ItemNodeParent != null)
            {
                m_text.text = m_itemName;
            }
            else
            {
                m_text.text = "  " + m_itemName;
            }
        }
    }
    
    public bool IsSelected
    {
        set
        {
            m_eventButton.IsSelected = value;
        }

        get
        {
            return m_eventButton.IsSelected;
        }
    }
    
    public ITEMTYPEENUM Itemtypeenum { get; private set; }
    
    public Transform ItemNodeTransform { get; private set; }

    protected TextMeshProUGUI m_text;

    protected EventButton<ItemNode> m_eventButton;

    private string m_itemName;

    public ItemNode(ItemProduct itemProduct,Transform itemNodeContent,Action<ItemNode> onSelect,ScrollRect scrollView)
    {
        Itemtypeenum = itemProduct.ItemType;
        ItemNodeTransform = ObjectPool.Instance.OnTake(itemProduct.ItemNode).transform;
        ItemNodeTransform.SetParent(itemNodeContent);
        m_text = ItemNodeTransform.transform.Find("DescribeText").GetComponent<TextMeshProUGUI>();
        m_eventButton = new EventButton<ItemNode>(this, ItemNodeTransform,onSelect,scrollView);
    }
    
    public void RemoveNode()
    {
        ObjectPool.Instance.OnRelease(ItemNodeTransform.gameObject);
        m_eventButton.RemoveEvents();
    }
}
