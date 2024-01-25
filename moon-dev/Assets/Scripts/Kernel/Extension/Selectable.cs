using System;
using System.Linq;
using UnityEngine.EventSystems;

namespace Moon.Kernel.Extension
{
    /// <summary>
    /// </summary>
    public static class Selectable
    {
        public static EventTrigger AddTriggerEvent(this UnityEngine.UI.Selectable theSelectable, EventTriggerType eventTriggerType, Action<BaseEventData> onTriggerAction = null)
        {
            var eventrTrigger = theSelectable.gameObject.GetComponent<EventTrigger>();

            if (eventrTrigger == null)
            {
                eventrTrigger = theSelectable.gameObject.AddComponent<EventTrigger>();
            }

            if (onTriggerAction != null)
            {
                var pointerEvent = new EventTrigger.Entry();
                pointerEvent.eventID = eventTriggerType;
                pointerEvent.callback.AddListener(x => onTriggerAction(x));
                eventrTrigger.triggers.Add(pointerEvent);
            }

            return eventrTrigger;
        }

        public static void Invoke(this UnityEngine.UI.Selectable theSelectable, EventTriggerType eventTriggerType)
        {
            var eventrTrigger = theSelectable.gameObject.GetComponent<EventTrigger>();

            if (eventrTrigger == null)
            {
                eventrTrigger = theSelectable.gameObject.AddComponent<EventTrigger>();
            }

            var trigger = eventrTrigger.triggers
                .FirstOrDefault(trigger => trigger.eventID == eventTriggerType);

            if (trigger == null)
            {
                return;
            }

            trigger.callback?.Invoke(null);
        }

        public static void RemoveAllTriggerEvents(this UnityEngine.UI.Selectable theSelectable)
        {
            var eventTrigger = theSelectable.gameObject.GetComponent<EventTrigger>();

            if (eventTrigger != null)
            {
                eventTrigger.triggers.Clear();
            }
        }
    }
}