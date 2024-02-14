using System.Collections.Generic;
using UnityEngine;

namespace LevelEditor
{
    /// <inheritdoc />
    /// <summary>
    ///     Commands to manipulate the scaling of items
    /// </summary>
    public class ItemScaleCommand : ICommand
    {
        private readonly List<ItemDataBase> m_items = new();

        private readonly List<Vector3> m_lastScale = new();

        private readonly List<Vector3> m_nextScale = new();

        private readonly List<Vector3> m_lastPosition = new();

        private readonly List<Vector3> m_nextPosition = new();

        private readonly GameObject m_item;

        private readonly Vector3 m_newPos;

        private readonly Vector3 m_newScale;

        private readonly Vector3 m_oldPos;

        private readonly Vector3 m_oldScale;

        public ItemScaleCommand(ObservableList<ItemDataBase> items, List<Vector3> lastPosition, List<Vector3> nextPosition, List<Vector3> lastScale, List<Vector3> nextScale)
        {
            m_items.AddRange(items);
            m_lastScale.AddRange(lastScale);
            m_nextScale.AddRange(nextScale);
            m_lastPosition.AddRange(lastPosition);
            m_nextPosition.AddRange(nextPosition);
        }

        public ItemScaleCommand
        (
            GameObject item,
            Vector3    newPos,
            Vector3    newScale
        )
        {
            m_item     = item;
            m_oldPos   = item.transform.position;
            m_newPos   = newPos;
            m_oldScale = item.transform.localScale;
            m_newScale = newScale;
        }

        /// <inheritdoc />
        public void Execute()
        {
            m_item.transform.position   = m_newPos;
            m_item.transform.localScale = m_newScale;

            // for (var i = 0; i < m_items.Count; i++)
            // {
            //     m_items[i].GetItemObjEditor.transform.position   = m_nextPosition[i];
            //     m_items[i].GetItemObjEditor.transform.localScale = m_nextScale[i];
            // }
        }

        /// <inheritdoc />
        public void Undo()
        {
            m_item.transform.position   = m_oldPos;
            m_item.transform.localScale = m_oldScale;

            // for (var i = 0; i < m_items.Count; i++)
            // {
            //     m_items[i].GetItemObjEditor.transform.position   = m_lastPosition[i];
            //     m_items[i].GetItemObjEditor.transform.localScale = m_lastScale[i];
            // }
        }
    }
}