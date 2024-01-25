using System.Collections.Generic;

namespace LevelEditor
{
    public class ItemCreateCommand : LevelEditCommand
    {
        private ItemProduct m_itemProduct;

        private ItemData m_itemData;

        private ObservableList<ItemData> m_targetAssets;

        private ObservableList<ItemData> m_itemAssets;

        private OutlinePainter m_outlinePainter;

        private List<ItemData> m_lastAssets = new List<ItemData>();

        private ItemFactory m_itemFactory = new ItemFactory();

        public ItemCreateCommand(ObservableList<ItemData> targetAssets, ObservableList<ItemData> itemAssets, OutlinePainter outlinePainter, ItemProduct itemProduct)
        {
            m_targetAssets = targetAssets;
            m_itemAssets = itemAssets;
            m_outlinePainter = outlinePainter;
            m_itemProduct = itemProduct;
        }

        public override void Execute()
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

        public override void Undo()
        {
            m_targetAssets.Clear();
            m_targetAssets.AddRange(m_lastAssets);
            m_itemAssets.Remove(m_itemData);
            m_outlinePainter.SetTargetObj = m_targetAssets.GetItemObjs();
            m_itemData.SetActiveEditor(false);
        }
    }
}