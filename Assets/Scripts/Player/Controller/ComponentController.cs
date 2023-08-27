using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ComponentController
{
    public Rigidbody2D Rigidbody;
    public CapsuleCollider2D CapsuleCollider;
    
    public ComponentController(Transform player)
    {
        Rigidbody = player.GetComponent<Rigidbody2D>();
        CapsuleCollider = player.GetComponent<CapsuleCollider2D>();
    }
}
