using Editor;
using UnityEngine;

namespace Data.ScriptableObject
{
    [CreateAssetMenu(menuName = "CustomProperty/ObjectFactory",order = 1,fileName = "ObjectFactory")]
    public class PrefabFactory : UnityEngine.ScriptableObject
    {
        [CustomLabel("裁切生成物体")] 
        public GameObject SLICE_OBJ;

        [CustomLabel("组合碰撞刚体父节点")] 
        public GameObject COMBINATION_COLLIDES_RIGIDBODY_PARENT;
    
        [CustomLabel("组合碰撞非刚体父节点")] 
        public GameObject COMBINATION_COLLIDES_NOT_RIGIDBODY_PARENT;
    
        [CustomLabel("刚体组件父节点")] 
        public GameObject RIGIDBODY_PARENT;
        
        [CustomLabel("空物体节点")] 
        public GameObject EMPTY_GAMEOBJECT;
    }
}

