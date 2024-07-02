using System;
using DG.Tweening;
using Frame.Tool.Popover;
using Moon.Kernel;
using Moon.Runtime.DesignPattern;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Object = UnityEngine.Object;

namespace Moon.Runtime
{
    /// <summary>
    /// </summary>
    public enum Popoverlocation
    {
        /// <summary>
        /// </summary>
        TOP,
        
        /// <summary>
        /// </summary>
        RIGHT,
        
        /// <summary>
        /// </summary>
        BOTTOM,
        
        /// <summary>
        /// </summary>
        LEFT,
        
        /// <summary>
        /// </summary>
        CENTER
    }
    
    /// <summary>
    /// </summary>
    public class PopoverLauncher : Singleton<PopoverLauncher>
    {
        /// <summary>
        /// </summary>
        public bool CanInput { get; private set; } = true;
        
        private readonly PopoverProperty.SelectorPopoverProperty _mSelectorPopoverProperty;
        private readonly PopoverProperty.TipsPopoverProperty     _mTipsPopoverProperty;
        
        /// <summary>
        /// </summary>
        public PopoverLauncher()
        {
            var popoverProperty = Explorer.TryGetSetting<PopoverProperty>();
            _mSelectorPopoverProperty = popoverProperty.GetSelectorPopoverProperty;
            _mTipsPopoverProperty     = popoverProperty.GetTipsPopoverProperty;
        }
        
        /// <summary>
        /// </summary>
        /// <param name="referenceTransform"></param>
        /// <param name="popoverLocation"></param>
        /// <param name="size"></param>
        /// <param name="color"></param>
        /// <param name="text"></param>
        /// <param name="duration"></param>
        public void LaunchTip(Transform referenceTransform, Popoverlocation popoverLocation, Vector2 size, Color color, string text, float duration)
        {
            var tipsPopover = Object.Instantiate(_mTipsPopoverProperty.TIPS_POPOVER_PREFAB);
            var popoverRect = tipsPopover.transform as RectTransform;
            if (popoverRect == null) throw new NullReferenceException();
            
            var popoverText = popoverRect.Find(_mTipsPopoverProperty.DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            popoverRect.SetParent(referenceTransform.GetComponentInParent<Canvas>().rootCanvas.transform);
            var popoverImage = tipsPopover.GetComponent<Image>();
            
            popoverRect.sizeDelta = size;
            popoverText.text      = text;
            
            switch (popoverLocation)
            {
                case Popoverlocation.CENTER:
                    popoverRect.anchorMin        = new Vector2(0.5f, 0.5f);
                    popoverRect.anchorMax        = new Vector2(0.5f, 0.5f);
                    popoverRect.anchoredPosition = Vector2.zero;
                    break;
                case Popoverlocation.BOTTOM:
                    popoverRect.anchorMin        = new Vector2(0.5f, 0);
                    popoverRect.anchorMax        = new Vector2(0.5f, 0);
                    popoverRect.anchoredPosition = new Vector2(0,    popoverRect.sizeDelta.y / 2);
                    break;
                case Popoverlocation.LEFT:
                    popoverRect.anchorMin        = new Vector2(0,                           0.5f);
                    popoverRect.anchorMax        = new Vector2(0,                           0.5f);
                    popoverRect.anchoredPosition = new Vector2(popoverRect.sizeDelta.x / 2, 0);
                    break;
                case Popoverlocation.RIGHT:
                    popoverRect.anchorMin        = new Vector2(1f,                           0.5f);
                    popoverRect.anchorMax        = new Vector2(1f,                           0.5f);
                    popoverRect.anchoredPosition = new Vector2(-popoverRect.sizeDelta.x / 2, 0);
                    break;
                case Popoverlocation.TOP:
                    popoverRect.anchorMin        = new Vector2(0.5f, 1f);
                    popoverRect.anchorMax        = new Vector2(0.5f, 1f);
                    popoverRect.anchoredPosition = new Vector2(0,    -popoverRect.sizeDelta.y / 2);
                    break;
            }
            
            popoverImage.color = color;
            popoverText.color  = Color.white;
            
            popoverImage.DOColor(Color.clear, duration).OnComplete(() => { Object.Destroy(tipsPopover); });
            popoverText.DOColor(Color.clear, duration);
        }
        
        /// <summary>
        /// </summary>
        /// <param name="referenceTransform"></param>
        /// <param name="describe"></param>
        /// <param name="yesAction"></param>
        /// <param name="yesStr"></param>
        /// <param name="noStr"></param>
        public void LaunchSelector(Transform referenceTransform, string describe, Action yesAction, string yesStr = "Yes", string noStr = "No")
        {
            InputManager.Instance.CanInput = false;
            CanInput                       = false;
            
            var selectorPopover = Object.Instantiate(_mSelectorPopoverProperty.SELECTOR_POPOVER_PREFAB,
                                                     referenceTransform.GetComponentInParent<Canvas>().rootCanvas.transform, true);
            var selectorPopoverRect = selectorPopover.transform as RectTransform;
            if (selectorPopoverRect == null) throw new NullReferenceException();
            
            selectorPopoverRect.offsetMin = Vector2.zero;
            selectorPopoverRect.offsetMax = Vector2.zero;
            
            var backGround = selectorPopoverRect.Find(_mSelectorPopoverProperty.BACKGROUND) as RectTransform;
            if (backGround == null) throw new NullReferenceException();
            
            var popoverText = backGround.Find(_mSelectorPopoverProperty.DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            var yesButton   = backGround.Find(_mSelectorPopoverProperty.YES_BUTTON).GetComponent<Button>();
            var noButton    = backGround.Find(_mSelectorPopoverProperty.NO_BUTTON).GetComponent<Button>();
            var yesText     = yesButton.transform.Find(_mSelectorPopoverProperty.BUTTON_DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            var noText      = noButton.transform.Find(_mSelectorPopoverProperty.BUTTON_DESCIBE_TEXT).GetComponent<TextMeshProUGUI>();
            
            popoverText.text = describe;
            yesText.text     = yesStr;
            noText.text      = noStr;
            
            yesButton.onClick.RemoveAllListeners();
            
            yesButton.onClick.AddListener(() =>
            {
                yesAction?.Invoke();
                Object.Destroy(selectorPopover);
                CanInput                       = true;
                InputManager.Instance.CanInput = true;
            });
            
            noButton.onClick.RemoveAllListeners();
            
            noButton.onClick.AddListener(() =>
            {
                Object.Destroy(selectorPopover);
                CanInput                       = true;
                InputManager.Instance.CanInput = true;
            });
        }
    }
}