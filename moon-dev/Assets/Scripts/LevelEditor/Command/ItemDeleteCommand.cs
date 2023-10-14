using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDeleteCommand : LevelEditorCommand
{
    private List<GameObject> m_gameobjects = new List<GameObject>();

    public ItemDeleteCommand(List<GameObject> gameobjects)
    {
        m_gameobjects.AddRange(gameobjects);
    }
    
    public override void Execute()
    {
        foreach (var obj in m_gameobjects)
        {
            obj.SetActive(false);
        }
    }

    public override void Undo()
    {
        foreach (var obj in m_gameobjects)
        {
            obj.SetActive(true);
        }
    }
}
