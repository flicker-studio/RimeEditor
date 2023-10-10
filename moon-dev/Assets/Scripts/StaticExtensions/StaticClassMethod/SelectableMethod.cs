using System;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public static class SelectableMethod
{
    public static EventTrigger AddTriggersEvents(this Selectable theSelectable, EventTriggerType eventTriggerType, Action<BaseEventData> onTriggerAction = null)
    {
        EventTrigger eventrTrigger = theSelectable.gameObject.AddComponent<EventTrigger>();
        if (onTriggerAction != null)
        {
            EventTrigger.Entry pointerEvent = new EventTrigger.Entry();
            pointerEvent.eventID = eventTriggerType;
            pointerEvent.callback.AddListener((x) => onTriggerAction(x));
            eventrTrigger.triggers.Add(pointerEvent);
        }
        return eventrTrigger;
    }
}
