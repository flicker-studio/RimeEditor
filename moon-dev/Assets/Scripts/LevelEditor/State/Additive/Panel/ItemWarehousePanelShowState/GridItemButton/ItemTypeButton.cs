using System;
using Frame.CompnentExtensions;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemTypeButton : ListEntry
{
    public TextMeshProUGUI Text => _text;

    private readonly TextMeshProUGUI _text;

    public ItemTypeButton
    (
        GameObject        buttonPrefab,
        Action<ListEntry> onSelect,
        Transform         parent,
        ScrollRect        scrollRect,
        string            textName
    ) : base(buttonPrefab, null, parent, scrollRect)
    {
        _text = ButtonObj.transform.Find(textName).GetComponent<TextMeshProUGUI>();
    }
}