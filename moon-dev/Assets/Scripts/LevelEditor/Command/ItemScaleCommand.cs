using UnityEngine;

public class ItemScaleCommand : LevelEditorCommand
{
    private Vector3 m_nextScale;
    private Vector3 m_lastScale;
    private Transform m_transform;
    
    public ItemScaleCommand(Transform transform,Vector3 nextScale)
    {
        m_lastScale = transform.localScale;
        m_nextScale = nextScale;
    }
    public override void Execute()
    {
        m_transform.localScale = m_nextScale;
    }

    public override void Undo()
    {
        m_transform.localScale = m_lastScale;
    }
}
