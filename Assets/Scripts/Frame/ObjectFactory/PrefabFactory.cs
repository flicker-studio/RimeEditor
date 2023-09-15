using System.Collections;
using System.Collections.Generic;
using UnityEngine;
[CreateAssetMenu(menuName = "ObjectFactory",order = 1,fileName = "ObjectFactory")]
public class PrefabFactory : ScriptableObject
{
    [CustomLabel("裁切生成物体")] 
    public GameObject SLICE_OBJ;
}
