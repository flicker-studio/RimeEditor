using System;
using Item;
using LevelEditor.Data;

namespace LevelEditor
{
    /// <summary>
    ///     Ingress data, which is used to initialize the role location
    /// </summary>
    internal class Entrance : Item
    {
        private readonly EntrancePlay _play;
        
        internal Entrance() : base(ItemType.ENTRANCE)
        {
            _play = GameObject.AddComponent<EntrancePlay>();
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
            
            _play.Play();
        }
        
        public override void Reset()
        {
            base.Reset();
            _play.Stop();
        }
    }
}