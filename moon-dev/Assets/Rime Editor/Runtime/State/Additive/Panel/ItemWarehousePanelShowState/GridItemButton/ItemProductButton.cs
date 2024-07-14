using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemProductButton : ListEntry
{
    private readonly Image m_image;

    private readonly ItemProduct     m_itemProduct;
    private readonly TextMeshProUGUI m_text;

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

    public TextMeshProUGUI GetText => m_text;

    public Image GetImage => m_image;

    public ItemProduct GetItemProduct => m_itemProduct;
}