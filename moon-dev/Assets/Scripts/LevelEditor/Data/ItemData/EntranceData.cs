using Item;

namespace LevelEditor
{
    /// <summary>
    ///     Ingress data, which is used to initialize the role location
    /// </summary>
    public class EntranceData : ItemDataBase
    {
        public override ItemDataType ItemDataType => ItemDataType.ENTRANCE;

        public override ItemDataBase Copy(ItemDataBase saveData)
        {
            return saveData;
        }

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