using UnityEngine;

namespace Item
{
    public abstract class ItemPlay : MonoBehaviour
    {
        protected string m_name;

        protected virtual void Awake()
        {
            m_name = gameObject.name;
        }

        public abstract void Play();

        public abstract void Stop();
    }
}