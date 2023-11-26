using System;
using UnityEngine;

namespace Moon
{


    
    [RequireComponent(typeof(Collider2D))]
    public class TriggerEnter2D : MonoBehaviour
    {
        public event Action<Collider2D> Enter2D;
        private void OnTriggerEnter2D(Collider2D other)
        {
            Enter2D?.Invoke(other);
        }
#if UNITY_EDITOR
        private void OnValidate()
        {
            GetComponent<Collider2D>().isTrigger = true;
        }
#endif
    }
}