using LevelEditor;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public abstract class ItemNode
{
    public string ItemName;
    
    private Image m_image;

    protected TextMeshProUGUI m_text;
    
    protected Button m_itemButton;

    private UIProperty.ItemNodeProperty m_itemNodeProperty;

    private bool m_isSelected;
    public bool IsSelected
    {
        get
        {
            return m_isSelected;
        }
        set
        {
            if (value)
            {
                m_image.color = m_itemNodeProperty.SELECTED_COLOR;
            }
            else
            {
                m_image.color = Color.clear;
            }

            m_isSelected = value;
        }
    }
    
    public ItemNode(string itemName,Transform itemNodeTransform,UIProperty.ItemNodeProperty itemNodeProperty,OnSelect onSelect)
    {
        ItemName = itemName;
        m_text = itemNodeTransform.transform.Find("DescribeText").GetComponent<TextMeshProUGUI>();
        m_text.text = ItemName;
        m_image = itemNodeTransform.GetComponent<Image>();
        m_itemButton = itemNodeTransform.GetComponent<Button>();
        m_itemNodeProperty = itemNodeProperty;
        m_itemButton.AddTriggerEvent(EventTriggerType.PointerEnter,
            data =>
            {
                if(IsSelected) return;
                m_image.color = m_itemNodeProperty.HIGH_LIGHTED_COLOR;
            });
        m_itemButton.AddTriggerEvent(EventTriggerType.PointerExit,
            data =>
            {
                if(IsSelected) return;
                m_image.color = Color.clear;
            });
        m_itemButton.onClick.AddListener(() =>
        {
            onSelect?.Invoke(this);
        });
    }
}
