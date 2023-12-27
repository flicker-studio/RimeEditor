using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

public class IconTest : MonoBehaviour
{
    private void Start()
    {
#if UNITY_EDITOR
        GetComponent<RawImage>().texture = EditorGUIUtility.IconContent("Shader Icon").image;
#endif
    }
}