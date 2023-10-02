using UnityEngine;

public class ItemRectCommand : LevelEditorCommand
{
    private Vector3 m_lastPosition;
    private Vector3 m_lastScale;
    private Vector3 m_nextPosition;
    private Vector3 m_nextScale;
    private Transform m_transform;

    ItemRectCommand(Transform transform,Vector3 nextPosition,Vector3 nextScale)
    {
        m_lastPosition = transform.position;
        m_lastScale = transform.localScale;
        m_nextPosition = nextPosition;
        m_nextScale = nextScale;
    }
    public override void Execute()
    {
        m_transform.position = m_nextPosition;
        m_transform.localScale = m_nextScale;
    }

    public override void Undo()
    {
        m_transform.position = m_lastPosition;
        m_transform.localScale = m_lastScale;
    }
}
