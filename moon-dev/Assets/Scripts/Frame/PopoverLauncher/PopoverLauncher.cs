using Data.ScriptableObject;
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
        private GameObject m_popoverWindow;
        public PopoverLauncher()
        {
            PrefabFactory prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_popoverWindow = prefabFactory.POPOVER_WINDOW;
        }

        public void Launch(Transform referenceTransform,POPOVERLOCATION popoverLocation,Vector2 size,Color color,string text,float duration)
        {
            GameObject popoverWindow = ObjectPool.Instance.OnTake(m_popoverWindow);
            RectTransform popoverRect = popoverWindow.transform as RectTransform;
            TextMeshProUGUI popoverText = popoverRect.GetChild(0).GetComponent<TextMeshProUGUI>();
            popoverRect.SetParent(referenceTransform.GetComponentInParent<Canvas>().rootCanvas.transform);
            Image popoverImage = popoverWindow.GetComponent<Image>();
            
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
                ObjectPool.Instance.OnRelease(popoverWindow);
            });
            popoverText.DOColor(Color.clear, duration);
        }
    }
}
