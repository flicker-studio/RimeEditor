using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTypeButton : GridItemButton
{
    private TextMeshProUGUI m_text;

    public TextMeshProUGUI GetText
    {
        get
        {
            return m_text;
        }
    }

    public ItemTypeButton(GameObject buttonPrefab, Action<GridItemButton> onSelect, Transform parent,ScrollRect scrollRect,string textName) 
        : base(buttonPrefab, onSelect, parent,scrollRect)
    {
        m_text = m_buttonObj.transform.Find(textName).GetComponent<TextMeshProUGUI>();
    }
}
