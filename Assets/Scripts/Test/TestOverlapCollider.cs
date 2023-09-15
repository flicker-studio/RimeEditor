using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestOverlapCollider : MonoBehaviour
{
    public List<Collider2D> collider2D = new List<Collider2D>();
    // Update is called once per frame
    void Update()
    {
        ContactFilter2D contactFilter2D = new ContactFilter2D();
        GetComponent<Collider2D>().OverlapCollider(contactFilter2D,collider2D);
    }
}
