using Item;

namespace LevelEditor
{
    public class PlatformData : ItemData
    {
        public bool CanPush;

        public bool CanCopy;

        public override ItemData Copy(ItemData saveData)
        {
            PlatformData tempData = saveData as PlatformData;
            tempData.CanPush = CanPush;
            tempData.CanCopy = CanCopy;
            return saveData;
        }

        public override ItemDataType ItemDataType => ItemDataType.Platform;

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);

            if (active)
            {
                PlatformPlay platformPlay = m_itemObjPlay.GetComponent<PlatformPlay>();
                platformPlay.CanCopy = CanCopy;
                platformPlay.CanPush = CanPush;
                m_itemObjPlay.GetComponent<ItemPlay>().Play();
            }
            else
            {
                m_itemObjPlay.GetComponent<ItemPlay>().Stop();
            }
        }

        public PlatformData(ItemProduct itemProduct, bool fromJson = false) : base(itemProduct, fromJson)
        {
        }
    }
}