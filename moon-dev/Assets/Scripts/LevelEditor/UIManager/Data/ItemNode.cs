using Frame.Tool.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
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
    
    protected Button m_nodeButton;

    private Image m_nodeImage;

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
                m_nodeImage.color = m_nodeButton.colors.selectedColor;
            }
            else
            {
                m_nodeImage.color = m_nodeButton.colors.normalColor;
            }
        }
    }

    public ItemNode(ItemProduct itemProduct,Transform itemNodeContent,OnSelect onSelect)
    {
        Itemtype = itemProduct.ItemType;
        ItemNodeTransform = ObjectPool.Instance.OnTake(itemProduct.ItemNode).transform;
        ItemNodeTransform.SetParent(itemNodeContent);
        m_text = ItemNodeTransform.transform.Find("DescribeText").GetComponent<TextMeshProUGUI>();
        m_nodeButton = ItemNodeTransform.GetComponent<Button>();
        m_nodeImage = ItemNodeTransform.GetComponent<Image>();
        InitEvents(onSelect);
    }
    
    public void RemoveNode()
    {
        ObjectPool.Instance.OnRelease(ItemNodeTransform.gameObject);
        m_nodeButton.RemoveAllTriggerEvents();
    }

    private void InitEvents(OnSelect onSelect)
    {
        m_nodeButton.RemoveAllTriggerEvents();
        m_nodeButton.AddTriggerEvent(EventTriggerType.PointerClick, context =>
        {
            onSelect?.Invoke(this);
        });
        m_nodeButton.AddTriggerEvent(EventTriggerType.PointerEnter, context =>
        {
            if(m_isSelected) return;
            m_nodeImage.color = m_nodeButton.colors.highlightedColor;
        });
        m_nodeButton.AddTriggerEvent(EventTriggerType.PointerExit, context =>
        {
            if(m_isSelected) return;
            m_nodeImage.color = m_nodeButton.colors.normalColor;
        });
    }
}
