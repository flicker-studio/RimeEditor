using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestConnectivity : MonoBehaviour
{
    public Collider2D m_targetCollider;
    public List<Collider2D> m_connectCollider = new List<Collider2D>();
    
    void FixedUpdate()
    {
        m_connectCollider = m_targetCollider.CheckColliderConnectivity();
    }
    
}
