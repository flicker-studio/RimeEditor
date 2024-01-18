using Item;

namespace LevelEditor
{
    public class ExitData : ItemData
    {

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

        public ExitData(ItemProduct itemProduct, bool fromJson = false) : base(itemProduct, fromJson)
        {
        }
    }
}
