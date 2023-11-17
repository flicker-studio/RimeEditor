using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemNodeParent : ItemNode
{
    private List<ItemNodeChild> m_childList = new List<ItemNodeChild>();

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

    public void ShowChilds()
    {
        m_isShowOrHide = true;
        foreach (var child in m_childList)
        {
            child.ItemNodeTransform.gameObject.SetActive(true);
        }
        m_arrowButton.transform.rotation = Quaternion.Euler(0,0,90);
    }
    
    private void HideChilds()
    {
        m_isShowOrHide = false;
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
            ItemName = $"{Enum.GetName(typeof(ITEMTYPEENUM), Itemtypeenum)}({m_childList.Count})";
        }
        else
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPEENUM), Itemtypeenum)}";
        }
    }
    
    public void RemoveChild(ItemNodeChild itemNodeChild)
    {
        m_childList.Remove(itemNodeChild);
        if (m_childList.Count > 0)
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPEENUM), Itemtypeenum)}({m_childList.Count})";
        }
        else
        {
            ItemName = $"{Enum.GetName(typeof(ITEMTYPEENUM), Itemtypeenum)}";
        }
    }


    public ItemNodeParent(ItemProduct itemProduct, Transform itemNodeContent,Action<ItemNode> onSelect,ScrollRect scrollView) 
        : base(itemProduct, itemNodeContent, onSelect,scrollView)
    {
        ItemName = Enum.GetName(typeof(ITEMTYPEENUM), itemProduct.ItemType);
        m_arrowButton.gameObject.SetActive(true);
        m_eventButton.AddEvents(EventTriggerType.PointerClick, context =>
        {
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
