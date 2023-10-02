using UnityEngine;

public class ItemRotationCommand : LevelEditorCommand
{
    private Vector3 m_lastRotation;
    private Vector3 m_nextRotation;
    private Transform m_transform;

    public ItemRotationCommand(Transform transform,Vector3 nextRotation)
    {
        m_lastRotation = transform.position;
        m_nextRotation = nextRotation;
    }
    public override void Execute()
    {
        m_transform.rotation = Quaternion.Euler(m_nextRotation);
    }

    public override void Undo()
    {
        m_transform.rotation = Quaternion.Euler(m_lastRotation);
    }
}
