using System.Collections;
using System.Collections.Generic;
using Frame.Static.Extensions;
using UnityEngine;

public class ItemSelectCommand : LevelEditorCommand
{
    private List<GameObject> m_targetList;
    private OutlinePainter m_outlinePainter;
    private List<GameObject> m_lastList = new List<GameObject>();
    private List<GameObject> m_nextList = new List<GameObject>();
    
    public ItemSelectCommand(List<GameObject> targetList,List<GameObject> nextList,OutlinePainter outlinePainter)
    {
        m_targetList = targetList;
        m_outlinePainter = outlinePainter;
        m_lastList.AddRange(targetList);
        m_nextList.AddRange(nextList);
    }
    
    public override void Execute()
    {
        m_targetList.Clear();
        m_targetList.AddRange(m_nextList);
        m_outlinePainter.SetTargetObj = m_targetList;
    }

    public override void Undo()
    {
        m_targetList.Clear();
        m_targetList.AddRange(m_lastList);
        m_outlinePainter.SetTargetObj = m_targetList;
    }
}
