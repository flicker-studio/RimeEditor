using System.Collections;
using System.Collections.Generic;
using Frame.Tool.Pool;
using UnityEngine;

namespace LevelEditor
{
    public class ItemCreateCommand : Command
    {
        private GameObject m_item;
        private GameObject m_itemPrefab;
        
        private List<GameObject> m_targetList;
        private OutlinePainter m_outlinePainter;
        private List<GameObject> m_lastList = new List<GameObject>();
        private List<GameObject> m_nextList = new List<GameObject>();
        
        private Vector3 GetScreenMiddlePoint =>
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));

        public ItemCreateCommand(List<GameObject> targetList,OutlinePainter outlinePainter,GameObject prefab)
        {
            m_targetList = targetList;
            m_outlinePainter = outlinePainter;
            m_lastList.AddRange(targetList);
            m_itemPrefab = prefab;
        }
    
        public override void Execute()
        {
            m_item = ObjectPool.Instance.OnTake(m_itemPrefab);
            m_item.transform.rotation = Quaternion.identity;
            m_item.transform.position = GetScreenMiddlePoint;
            m_nextList.Clear();
            m_nextList.Add(m_item);
            m_targetList.Clear();
            m_targetList.AddRange(m_nextList);
            m_outlinePainter.SetTargetObj = m_targetList;
        }

        public override void Undo()
        {
            ObjectPool.Instance.OnRelease(m_item);
            m_targetList.Clear();
            m_targetList.AddRange(m_lastList);
            m_outlinePainter.SetTargetObj = m_targetList;
        }
    }

}