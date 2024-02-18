using LevelEditor.Data;

namespace LevelEditor
{
    public class Platform : AbstractItem
    {
        public bool CanPush;

        public bool CanCopy;

        private readonly PlatformPlay _play;

        /// <summary>
        ///     Constructor of new Item
        /// </summary>
        public Platform(ItemView view = null) : base(ItemType.PLATFORM, view)
        {
            _play = GameObject.AddComponent<PlatformPlay>();
        }

        public override void SetActivePlay(bool active)
        {
            base.SetActivePlay(active);

            if (active)
            {
                _play.CanCopy = CanCopy;
                _play.CanPush = CanPush;
                _play.Play();
            }
            else
            {
                _play.Stop();
            }
        }
    }
}