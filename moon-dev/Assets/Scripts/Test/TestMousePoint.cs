using UnityEngine;
using UnityEngine.EventSystems;

public class TestMousePoint : MonoBehaviour
{
    private void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            if (EventSystem.current.currentSelectedGameObject != null)
            {
                Debug.Log(EventSystem.current.currentSelectedGameObject.name);
            }
        }
    }
}
