using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeleteCommand : LevelEditorCommand
{
    private GameObject m_gameObject;

    public ItemDeleteCommand(GameObject gameObject)
    {
        m_gameObject = gameObject;
    }
    
    public override void Execute()
    {
        m_gameObject.SetActive(false);
    }

    public override void Undo()
    {
        m_gameObject.SetActive(true);
    }
}
