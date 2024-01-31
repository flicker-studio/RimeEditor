using System;
using Frame.Static.Global;
using Frame.Tool.Pool;
using Item;
using UnityEngine;

public class PlatformPlay : ItemPlay
{
    public bool CanCopy;
    public bool CanPush;
    private GameObject m_rigidbodyParent;
    private Renderer m_renderer;
    private Material m_originMaterial;
    [SerializeField]private GameObject m_rigidbodyPrefab;
    [SerializeField]private Material m_canCopymaterial;

    private void Start()
    {
        m_renderer = GetComponent<Renderer>();
        m_originMaterial = m_renderer.material;
    }

    public override void Play()
    {
        //TODO:需加载SO
        throw new Exception("需加载SO");

        // if (CanPush)
        // {
        //     m_rigidbodyParent = ObjectPool.Instance.OnTake(m_rigidbodyParent,m_rigidbodyPrefab);
        //     m_rigidbodyParent.transform.parent = transform.parent;
        //     transform.parent = m_rigidbodyParent.transform;
        //     if (!gameObject.name.Contains(GlobalSetting.ObjNameTag.RIGIDBODY_TAG))
        //     {
        //         gameObject.name += GlobalSetting.ObjNameTag.RIGIDBODY_TAG;
        //     }
        // }
        //
        // if (CanCopy)
        // {
        //     if (!gameObject.name.Contains(GlobalSetting.ObjNameTag.CAN_COPY_TAG))
        //     {
        //         gameObject.name += GlobalSetting.ObjNameTag.CAN_COPY_TAG;
        //     }
        //
        //     m_renderer.material = m_canCopymaterial;
        // }
    }

    public override void Stop()
    {
        //TODO:需加载SO
        throw new Exception("需加载SO");
        // if (CanPush)
        // {
        //     ObjectPool.Instance.OnRelease(m_rigidbodyParent);
        // }
        // if (gameObject.name.Contains(GlobalSetting.ObjNameTag.RIGIDBODY_TAG))
        // {
        //     gameObject.name = gameObject.name.Replace(GlobalSetting.ObjNameTag.RIGIDBODY_TAG, string.Empty);
        // }
        // if (gameObject.name.Contains(GlobalSetting.ObjNameTag.CAN_COPY_TAG))
        // {
        //     gameObject.name = gameObject.name.Replace(GlobalSetting.ObjNameTag.CAN_COPY_TAG, string.Empty);
        // }
        //
        // m_renderer.material = m_originMaterial;
    }
}
