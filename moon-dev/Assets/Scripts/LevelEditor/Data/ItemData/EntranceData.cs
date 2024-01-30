using Item;

namespace LevelEditor
{
    public class EntranceData : ItemData
    {
        public override ItemData Copy(ItemData saveData)
        {
            return saveData;
        }

        public override ItemDataType ItemDataType => ItemDataType.Entrance;

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);

            if (active)
            {
                m_itemObjPlay.GetComponent<ItemPlay>().Play();
            }
            else
            {
                m_itemObjPlay.GetComponent<ItemPlay>().Stop();
            }
        }

        public EntranceData(ItemProduct itemProduct, bool fromJson = false) : base(itemProduct, fromJson)
        {
        }
    }
}