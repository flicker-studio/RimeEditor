using System;
using Editor;
using UnityEngine;

namespace Frame.Tool.Popover
{
    [CreateAssetMenu(menuName = "CustomProperty/PopoverProperty",order = 5,fileName = "PopoverProperty")]
    public class PopoverProperty : ScriptableObject
    {
        [field:SerializeField,Header("提示弹窗属性")]
        public TipsPopoverProperty GetTipsPopoverProperty { get; private set; }
        [field:SerializeField,Header("选择弹窗属性")]
        public SelectorPopoverProperty GetSelectorPopoverProperty { get; private set; }
        
        [Serializable]
        public struct TipsPopoverProperty
        {
            [field:SerializeField,CustomLabel("提示弹窗预制体")]
            public GameObject TIPS_POPOVER_PREFAB{ get; private set; }
            [field:SerializeField,CustomLabel("提示弹窗文本名字")]
            public string DESCIBE_TEXT{ get; private set; }
        }
        
        [Serializable]
        public struct SelectorPopoverProperty
        {
            [field:SerializeField,CustomLabel("选择弹窗预制体")]
            public GameObject SELECTOR_POPOVER_PREFAB{ get; private set; }
            [field:SerializeField,CustomLabel("选择弹窗背景名字")]
            public string BACKGROUND{ get; private set; }
            [field:SerializeField,CustomLabel("选择弹窗描述文字名字")]
            public string DESCIBE_TEXT{ get; private set; }
            [field:SerializeField,CustomLabel("选择弹窗确定按钮")]
            public string YES_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("选择弹窗取消按钮")]
            public string NO_BUTTON{ get; private set; }
            [field:SerializeField,CustomLabel("选择弹窗按钮描述文本名字")]
            public string BUTTON_DESCIBE_TEXT{ get; private set; }
        }
    }
}
