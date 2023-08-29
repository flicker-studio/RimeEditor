using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public abstract class CompensateTimer
{
    protected float m_timer = 0f;
    
    public abstract bool CheckTimer(PlayerInformation playerInformation);
}
