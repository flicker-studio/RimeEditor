using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ItemNodeParent : ItemNode
{
    private List<ItemNodeChild> m_childList = new List<ItemNodeChild>();

    private Button m_arrowButton;

    private bool m_isShowOrHide = true;

    public List<ItemNodeChild> GetChilds()
    {
        List<ItemNodeChild> tempList = new List<ItemNodeChild>();
        foreach (var itemNodeChild in m_childList)
        {
            tempList.Add(itemNodeChild);
        }

        return tempList;
    }

    private void ShowChilds()
    {
        foreach (var child in m_childList)
        {
            child.ItemNodeTransform.gameObject.SetActive(true);
        }
        m_arrowButton.transform.rotation = Quaternion.Euler(0,0,90);
    }
    
    private void HideChilds()
    {
        foreach (var child in m_childList)
        {
            child.ItemNodeTransform.gameObject.SetActive(false);
        }
        m_arrowButton.transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void AddChild(ItemNodeChild targetChild)
    {
        m_childList.Add(targetChild);
        if (m_childList.Count > 0)
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPE), Itemtype)}({m_childList.Count})";
        }
        else
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPE), Itemtype)}";
        }
    }
    
    public void RemoveChild(ItemNodeChild itemNodeChild)
    {
        m_childList.Remove(itemNodeChild);
        if (m_childList.Count > 0)
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPE), Itemtype)}({m_childList.Count})";
        }
        else
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPE), Itemtype)}";
        }
    }


    public ItemNodeParent(ItemProduct itemProduct, Transform itemNodeContent,OnSelect onSelect) 
        : base(itemProduct, itemNodeContent, onSelect)
    {
        ItemName = Enum.GetName(typeof(ITEMTYPE), itemProduct.ItemType);
        m_arrowButton = ItemNodeTransform.transform.Find("Arrow").GetComponent<Button>();
        m_arrowButton.gameObject.SetActive(true);
        m_itemButton.onClick.AddListener(() =>
        {
            m_isShowOrHide = true;
            ShowChilds();
        });
        m_arrowButton.onClick.AddListener(() =>
        {
            m_isShowOrHide = !m_isShowOrHide;
            if (m_isShowOrHide)
            {
                ShowChilds();
            }
            else
            {
                HideChilds();
            }
        });
    }
}
