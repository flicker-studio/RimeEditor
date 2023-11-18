using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    public class ItemPositionCommand : Command
    {
        private List<ItemData> m_gameObjects = new List<ItemData>();
        private List<Vector3> m_lastPosition = new List<Vector3>();
        private List<Vector3> m_nextPosition = new List<Vector3>();
    
        public ItemPositionCommand(ObservableList<ItemData> gameObjects,List<Vector3> lastPosition,List<Vector3> nextPosition)
        {
            m_gameObjects.AddRange(gameObjects);
            m_lastPosition.AddRange(lastPosition);
            m_nextPosition.AddRange(nextPosition);
        }
        public override void Execute()
        {
            for (int i = 0; i < m_gameObjects.Count; i++)
            {
                m_gameObjects[i].GetItemObjEditor.transform.position = m_nextPosition[i];
            }
        }

        public override void Undo()
        {
            for (int i = 0; i < m_gameObjects.Count; i++)
            {
                m_gameObjects[i].GetItemObjEditor.transform.position = m_lastPosition[i];
            }
        }
    }
}
