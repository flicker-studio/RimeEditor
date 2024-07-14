using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Frame.CompnentExtensions
{
    public class GridButton : Button
    {
        public override void OnPointerExit(PointerEventData eventData)
        {
            base.OnPointerExit(eventData);
            if (interactable && eventData.pressPosition != Vector2.zero) DoStateTransition(SelectionState.Normal, false);
        }
    }
}