using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class PropogateDrag : MonoBehaviour
{
    public UnityEngine.UI.ScrollRect scrollView;
    // Start is called before the first frame update
    void OnDisable()
    {
        EventTrigger trigger = GetComponent<EventTrigger>();
        EventTrigger.Entry entryBegin = new EventTrigger.Entry(), entryDrag = new EventTrigger.Entry(), entryEnd = new EventTrigger.Entry(), entrypotential = new EventTrigger.Entry()
            , entryScroll = new EventTrigger.Entry();

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
}
