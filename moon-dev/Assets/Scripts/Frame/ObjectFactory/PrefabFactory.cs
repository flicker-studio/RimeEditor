using Moon.Kernel.Attribute;
using UnityEngine;

namespace Data.ScriptableObject
{
    [CreateAssetMenu(menuName = "CustomProperty/ObjectFactory", order = 1, fileName = "ObjectFactory")]
    public class PrefabFactory : UnityEngine.ScriptableObject
    {
        [field: SerializeField, CustomLabel("弹窗预制体")]
        public GameObject POPOVER_WINDOW { get; private set; }

        [field: SerializeField, CustomLabel("裁切生成物体")]
        public GameObject SLICE_OBJ { get; private set; }

        [field: SerializeField, CustomLabel("组合碰撞刚体父节点")]
        public GameObject COMBINATION_COLLIDES_RIGIDBODY_PARENT { get; private set; }

        [field: SerializeField, CustomLabel("组合碰撞非刚体父节点")]
        public GameObject COMBINATION_COLLIDES_NOT_RIGIDBODY_PARENT { get; private set; }

        [field: SerializeField, CustomLabel("刚体组件父节点")]
        public GameObject RIGIDBODY_PARENT { get; private set; }

        [field: SerializeField, CustomLabel("空物体节点")]
        public GameObject EMPTY_GAMEOBJECT { get; private set; }

        [field: SerializeField, CustomLabel("层级面板物体节点")]
        public GameObject ITEM_NODE { get; private set; }

        [field: SerializeField, CustomLabel("物件栏")]
        public GameObject ITEM_DETAIL_GROUP { get; private set; }

        [field: SerializeField, CustomLabel("物件格子")]
        public GameObject ITEM_LATTICE { get; private set; }

        [field: SerializeField, CustomLabel("物件种类侧边栏")]
        public GameObject ITEM_TYPE { get; private set; }

        [field: SerializeField, CustomLabel("玩家")]
        public GameObject PLAYER { get; private set; }

        [field: SerializeField, CustomLabel("玩家相机")]
        public GameObject PLAYER_CAMERA { get; private set; }

        [field: SerializeField, CustomLabel("裁切器")]
        public GameObject SLICER { get; private set; }

        [field: SerializeField, CustomLabel("布尔项")]
        public GameObject BOOL_ITEM { get; private set; }

        [field: SerializeField, CustomLabel("关卡列表按钮")]
        public GameObject LEVEL_DATA_BUTTON { get; private set; }
    }
}