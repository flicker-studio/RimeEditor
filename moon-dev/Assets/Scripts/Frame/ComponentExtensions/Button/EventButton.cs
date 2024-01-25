using System;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Frame.CompnentExtensions
{
    public class EventButton<T> where T : class
    {
        public bool IsSelected
        {
            get { return m_isSelected; }
            set
            {
                m_isSelected = value;

                if (m_isSelected)
                {
                    m_image.color = m_button.colors.selectedColor;
                }
                else
                {
                    m_image.color = m_button.colors.normalColor;
                }
            }
        }

        private Button m_button;

        private Image m_image;

        private T m_item;

        private bool m_isSelected;

        public EventButton(T item, Transform itemRect, Action<T> onSelect, ScrollRect scrollRect = null)
        {
            m_item = item;
            m_button = itemRect.GetComponent<Button>();
            m_image = itemRect.GetComponent<Image>();
            InitEvents(onSelect);

            if (scrollRect != null)
            {
                InitScroll(scrollRect);
            }
        }

        private void InitEvents(Action<T> onSelect)
        {
            m_button.enabled = false;
            m_image.color = m_button.colors.normalColor;
            m_button.RemoveAllTriggerEvents();
            m_button.AddTriggerEvent(EventTriggerType.PointerClick, context => { onSelect?.Invoke(m_item); });

            m_button.AddTriggerEvent(EventTriggerType.PointerEnter, context =>
            {
                if (m_isSelected) return;

                m_image.color = m_button.colors.highlightedColor;
            });

            m_button.AddTriggerEvent(EventTriggerType.PointerExit, context =>
            {
                if (m_isSelected) return;

                m_image.color = m_button.colors.normalColor;
            });
        }

        private void InitScroll(ScrollRect scrollView)
        {
            EventTrigger trigger = m_button.GetComponent<EventTrigger>();

            EventTrigger.Entry entryBegin = new EventTrigger.Entry(),
                entryDrag = new EventTrigger.Entry(),
                entryEnd = new EventTrigger.Entry(),
                entrypotential = new EventTrigger.Entry(),
                entryScroll = new EventTrigger.Entry();

            entryBegin.eventID = EventTriggerType.BeginDrag;
            entryBegin.callback.AddListener((data) => { scrollView.OnBeginDrag((PointerEventData)data); });
            trigger.triggers.Add(entryBegin);

            entryDrag.eventID = EventTriggerType.Drag;
            entryDrag.callback.AddListener((data) => { scrollView.OnDrag((PointerEventData)data); });
            trigger.triggers.Add(entryDrag);

            entryEnd.eventID = EventTriggerType.EndDrag;
            entryEnd.callback.AddListener((data) => { scrollView.OnEndDrag((PointerEventData)data); });
            trigger.triggers.Add(entryEnd);

            entrypotential.eventID = EventTriggerType.InitializePotentialDrag;
            entrypotential.callback.AddListener((data) => { scrollView.OnInitializePotentialDrag((PointerEventData)data); });
            trigger.triggers.Add(entrypotential);

            entryScroll.eventID = EventTriggerType.Scroll;
            entryScroll.callback.AddListener((data) => { scrollView.OnScroll((PointerEventData)data); });
            trigger.triggers.Add(entryScroll);
        }

        public void AddEvents(EventTriggerType triggerType, Action<BaseEventData> eventData)
        {
            m_button.AddTriggerEvent(triggerType, eventData);
        }

        public void RemoveEvents()
        {
            m_button.enabled = true;
            m_button.RemoveAllTriggerEvents();
        }

        public void Invoke()
        {
            m_button?.Invoke(EventTriggerType.PointerClick);
        }
    }
}