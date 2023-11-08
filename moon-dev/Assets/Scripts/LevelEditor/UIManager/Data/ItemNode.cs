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
    
    public ITEMTYPE Itemtype { get; private set; }
    
    public Transform ItemNodeTransform { get; private set; }

    protected TextMeshProUGUI m_text;
    
    protected Button m_itemButton;

    private bool m_isSelected;

    private string m_itemName;
    public bool IsSelected
    {
        get
        {
            return m_isSelected;
        }
        set
        {
            m_isSelected = value;
            if (m_isSelected)
            {
                m_itemButton.interactable = false;
            }
            else
            {
                m_itemButton.interactable = true;
            }
        }
    }

    public void RemoveNode()
    {
        ObjectPool.Instance.OnRelease(ItemNodeTransform.gameObject);
        m_itemButton.onClick.RemoveAllListeners();
    }
    
    public ItemNode(ItemProduct itemProduct,Transform itemNodeContent,OnSelect onSelect)
    {
        Itemtype = itemProduct.ItemType;
        ItemNodeTransform = ObjectPool.Instance.OnTake(itemProduct.ItemNode).transform;
        ItemNodeTransform.SetParent(itemNodeContent);
        m_text = ItemNodeTransform.transform.Find("DescribeText").GetComponent<TextMeshProUGUI>();
        m_itemButton = ItemNodeTransform.GetComponent<Button>();
        m_itemButton.onClick.AddListener(() =>
        {
            onSelect?.Invoke(this);
        });
    }
}
