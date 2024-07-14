using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class PropogateDrag : MonoBehaviour
{
    public ScrollRect scrollView;

    // Start is called before the first frame update
    private void OnDisable()
    {
        var trigger = GetComponent<EventTrigger>();

        EventTrigger.Entry entryBegin     = new(),
                           entryDrag      = new(),
                           entryEnd       = new(),
                           entrypotential = new(),
                           entryScroll    = new();

        entryBegin.eventID = EventTriggerType.BeginDrag;
        entryBegin.callback.AddListener(data => { scrollView.OnBeginDrag((PointerEventData)data); });
        trigger.triggers.Add(entryBegin);

        entryDrag.eventID = EventTriggerType.Drag;
        entryDrag.callback.AddListener(data => { scrollView.OnDrag((PointerEventData)data); });
        trigger.triggers.Add(entryDrag);

        entryEnd.eventID = EventTriggerType.EndDrag;
        entryEnd.callback.AddListener(data => { scrollView.OnEndDrag((PointerEventData)data); });
        trigger.triggers.Add(entryEnd);

        entrypotential.eventID = EventTriggerType.InitializePotentialDrag;
        entrypotential.callback.AddListener(data => { scrollView.OnInitializePotentialDrag((PointerEventData)data); });
        trigger.triggers.Add(entrypotential);

        entryScroll.eventID = EventTriggerType.Scroll;
        entryScroll.callback.AddListener(data => { scrollView.OnScroll((PointerEventData)data); });
        trigger.triggers.Add(entryScroll);
    }
}