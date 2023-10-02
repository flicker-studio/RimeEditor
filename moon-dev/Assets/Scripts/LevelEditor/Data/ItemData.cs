using UnityEngine;

public class ItemData
{
    public string Name;
    public Vector3 Position;
    public Vector3 Rotation;
    public Vector3 Scale;
    public bool CanBeCopy;
    public bool CanBePush;

    public ItemData(GameObject gameObject,bool canBeCopy,bool canBePush)
    {
        Name = gameObject.name;
        Position = gameObject.transform.position;
        Rotation = gameObject.transform.rotation.eulerAngles;
        Scale = gameObject.transform.localScale;
        CanBeCopy = canBeCopy;
        CanBePush = canBePush;
    }
}