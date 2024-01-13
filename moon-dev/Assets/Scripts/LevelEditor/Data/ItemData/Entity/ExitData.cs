using Item;

namespace LevelEditor
{
    public class ExitData : ItemData
    {
        public ExitData(ItemProduct itemProduct) : base(itemProduct)
        {
        }

        public override ItemData Copy(ItemData saveData)
        {
            return saveData;
        }

        public override ItemDataType ItemDataType => ItemDataType.Exit;

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);
            if (active)
            {
                GetItemObjPlay.GetComponent<ExitPlay>().enterAction += () =>
                {
                    LevelPlay.Instance.NextLevel();
                } ;
                m_itemObjPlay.GetComponent<ItemPlay>().Play();
            }
            else
            {
                m_itemObjPlay.GetComponent<ItemPlay>().Stop();
            }
        }
    }
}
