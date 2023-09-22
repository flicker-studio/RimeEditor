using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[CreateAssetMenu(menuName = "CustomProperty/ObjectFactory",order = 1,fileName = "ObjectFactory")]
public class PrefabFactory : ScriptableObject
{
    [CustomLabel("裁切生成物体")] 
    public GameObject SLICE_OBJ;

    [CustomLabel("组合碰撞刚体父节点")] 
    public GameObject COMBINATION_COLLIDES_RIGIDBODY_PARENT;
    
    [CustomLabel("组合碰撞非刚体父节点")] 
    public GameObject COMBINATION_COLLIDES_NOT_RIGIDBODY_PARENT;
    
    [CustomLabel("刚体组件父节点")] 
    public GameObject RIGIDBODY_PARENT;
}
