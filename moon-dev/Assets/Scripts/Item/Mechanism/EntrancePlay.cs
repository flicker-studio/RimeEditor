using Cinemachine;
using Data.ScriptableObject;
using Frame.Tool.Pool;
using Slicer;
using UnityEngine;

namespace Item
{
    public class EntrancePlay : ItemPlay
    {
        private PrefabFactory m_prefabFactory;
        private Collider2D m_collider2D;
        private GameObject m_player;
        private GameObject m_camera;
        private GameObject m_slicer;
        private CinemachineVirtualCamera camera;
        private SlicerController slicerController;

        private void Start()
        {
            m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
            m_collider2D = GetComponent<Collider2D>();
        }

        public override void Play()
        {
            m_player = ObjectPool.Instance.OnTake(m_player,m_prefabFactory.PLAYER);
            m_camera = ObjectPool.Instance.OnTake(m_camera,m_prefabFactory.PLAYER_CAMERA);
            m_slicer = ObjectPool.Instance.OnTake(m_slicer, m_prefabFactory.SLICER);
            camera = m_camera.GetComponent<CinemachineVirtualCamera>();
            slicerController = m_slicer.GetComponent<SlicerController>();
            camera.m_Follow = m_player.transform;
            camera.m_LookAt = m_player.transform;
            slicerController.PlayerTransform = m_player.transform;
            slicerController.ResetCopy();
            m_player.transform.position = transform.position;
            m_collider2D.isTrigger = true;
        }

        public override void Stop()
        {
            ObjectPool.Instance.OnRelease(m_player);
            ObjectPool.Instance.OnRelease(m_camera);
            ObjectPool.Instance.OnRelease(m_slicer);
            slicerController.ResetCopy();
            m_collider2D.isTrigger = false;
        }
    }
}
