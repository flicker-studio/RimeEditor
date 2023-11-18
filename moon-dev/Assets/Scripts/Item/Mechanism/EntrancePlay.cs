using Cinemachine;
using Data.ScriptableObject;
using Frame.Tool.Pool;
using UnityEngine;

namespace Item
{
    public class EntrancePlay : ItemPlay
    {
        private PrefabFactory m_prefabFactory;
        private Collider2D m_collider2D;
        private GameObject m_player;
        private GameObject m_camera;

        private void Start()
        {
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_collider2D = GetComponent<Collider2D>();
        }

        public override void Play()
        {
            m_player = ObjectPool.Instance.OnTake(m_player,m_prefabFactory.PLAYER);
            m_camera = ObjectPool.Instance.OnTake(m_player,m_prefabFactory.PLAYER_CAMERA);
            CinemachineVirtualCamera camera = m_camera.GetComponent<CinemachineVirtualCamera>();
            camera.m_Follow = m_player.transform;
            camera.m_LookAt = m_player.transform;
            m_player.transform.position = transform.position;
            m_collider2D.isTrigger = true;
        }

        public override void Stop()
        {
            ObjectPool.Instance.OnRelease(m_player);
            ObjectPool.Instance.OnRelease(m_camera);
            m_collider2D.isTrigger = false;
        }
    }
}
