using Item;
using LevelEditor.Data;

namespace LevelEditor
{
    public class Exit : AbstractItem
    {
        private readonly ExitPlay _play;

        public Exit(ItemView view) : base(ItemType.EXIT, view)
        {
            _play = GameObject.AddComponent<ExitPlay>();
        }

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);

            if (active)
            {
                _play.enterAction += () => { LevelPlay.Instance.NextLevel(); };
                _play.Play();
            }
            else
            {
                _play.Stop();
            }
        }
    }
}