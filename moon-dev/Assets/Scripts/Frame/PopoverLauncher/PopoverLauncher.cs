using System;
using DG.Tweening;
using Frame.Tool.Pool;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.Tool.Popover
{
    public enum POPOVERLOCATION
    {
        Top,
        Right,
        Bottom,
        Left,
        Center
    }

    public class PopoverLauncher : Singleton<PopoverLauncher>
    {
        public bool CanInput { get; private set; } = true;
        
        private PopoverProperty.SelectorPopoverProperty m_selectorPopoverProperty;

        private PopoverProperty.TipsPopoverProperty m_tipsPopoverProperty;
        public PopoverLauncher()
        {
            PopoverProperty popoverProperty = Resources.Load<PopoverProperty>("GlobalSettings/PopoverProperty");
            m_selectorPopoverProperty = popoverProperty.GetSelectorPopoverProperty;
            m_tipsPopoverProperty = popoverProperty.GetTipsPopoverProperty;
        }

        public void LaunchTip(Transform referenceTransform,POPOVERLOCATION popoverLocation,Vector2 size,Color color,string text,float duration)
        {
            GameObject tipsPopover = ObjectPool.Instance.OnTake(m_tipsPopoverProperty.TIPS_POPOVER_PREFAB);
            RectTransform popoverRect = tipsPopover.transform as RectTransform;
            TextMeshProUGUI popoverText = popoverRect.Find(m_tipsPopoverProperty.DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            popoverRect.SetParent(referenceTransform.GetComponentInParent<Canvas>().rootCanvas.transform);
            Image popoverImage = tipsPopover.GetComponent<Image>();
            
            popoverRect.sizeDelta = size;
            popoverText.text = text;
            
            switch (popoverLocation)
            {
                case POPOVERLOCATION.Center:
                    popoverRect.anchorMin = new Vector2(0.5f,0.5f);
                    popoverRect.anchorMax = new Vector2(0.5f, 0.5f);
                    popoverRect.anchoredPosition = Vector2.zero;
                    break;
                case POPOVERLOCATION.Bottom:
                    popoverRect.anchorMin = new Vector2(0.5f,0);
                    popoverRect.anchorMax = new Vector2(0.5f, 0);
                    popoverRect.anchoredPosition = new Vector2(0, popoverRect.sizeDelta.y / 2);
                    break;
                case POPOVERLOCATION.Left:
                    popoverRect.anchorMin = new Vector2(0,0.5f);
                    popoverRect.anchorMax = new Vector2(0, 0.5f);
                    popoverRect.anchoredPosition = new Vector2(popoverRect.sizeDelta.x / 2, 0);
                    break;
                case POPOVERLOCATION.Right:
                    popoverRect.anchorMin = new Vector2(1f,0.5f);
                    popoverRect.anchorMax = new Vector2(1f, 0.5f);
                    popoverRect.anchoredPosition = new Vector2(-popoverRect.sizeDelta.x / 2, 0);
                    break;
                case POPOVERLOCATION.Top:
                    popoverRect.anchorMin = new Vector2(0.5f,1f);
                    popoverRect.anchorMax = new Vector2(0.5f, 1f);
                    popoverRect.anchoredPosition = new Vector2(0, -popoverRect.sizeDelta.y / 2);
                    break;
            }
            
            popoverImage.color = color;
            popoverText.color = Color.white;
            
            popoverImage.DOColor(Color.clear, duration).OnComplete(() =>
            {
                ObjectPool.Instance.OnRelease(tipsPopover);
            });
            popoverText.DOColor(Color.clear, duration);
        }

        public void LaunchSelector(Transform referenceTransform,string describe,Action yesAction,string yesStr = "Yes",string noStr = "No")
        {
            InputManager.Instance.CanInput = false;
            CanInput = false;
            GameObject selectorPopover = ObjectPool.Instance.OnTake(m_selectorPopoverProperty.SELECTOR_POPOVER_PREFAB);
            selectorPopover.transform.SetParent(referenceTransform.GetComponentInParent<Canvas>().rootCanvas.transform);
            RectTransform selectorPopoverRect = selectorPopover.transform as RectTransform;
            selectorPopoverRect.offsetMin = Vector2.zero;
            selectorPopoverRect.offsetMax = Vector2.zero;
            RectTransform backGround = selectorPopoverRect.Find(m_selectorPopoverProperty.BACKGROUND) as RectTransform;
            TextMeshProUGUI popoverText = backGround.Find(m_selectorPopoverProperty.DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            Button yesButton = backGround.Find(m_selectorPopoverProperty.YES_BUTTON).GetComponent<Button>();
            Button noButton = backGround.Find(m_selectorPopoverProperty.NO_BUTTON).GetComponent<Button>();
            TextMeshProUGUI yesText = yesButton.transform.Find(m_selectorPopoverProperty.BUTTON_DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            TextMeshProUGUI noText = noButton.transform.Find(m_selectorPopoverProperty.BUTTON_DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            popoverText.text = describe;
            yesText.text = yesStr;
            noText.text = noStr;
            yesButton.onClick.RemoveAllListeners();
            yesButton.onClick.AddListener(() =>
            {
                yesAction?.Invoke();
                ObjectPool.Instance.OnRelease(selectorPopover);
                CanInput = true;
                InputManager.Instance.CanInput = true;
            });
            noButton.onClick.RemoveAllListeners();
            noButton.onClick.AddListener(() =>
            {
                ObjectPool.Instance.OnRelease(selectorPopover);
                CanInput = true;
                InputManager.Instance.CanInput = true;
            });
        }
    }
}
