using System.Collections.Generic;
using UnityEngine;
using Frame.Tool;
namespace LevelEditor
{
    public class ItemScaleCommand : LevelEditCommand
    {
        private List<ItemData> m_itemDatas = new List<ItemData>();
        private List<Vector3> m_lastScale = new List<Vector3>();
        private List<Vector3> m_nextScale = new List<Vector3>();
    
        public ItemScaleCommand(ObservableList<ItemData> itemDatas,List<Vector3> lastScale,List<Vector3> nextScale)
        {
            m_itemDatas.AddRange(itemDatas);
            m_lastScale.AddRange(lastScale);
            m_nextScale.AddRange(nextScale);
        }
        public override void Execute()
        {
            for (int i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GetItemObjEditor.transform.localScale = m_nextScale[i];
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GetItemObjEditor.transform.localScale = m_lastScale[i];
            }
        }
    }

}