using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class ItemScaleCommand : LevelEditCommand
    {
        private List<ItemData> m_itemDatas = new List<ItemData>();

        private List<Vector3> m_lastScale = new List<Vector3>();

        private List<Vector3> m_nextScale = new List<Vector3>();

        private List<Vector3> m_lastPosition = new List<Vector3>();

        private List<Vector3> m_nextPosition = new List<Vector3>();

        public ItemScaleCommand(ObservableList<ItemData> itemDatas, List<Vector3> lastPosition, List<Vector3> nextPosition, List<Vector3> lastScale, List<Vector3> nextScale)
        {
            m_itemDatas.AddRange(itemDatas);
            m_lastScale.AddRange(lastScale);
            m_nextScale.AddRange(nextScale);
            m_lastPosition.AddRange(lastPosition);
            m_nextPosition.AddRange(nextPosition);
        }

        public override void Execute()
        {
            for (int i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GetItemObjEditor.transform.position = m_nextPosition[i];
                m_itemDatas[i].GetItemObjEditor.transform.localScale = m_nextScale[i];
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GetItemObjEditor.transform.position = m_lastPosition[i];
                m_itemDatas[i].GetItemObjEditor.transform.localScale = m_lastScale[i];
            }
        }
    }
}