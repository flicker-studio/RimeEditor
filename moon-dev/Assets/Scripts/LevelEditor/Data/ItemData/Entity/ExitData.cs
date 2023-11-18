using Item;

namespace LevelEditor
{
    public class ExitData : ItemData
    {
        public ExitData(ItemProduct itemProduct) : base(itemProduct)
        {
        }

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);
            if (active)
            {
                GetItemObjPlay.GetComponent<ExitPlay>().enterAction += () =>
                {
                    LevelPlay.Instance.NextLevel();
                } ;
            }
        }
    }
}
