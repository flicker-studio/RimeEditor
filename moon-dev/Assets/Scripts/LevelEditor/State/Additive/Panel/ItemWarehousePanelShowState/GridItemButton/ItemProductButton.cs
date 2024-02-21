using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemProductButton : ListEntry
{
    private TextMeshProUGUI m_text;

    private Image m_image;

    private ItemProduct m_itemProduct;

    public TextMeshProUGUI GetText
    {
        get { return m_text; }
    }

    public Image GetImage
    {
        get { return m_image; }
    }

    public ItemProduct GetItemProduct
    {
        get { return m_itemProduct; }
    }

    public ItemProductButton(GameObject buttonPrefab, ItemProduct itemProduct, Action<ListEntry> onSelect
                           , Transform  parent,       ScrollRect  scrollRect,  string            textName, string imageName)
        : base(buttonPrefab, null, parent, scrollRect)
    {
        m_text         = ButtonObj.transform.Find(textName).GetComponent<TextMeshProUGUI>();
        m_image        = ButtonObj.transform.Find(imageName).GetComponent<Image>();
        m_itemProduct  = itemProduct;
        m_image.sprite = m_itemProduct.ItemIcon;
        m_text.text    = m_itemProduct.Name;
    }
}