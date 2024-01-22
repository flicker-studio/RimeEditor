using UnityEngine;

public class TestPopover : MonoBehaviour
{
    void Start()
    {
        Debug.Log(GetComponentInParent<Canvas>().rootCanvas.gameObject.name);
    }
}
