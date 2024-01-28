using System.Collections.Generic;
using Moon.Kernel.Utils;

namespace LevelEditor
{
    public class ItemDeleteCommand : ICommand
    {
        private ObservableList<ItemData> m_targetAssets;

        private ObservableList<ItemData> m_itemAssets;

        private OutlinePainter m_outlinePainter;

        private List<ItemData> m_lastAssets = new List<ItemData>();

        public ItemDeleteCommand(ObservableList<ItemData> targetAssets, ObservableList<ItemData> itemAssets, OutlinePainter outlinePainter)
        {
            m_targetAssets = targetAssets;
            m_itemAssets = itemAssets;
            m_outlinePainter = outlinePainter;
        }

        public void Execute()
        {
            foreach (var targetAsset in m_targetAssets)
            {
                targetAsset.SetActiveEditor(false);
            }

            m_lastAssets.Clear();
            m_lastAssets.AddRange(m_targetAssets);

            m_itemAssets.RemoveAll(m_lastAssets);
            m_targetAssets.Clear();

            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
        }

        public void Undo()
        {
            foreach (var lastAsset in m_lastAssets)
            {
                lastAsset.SetActiveEditor(true);
            }

            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_lastAssets);
            m_itemAssets.AddRange(m_lastAssets);
            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
        }
    }
}