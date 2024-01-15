using Slicer;
using UnityEngine;

public class MeshTest : MonoBehaviour
{
    public PolygonCollider2D Target;
    private void Start()
    {
        PolygonCollider2D polygonCollider2D = GetComponent<PolygonCollider2D>();
        
        polygonCollider2D.CreatePolygonCollider(Target,new Plane(transform.up,transform.position));
    }
}