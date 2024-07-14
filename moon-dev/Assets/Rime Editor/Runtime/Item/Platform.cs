using System;
using LevelEditor.Data;

namespace LevelEditor.Item
{
    public class Platform : ItemBase
    {
        private readonly PlatformPlay _play;

        public bool CanCopy;
        public bool CanPush;

        /// <summary>
        ///     Constructor of new Item
        /// </summary>
        public Platform() : base(ItemType.PLATFORM)
        {
            _play = GameObject.AddComponent<PlatformPlay>();
        }

        public override void Inactive()
        {
            throw new NotImplementedException();
        }

        public override void Active(bool a = false)
        {
            throw new NotImplementedException();
        }

        public override void Preview()
        {
            base.Preview();

            _play.CanCopy = CanCopy;
            _play.CanPush = CanPush;
            _play.Play();
        }

        public override void Reset()
        {
            base.Reset();
            _play.Stop();
        }
    }
}