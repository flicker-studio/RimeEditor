using System.Collections.Generic;
using Moon.Kernel.Utils;

namespace LevelEditor
{
    public class ItemCreateCommand : ICommand
    {
        private ItemProduct m_itemProduct;

        private ItemDataBase m_itemData;

        private readonly ObservableList<ItemDataBase> m_targetAssets;

        private readonly ObservableList<ItemDataBase> m_itemAssets;

        private OutlinePainter m_outlinePainter;

        private readonly List<ItemDataBase> m_lastAssets = new();

        private ItemFactory m_itemFactory = new ItemFactory();

        public ItemCreateCommand(ObservableList<ItemDataBase> targetAssets, ObservableList<ItemDataBase> itemAssets, OutlinePainter outlinePainter, ItemProduct itemProduct)
        {
            m_targetAssets = targetAssets;
            m_itemAssets = itemAssets;
            m_outlinePainter = outlinePainter;
            m_itemProduct = itemProduct;
        }

        public void Execute()
        {
            if (m_itemData == null)
            {
                m_itemData = m_itemFactory.CreateItem(m_itemProduct);
            }
            else
            {
                m_itemData.SetActiveEditor(true);
            }

            m_lastAssets.Clear();
            m_lastAssets.AddRange(m_targetAssets);
            m_targetAssets.Clear();
            m_targetAssets.Add(m_itemData);
            m_itemAssets.Add(m_itemData);
            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
        }

        public void Undo()
        {
            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_lastAssets);
            m_itemAssets.Remove(m_itemData);
            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
            m_itemData.SetActiveEditor(false);
        }
    }
}