using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class RaycastMethod
{
    public static List<Vector2> CastRaysBetweenPoints(Vector2 startPoint, Vector2 endPoint, int rayCount,
                                                        Vector2 startPointCompensation,
                                                        Vector2 direction,float distance, LayerMask layerMask)
    {
        List<Vector2> hitPoints = new List<Vector2>();

        Vector2 interval = endPoint - startPoint;
        float step = (endPoint - startPoint).magnitude / (rayCount - 1);
        interval.Normalize();

        for (int i = 0; i < rayCount; i++)
        {
            Vector2 rayOrigin = startPoint + i * step * interval;
            RaycastHit2D hit = Physics2D.Raycast(rayOrigin + startPointCompensation, direction, distance, layerMask);
            Debug.DrawRay(rayOrigin + startPointCompensation,direction);
            if (hit.collider != null)
            {
                hitPoints.Add(hit.point);
            }
        }

        return hitPoints;
    }
}
