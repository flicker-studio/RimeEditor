using LevelEditor.Data;
using UnityEngine;

namespace LevelEditor.Item
{
    /// <summary>
    ///     There can be multiple exits in a single level
    /// </summary>
    public sealed class Exit : ItemBase
    {
        private readonly ItemBehaviour _behaviour;
        private readonly Collider2D    _collider2D;

        /// <summary>
        ///     The default constructor creates a brand new exit-item
        /// </summary>
        public Exit() : base(ItemType.EXIT)
        {
            _behaviour  = GameObject.AddComponent<ItemBehaviour>();
            _collider2D = GameObject.AddComponent<Collider2D>();

            _collider2D.isTrigger = true;
            _behaviour.OnTrigger += collider2D =>
            {
                if (collider2D.CompareTag("Player")) LevelPlay.Instance.NextLevel();
            };
        }

        public Exit(Exit exitItem) : base(ItemType.EXIT)
        {
            _behaviour = exitItem._behaviour;
        }

        public override void Preview()
        {
            base.Preview();
            _behaviour.OnTrigger += collider2D =>
            {
                if (collider2D.CompareTag("Player")) LevelPlay.Instance.NextLevel();
            };
        }

        public override void Reset()
        {
            base.Reset();
            _behaviour.OnTrigger -= collider2D =>
            {
                if (collider2D.CompareTag("Player")) LevelPlay.Instance.NextLevel();
            };
        }
    }
}