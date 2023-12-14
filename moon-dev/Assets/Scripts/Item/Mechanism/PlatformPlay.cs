using Frame.Static.Global;
using Frame.Tool.Pool;
using Data.ScriptableObject;
using Frame.Static.Global;
using Frame.Tool.Pool;
using Item;
using UnityEngine;

public class PlatformPlay : ItemPlay
{
    public bool CanCopy;
    public bool CanPush;
    private GameObject m_rigidbodyParent;
    private GameObject m_rigidbodyPrefab;

    private void Start()
    {
        PrefabFactory m_prefabFactory = Resources.Load<PrefabFactory>("GlobalSettings/PrefabFactory");
        m_rigidbodyPrefab = m_prefabFactory.RIGIDBODY_PARENT;
    }

    public bool CanCopy;
    public bool CanPush;
    private GameObject m_rigidbodyParent;
    private Renderer m_renderer;
    private Material m_originMaterial;
    [SerializeField] private GameObject m_rigidbodyPrefab;
    [SerializeField] private Material m_canCopymaterial;

    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_originMaterial = m_renderer.material;
    }

    public override void Play()
    {

    }

    public override void Stop()
    {

    }
}
