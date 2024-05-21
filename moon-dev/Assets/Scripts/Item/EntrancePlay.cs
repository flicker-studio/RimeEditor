using Cinemachine;
using Data.ScriptableObject;
using Moon.Kernel.Service;
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
        
        private CinemachineVirtualCamera m_virtualCamera;
        
        private SlicerController m_slicerController;
        
        private async void Start()
        {
            m_prefabFactory = await ResourcesService.LoadAssetAsync<PrefabFactory>("Assets/Settings/GlobalSettings/PrefabFactory.asset");
            m_collider2D    = GetComponent<Collider2D>();
        }
        
        public override void Play()
        {
            m_player                           = Instantiate(m_prefabFactory.PLAYER);
            m_camera                           = Instantiate(m_prefabFactory.PLAYER_CAMERA);
            m_slicer                           = Instantiate(m_prefabFactory.SLICER);
            m_virtualCamera                    = m_camera.GetComponent<CinemachineVirtualCamera>();
            m_slicerController                 = m_slicer.GetComponent<SlicerController>();
            m_virtualCamera.m_Follow           = m_player.transform;
            m_virtualCamera.m_LookAt           = m_player.transform;
            m_slicerController.PlayerTransform = m_player.transform;
            m_slicerController.ResetCopy();
            m_player.transform.position = transform.position;
            m_collider2D.isTrigger      = true;
        }
        
        public override void Stop()
        {
            Destroy(m_player);
            Destroy(m_camera);
            Destroy(m_slicer);
            m_slicerController.ResetCopy();
            m_collider2D.isTrigger = false;
        }
    }
}