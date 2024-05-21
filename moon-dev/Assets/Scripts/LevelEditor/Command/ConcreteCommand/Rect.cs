using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor.Command
{
    public class Rect : ICommand
    {
        private readonly List<Item>    m_itemDatas    = new();
        private readonly List<Vector3> m_lastScale    = new();
        private readonly List<Vector3> m_nextScale    = new();
        private readonly List<Vector3> m_lastPosition = new();
        private readonly List<Vector3> m_nextPosition = new();
        
        public Rect(List<Item>    itemDatas, List<Vector3> lastPosition, List<Vector3> nextPosition, List<Vector3> lastScale,
                    List<Vector3> nextScale)
        {
            m_itemDatas.AddRange(itemDatas);
            m_lastScale.AddRange(lastScale);
            m_nextScale.AddRange(nextScale);
            m_lastPosition.AddRange(lastPosition);
            m_nextPosition.AddRange(nextPosition);
        }

        public void Execute()
        {
            for (var i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GameObject.transform.position   = m_nextPosition[i];
                m_itemDatas[i].GameObject.transform.localScale = m_nextScale[i];
            }
        }

        public void Undo()
        {
            for (var i = 0; i < m_itemDatas.Count; i++)
            {
                m_itemDatas[i].GameObject.transform.position   = m_lastPosition[i];
                m_itemDatas[i].GameObject.transform.localScale = m_lastScale[i];
            }
        }
    }
}