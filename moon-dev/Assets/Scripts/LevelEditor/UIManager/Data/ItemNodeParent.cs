using System.Collections.Generic;
using LevelEditor;
using UnityEngine;
using UnityEngine.UI;

public class ItemNodeParent : ItemNode
{
    private List<ItemNodeChild> m_childList = new List<ItemNodeChild>();

    private Button m_arrowButton;

    private bool m_isShowOrHide = true;

    public List<ItemNodeChild> GetTargetChilds()
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
            child.GetItemNodeTransform.gameObject.SetActive(true);
        }
        m_arrowButton.transform.rotation = Quaternion.Euler(0,0,90);
    }
    
    private void HideChilds()
    {
        foreach (var child in m_childList)
        {
            child.GetItemNodeTransform.gameObject.SetActive(false);
        }
        m_arrowButton.transform.rotation = Quaternion.Euler(0,0,0);
    }

    public void AddTargetChilds(ItemNodeChild targetChild)
    {
        m_childList.Add(targetChild);
    }


    public ItemNodeParent(string itemName, Transform itemNodeTransform, UIProperty.ItemNodeProperty itemNodeProperty, OnSelect onSelect) : base(itemName, itemNodeTransform, itemNodeProperty, onSelect)
    {
        m_arrowButton = itemNodeTransform.transform.Find("Arrow").GetComponent<Button>();
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
