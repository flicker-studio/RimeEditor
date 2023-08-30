using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastMethod
{
    public static List<Vector2> CastRaysBetweenPoints(Vector2 startPoint, Vector2 endPoint, int rayCount, LayerMask layerMask)
    {
        List<Vector2> hitPoints = new List<Vector2>();

        Vector2 direction = endPoint - startPoint;
        float distance = direction.magnitude;
        float step = distance / (rayCount - 1);
        direction.Normalize();

        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = startPoint + i * step * direction;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin, direction, distance, layerMask);
            if (hit.collider != null)
            {
                hitPoints.Add(hit.point);
            }
        }

        return hitPoints;
    }
}
