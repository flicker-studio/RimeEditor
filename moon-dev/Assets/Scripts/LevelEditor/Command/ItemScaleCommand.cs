using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class ItemScaleCommand : Command
    {
        private List<GameObject> m_gameObjects = new List<GameObject>();
        private List<Vector3> m_lastScale = new List<Vector3>();
        private List<Vector3> m_nextScale = new List<Vector3>();
    
        public ItemScaleCommand(List<GameObject> gameObjects,List<Vector3> lastScale,List<Vector3> nextScale)
        {
            m_gameObjects.AddRange(gameObjects);
            m_lastScale.AddRange(lastScale);
            m_nextScale.AddRange(nextScale);
        }
        public override void Execute()
        {
            for (int i = 0; i < m_gameObjects.Count; i++)
            {
                m_gameObjects[i].transform.localScale = m_nextScale[i];
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < m_gameObjects.Count; i++)
            {
                m_gameObjects[i].transform.localScale = m_lastScale[i];
            }
        }
    }

}