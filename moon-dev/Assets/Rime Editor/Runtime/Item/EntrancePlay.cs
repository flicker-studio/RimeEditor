using Cinemachine;
using UnityEngine;

namespace Item
{
    public class EntrancePlay : ItemPlay
    {
        private GameObject m_camera;
        private Collider2D m_collider2D;

        private GameObject m_player;

        private GameObject m_slicer;

        private CinemachineVirtualCamera m_virtualCamera;

        private async void Start()
        {
        }

        public override void Play()
        {
            m_virtualCamera = m_camera.GetComponent<CinemachineVirtualCamera>();

            m_virtualCamera.m_Follow = m_player.transform;
            m_virtualCamera.m_LookAt = m_player.transform;

            m_player.transform.position = transform.position;
            m_collider2D.isTrigger      = true;
        }

        public override void Stop()
        {
            Destroy(m_player);
            Destroy(m_camera);
            Destroy(m_slicer);

            m_collider2D.isTrigger = false;
        }
    }
}