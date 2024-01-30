using System.Collections.Generic;
using Moon.Kernel.Utils;

namespace LevelEditor
{
    public class ItemSelectCommand : ICommand
    {
        private readonly ObservableList<ItemDataBase> m_targetList;

        private OutlinePainter m_outlinePainter;

        private readonly List<ItemDataBase> m_lastList = new();

        private readonly List<ItemDataBase> m_nextList = new();

        public ItemSelectCommand(ObservableList<ItemDataBase> targetList, List<ItemDataBase> nextList, OutlinePainter outlinePainter)
        {
            m_targetList = targetList;
            m_outlinePainter = outlinePainter;
            m_lastList.AddRange(targetList);
            m_nextList.AddRange(nextList);
        }

        public void Execute()
        {
            m_targetList.Clear();
            m_targetList.AddRange(m_nextList);
            m_outlinePainter.SetTargetObj = m_targetList.GetItemObjs();
        }

        public void Undo()
        {
            m_targetList.Clear();
            m_targetList.AddRange(m_lastList);
            m_outlinePainter.SetTargetObj = m_targetList.GetItemObjs();
        }
    }
}