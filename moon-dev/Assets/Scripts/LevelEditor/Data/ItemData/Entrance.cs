using Item;
using LevelEditor.Data;

namespace LevelEditor
{
    /// <summary>
    ///     Ingress data, which is used to initialize the role location
    /// </summary>
    public class Entrance : AbstractItem
    {
        private readonly EntrancePlay _play;

        public Entrance(ItemView view) : base(ItemType.ENTRANCE, view)
        {
            _play = GameObject.AddComponent<EntrancePlay>();
        }

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);

            if (active)
                _play.Play();
            else
                _play.Stop();
        }
    }
}