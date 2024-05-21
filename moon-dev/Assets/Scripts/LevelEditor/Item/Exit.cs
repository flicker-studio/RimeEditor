using System;
using Item;
using LevelEditor.Data;

namespace LevelEditor
{
    /// <summary>
    ///     There can be multiple exits in a single level
    /// </summary>
    public sealed class Exit : Item
    {
        private readonly ExitPlay _play;
        
        public Exit() : base(ItemType.EXIT)
        {
            _play = GameObject.AddComponent<ExitPlay>();
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
            
            _play.enterAction += () => { LevelPlay.Instance.NextLevel(); };
            _play.Play();
        }
        
        public override void Reset()
        {
            base.Reset();
            _play.Stop();
        }
    }
}