using System;
using Moon.Kernel.Extension;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Frame.CompnentExtensions
{
    /// <summary>
    ///     Added EventTrigger Button
    /// </summary>
    public class EventButton
    {
        /// <summary>
        /// </summary>
        public bool IsSelected
        {
            get => _isSelected;
            set
            {
                _isSelected = value;

                /*_image.color = value
                    ? _button.colors.selectedColor
                    : _button.colors.normalColor;*/
            }
        }

        public readonly Button _button;
        // private readonly Image  _image;

        private readonly EventTrigger _trigger;
        private          bool         _isSelected;

        public EventButton
        (
            GameObject gameObject,
            Action     onClick,
            ScrollRect scrollRect = null
        )
        {
            _button = gameObject.GetComponent<Button>();
            // _image   = gameObject.GetComponent<Image>();
            _trigger = gameObject.AddComponent<EventTrigger>();
//            _button.onClick.AddListener(onClick.Invoke);
            InitScroll(scrollRect);
        }

        public void AddEvents(Action onSelect)
        {
            _button.onClick.AddListener(() => { onSelect?.Invoke(); });
        }

        private void InitEvents(Action<Button> onSelect)
        {
            _button.onClick.AddListener(() => { onSelect?.Invoke(_button); });

            _button.enabled = false;
            // _image.color    = _button.colors.normalColor;

            var pointerEvent = new EventTrigger.Entry
                               {
                                   eventID = EventTriggerType.PointerClick
                               };
            pointerEvent.callback.AddListener(_ => { onSelect?.Invoke(_button); });
            _trigger.triggers.Add(pointerEvent);

            _button.AddTriggerEvent(EventTriggerType.PointerEnter, _ =>
            {
                if (_isSelected) return;

                // _image.color = _button.colors.highlightedColor;
            });

            _button.AddTriggerEvent(EventTriggerType.PointerExit, context =>
            {
                if (_isSelected) return;

                //  _image.color = _button.colors.normalColor;
            });
        }

        private void InitScroll(ScrollRect scrollView)
        {
            if (scrollView is null) return;

            // AddTrigger(EventTriggerType.BeginDrag,               data => { scrollView.OnBeginDrag((PointerEventData)data); });
            // AddTrigger(EventTriggerType.Drag,                    data => { scrollView.OnEndDrag((PointerEventData)data); });
            // AddTrigger(EventTriggerType.EndDrag,                 data => { scrollView.OnScroll((PointerEventData)data); });
            // AddTrigger(EventTriggerType.InitializePotentialDrag, data => { scrollView.OnInitializePotentialDrag((PointerEventData)data); });
            AddTrigger(EventTriggerType.Scroll, data => { scrollView.OnScroll((PointerEventData)data); });
        }

        public void AddEvents(EventTriggerType triggerType, Action<BaseEventData> eventData)
        {
            _button.AddTriggerEvent(triggerType, eventData);
        }

        public void RemoveEvents()
        {
            _button.enabled = true;
            _trigger.triggers.Clear();
        }

        public void Invoke()
        {
            _trigger.OnPointerClick(null);
        }

        /// <summary>
        ///     Add a corresponding event to the button
        /// </summary>
        /// <param name="triggerType"></param>
        /// <param name="action"></param>
        private void AddTrigger(EventTriggerType triggerType, UnityAction<BaseEventData> action)
        {
            var entry = new EventTrigger.Entry
                        {
                            eventID = triggerType
                        };
            entry.callback.AddListener(action);
            _trigger.triggers.Add(entry);
        }
    }
}