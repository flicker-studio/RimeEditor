using System;
using UnityEngine;

namespace Item
{
    public class ExitPlay : ItemPlay
    {
        private Collider2D m_collider2D;

        private void Start()
        {
            m_collider2D = GetComponent<Collider2D>();
        }

        private void OnTriggerEnter2D(Collider2D other)
        {
            if (other.CompareTag("Player")) enterAction?.Invoke();
        }

        public event Action enterAction;

        public override void Play()
        {
            m_collider2D.isTrigger = true;
        }

        public override void Stop()
        {
            m_collider2D.isTrigger = false;
            enterAction            = null;
        }
    }
}