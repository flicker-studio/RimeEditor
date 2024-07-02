using System;
using UnityEngine;

namespace LevelEditor.Item
{
    /// <summary>
    ///     An updated script attached to the Entrance and Exit
    ///     to trigger collision detection, may be removed in the future。
    /// </summary>
    public class ItemBehaviour : MonoBehaviour
    {
        /// <summary>
        ///     The events of the base loop
        /// </summary>
        public event Action OnUpdate;
        
        /// <summary>
        ///     The event of the trigger
        /// </summary>
        public event Action<Collider2D> OnTrigger;
        
        private void Update()
        {
            OnUpdate?.Invoke();
        }
        
        private void OnTriggerEnter2D(Collider2D other)
        {
            OnTrigger?.Invoke(other);
        }
    }
}