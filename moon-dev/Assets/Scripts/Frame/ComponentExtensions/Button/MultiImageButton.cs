using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Frame.CompnentExtensions
{
    public class MultiTargetButton : Button
    {
        [SerializeField]
        private List<Graphic> TargetGraphics;
    
        protected override void DoStateTransition(SelectionState state, bool instant)
        {
            base.DoStateTransition(state,instant);
            Color color;
            switch (state)
            {
                case SelectionState.Normal:
                    color = colors.normalColor;
                    break;
                case SelectionState.Highlighted:
                    color = colors.highlightedColor;
                    break;
                case SelectionState.Pressed:
                    color = colors.pressedColor;
                    break;
                case SelectionState.Selected:
                    color = colors.selectedColor;
                    break;
                case SelectionState.Disabled:
                    color = colors.disabledColor;
                    break;
                default:
                    color = Color.black;
                    break;
            }
    
            if (gameObject.activeInHierarchy)
            {
                switch (transition)
                {
                    case Transition.ColorTint:
                        ColorTween(color * colors.colorMultiplier, instant);
                        break;
                }
            }
        }
    
        private void ColorTween(Color targetColor, bool instant)
        {
            if (TargetGraphics == null) return;
    
            foreach (var graphic in TargetGraphics)
            {
                if (graphic == null) continue;
    
                graphic.CrossFadeColor(targetColor, (!instant) ? colors.fadeDuration : 0f, true, true);
            }
        }
    }
}