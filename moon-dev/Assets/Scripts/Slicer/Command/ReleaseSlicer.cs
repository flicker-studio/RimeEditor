using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class ReleaseSlicer : ICommand
{
    private SlicerInformation m_slicerInformation;

    private List<List<Collider2D>> m_colliderListGroup = new List<List<Collider2D>>();
    
    public ReleaseSlicer(SlicerInformation slicerInformation)
    {
        m_slicerInformation = slicerInformation;
    }

    public void Execute()
    {
        List<Collider2D> targetColliderList = m_slicerInformation.TargetList;
        foreach (var collider in targetColliderList)
        {
            collider.enabled = true;
        }
        m_colliderListGroup = targetColliderList.CheckColliderConnectivity(
            m_slicerInformation.GetDetectionCompensationScale
            , GlobalSetting.LayerMasks.GROUND);
        
        m_colliderListGroup.GetCombinationConnectivity(m_slicerInformation.GetPrefabFactory);
    }
}


