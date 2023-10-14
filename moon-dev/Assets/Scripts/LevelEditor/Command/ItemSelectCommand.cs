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
    
    // private void ShowActionView(EXCUTEORDER excuteOrder)
    // {
    //     switch (excuteOrder)
    //     {
    //         case EXCUTEORDER.last:
    //             m_nextActionUI.SetActive(false);
    //             if (m_targetList.Count > 0)
    //             {
    //                 m_lastActionUI.transform.position = Camera.main
    //                     .WorldToScreenPoint(GetVector3ListFromGameObjectList(m_targetList).GetCenterPoint());
    //                 m_lastActionUI.SetActive(true);
    //             }
    //             else
    //             {
    //                 m_lastActionUI.SetActive(false);
    //             }
    //             break;
    //         case EXCUTEORDER.next:
    //             m_lastActionUI.SetActive(false);
    //             if (m_targetList.Count > 0)
    //             {
    //                 m_nextActionUI.transform.position = Camera.main
    //                     .WorldToScreenPoint(GetVector3ListFromGameObjectList(m_targetList).GetCenterPoint());
    //                 m_nextActionUI.SetActive(true);
    //             }
    //             else
    //             {
    //                 m_nextActionUI.SetActive(false);
    //             }
    //             break;
    //     }
    // }
    //
    // private List<Vector3> GetVector3ListFromGameObjectList(List<GameObject> gameobjectList)
    // {
    //     List<Vector3> vector3List = new List<Vector3>();
    //     foreach (var obj in gameobjectList)
    //     {
    //         vector3List.Add(obj.transform.position);
    //     }
    //
    //     return vector3List;
    // }
}
