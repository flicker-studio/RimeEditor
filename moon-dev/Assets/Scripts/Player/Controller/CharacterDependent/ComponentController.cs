using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character.Information
{
    public class ComponentController
    {
        public Rigidbody2D Rigidbody;
        public Collider2D Collider;
    
        public ComponentController(Transform player)
        {
            Rigidbody = player.GetComponent<Rigidbody2D>();
            Collider = player.GetComponent<Collider2D>();
            Rigidbody.sharedMaterial = Resources.Load<PhysicsMaterial2D>("PhysicsMaterials/Player");
            Rigidbody.freezeRotation = true;
            Collider.sharedMaterial = Resources.Load<PhysicsMaterial2D>("PhysicsMaterials/Player");
        }
    }
}
