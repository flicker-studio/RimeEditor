using System.Collections.Generic;
using Frame.Tool.Pool;
using UnityEngine;

namespace LevelEditor
{
    public class ItemCreateCommand : Command
    {
        private GameObject m_item;
        private GameObject m_itemPrefab;
        private ITEMTYPE m_itemType;
        
        private List<GameObject> m_targetList;
        private OutlinePainter m_outlinePainter;
        private List<GameObject> m_lastList = new List<GameObject>();
        private Vector3 m_createPoint;
        
        private Vector3 GetScreenMiddlePoint =>
            Camera.main.ScreenToWorldPoint(new Vector3(Screen.width/2, Screen.height/2,
                Mathf.Abs(Camera.main.transform.position.z)));

        public ItemCreateCommand(List<GameObject> targetList,OutlinePainter outlinePainter,ItemProduct prefab)
        {
            m_targetList = targetList;
            m_outlinePainter = outlinePainter;
            m_itemPrefab = prefab.ItemObject;
            m_itemType = prefab.ItemType;
            m_createPoint = GetScreenMiddlePoint;
        }
    
        public override void Execute()
        {
            m_lastList.Clear();
            m_lastList.AddRange(m_targetList);
            m_item = ObjectPool.Instance.OnTake(m_item,m_itemPrefab);
            m_item.transform.rotation = Quaternion.identity;
            m_item.transform.position = m_createPoint;
            m_targetList.Clear();
            m_targetList.Add(m_item);
            m_outlinePainter.SetTargetObj = m_targetList;
        }

        public override void Undo()
        {
            m_targetList.Clear();
            m_targetList.AddRange(m_lastList);
            m_outlinePainter.SetTargetObj = m_targetList;
            ObjectPool.Instance.OnRelease(m_item);
        }
    }

}