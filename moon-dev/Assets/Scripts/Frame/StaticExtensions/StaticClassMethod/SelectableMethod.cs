using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class SelectableMethod
{
    public static EventTrigger AddTriggerEvent(this Selectable theSelectable, EventTriggerType eventTriggerType, Action<BaseEventData> onTriggerAction = null)
    {
        EventTrigger eventrTrigger = theSelectable.gameObject.GetComponent<EventTrigger>();
        if (eventrTrigger == null)
        {
            eventrTrigger = theSelectable.gameObject.AddComponent<EventTrigger>();
        }
        if (onTriggerAction != null)
        {
            EventTrigger.Entry pointerEvent = new EventTrigger.Entry();
            pointerEvent.eventID = eventTriggerType;
            pointerEvent.callback.AddListener((x) => onTriggerAction(x));
            eventrTrigger.triggers.Add(pointerEvent);
        }
        return eventrTrigger;
    }
    
    public static void RemoveAllTriggerEvents(this Selectable theSelectable)
    {
        EventTrigger eventTrigger = theSelectable.gameObject.GetComponent<EventTrigger>();
        if (eventTrigger != null)
        {
            eventTrigger.triggers.Clear();
        }
    }
}
