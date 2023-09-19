using System.Collections.Generic;
using UnityEngine;

public class TestOverlapCollider : MonoBehaviour
{
    public List<Collider2D> m_targetCollider = new List<Collider2D>();

    void Update()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        GetComponent<Collider2D>().OverlapCollider(contactFilter2D,m_targetCollider);
    }
}
