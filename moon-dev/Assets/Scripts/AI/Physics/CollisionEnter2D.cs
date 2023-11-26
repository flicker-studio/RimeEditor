using System;
using UnityEngine;

namespace Moon
{
    [RequireComponent(typeof(Collider2D))]
    public class CollisionEnter2D : MonoBehaviour
    {
        public event Action<Collision2D> Enter2D;
        private void OnCollisionEnter2D(Collision2D other)
        {
            Debug.Log("CollisionEnter2D");
            Enter2D?.Invoke(other);
        }
        
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = false;
        }
#endif
    }
}