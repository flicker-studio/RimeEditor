using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class ItemRotationCommand : LevelEditCommand
    {
        private List<ItemData> m_itemDatas = new List<ItemData>();

        private List<Quaternion> m_lastRotation = new List<Quaternion>();

        private List<Quaternion> m_nextRotation = new List<Quaternion>();

        private List<Vector3> m_lastPosition = new List<Vector3>();

        private List<Vector3> m_nextPosition = new List<Vector3>();

        public ItemRotationCommand(ObservableList<ItemData> itemDatas, List<Vector3> lastPosition, List<Vector3> nextPosition,
            List<Quaternion> lastRotation, List<Quaternion> nextRotation)
        {
            m_itemDatas.AddRange(itemDatas);
            m_lastPosition.AddRange(lastPosition);
            m_nextPosition.AddRange(nextPosition);
            m_lastRotation.AddRange(lastRotation);
            m_nextRotation.AddRange(nextRotation);
        }

        public override void Execute()
        {
            for (int i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GetItemObjEditor.transform.position = m_nextPosition[i];
                m_itemDatas[i].GetItemObjEditor.transform.rotation = m_nextRotation[i];
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GetItemObjEditor.transform.position = m_lastPosition[i];
                m_itemDatas[i].GetItemObjEditor.transform.rotation = m_lastRotation[i];
            }
        }
    }
}