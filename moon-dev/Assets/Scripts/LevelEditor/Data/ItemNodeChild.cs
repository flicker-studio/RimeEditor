using System;
using Cysharp.Threading.Tasks;
using LevelEditor;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ItemNodeChild : ItemNode
{
    public ItemDataBase ItemData { get; }

    private float m_doubleClickTimer;

    private int m_clickCount = 0;

    public ItemNodeChild(ItemProduct itemProduct, Transform itemNodeTransform, Action<ItemNode> onSelect, ItemDataBase targetItem, ScrollRect scrollView)
        : base(itemProduct, itemNodeTransform, onSelect, scrollView)
    {
        ItemData = targetItem;
        ItemName = itemProduct.Name;
        m_arrowButton.gameObject.SetActive(false);

        m_eventButton.AddEvents(EventTriggerType.PointerClick, context =>
        {
            if (IsSelected && m_clickCount == 0)
            {
                UniTask.Void(DoubleClickTimer);
            }

            m_clickCount++;
        });
    }

    private async UniTaskVoid DoubleClickTimer()
    {
        m_doubleClickTimer = 0f;

        while (m_doubleClickTimer < 0.4f)
        {
            m_doubleClickTimer += Time.fixedDeltaTime;

            if (m_clickCount >= 2)
            {
                m_clickCount = 0;
                MoveCameraToItemPos();
                return;
            }

            await UniTask.WaitForFixedUpdate();
        }

        m_clickCount = 0;
    }

    private void MoveCameraToItemPos()
    {
        Camera.main.transform.position = ItemData.GetItemObjEditor.transform.position.NewZ(-10f);
    }
}