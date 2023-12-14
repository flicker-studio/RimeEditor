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

    public override void Play()
    {
        if (CanPush)
        {
            m_rigidbodyParent = ObjectPool.Instance.OnTake(m_rigidbodyParent,m_rigidbodyPrefab);
            m_rigidbodyParent.transform.parent = transform.parent;
            transform.parent = m_rigidbodyParent.transform;
            if (!gameObject.name.Contains(GlobalSetting.ObjNameTag.RIGIDBODY_TAG))
            {
                gameObject.name += GlobalSetting.ObjNameTag.RIGIDBODY_TAG;
            }
        }
    }

    public override void Stop()
    {
        if (CanPush)
        {
            ObjectPool.Instance.OnRelease(m_rigidbodyParent);
        }
        if (gameObject.name.Contains(GlobalSetting.ObjNameTag.RIGIDBODY_TAG))
        {
            gameObject.name = gameObject.name.Replace(GlobalSetting.ObjNameTag.RIGIDBODY_TAG, string.Empty);
        }
    }
}
