using UnityEngine;

public class ItemPositionCommand : LevelEditorCommand
{
    private Vector3 m_nextPosition;
    private Vector3 m_lastPosition;
    private Transform m_transform;
    
    public ItemPositionCommand(Transform transform,Vector3 nextPosition)
    {
        m_lastPosition = transform.position;
        m_nextPosition = nextPosition;
    }
    public override void Execute()
    {
        m_transform.position = m_nextPosition;
    }

    public override void Undo()
    {
        m_transform.position = m_lastPosition;
    }
}
