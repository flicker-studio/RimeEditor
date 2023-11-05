using UnityEngine;

public class ItemTransformData
{
    public Vector3 Position;
    public Quaternion Rotation;
    public Vector3 Scale;

    public ItemTransformData(Transform transform)
    {
        Position = transform.position;
        Rotation = transform.rotation;
        Scale = transform.localScale;
    }
}

public class ItemPhysicalData
{
    public bool IsItemCanBeCopy;
    public bool IsRigidBody;

    public ItemPhysicalData(bool isItemCanBeCopy, bool isRigidBody)
    {
        IsItemCanBeCopy = isItemCanBeCopy;
        IsRigidBody = isRigidBody;
    }
}


public class ItemData : IWritable<ItemTransformData>,IWritable<ItemPhysicalData>
{
    private ItemTransformData m_itemTransform;
    private ItemPhysicalData m_itemPhysicalData;
    
    ItemData(ItemTransformData transformData,ItemPhysicalData physicalData)
    {
        WriteData(transformData);
        WriteData(physicalData);
    }

    public (ItemTransformData,ItemPhysicalData) ReadData()
    {
        return (m_itemTransform, m_itemPhysicalData);
    }
    
    public void WriteData(ItemTransformData data)
    {
        m_itemTransform = data;
    }

    public void WriteData(ItemPhysicalData data)
    {
        m_itemPhysicalData = data;
    }
}
