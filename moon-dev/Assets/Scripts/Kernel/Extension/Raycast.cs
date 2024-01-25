using System.Collections.Generic;
using UnityEngine;

namespace Moon.Kernel.Extension
{
    public static class Raycast
    {
        public static List<Vector2> CastRaysBetweenPoints(Vector2 startPoint, Vector2 endPoint, int rayCount,
            Vector2 startPointCompensation,
            Vector2 direction, float distance, float angle, UnityEngine.LayerMask layerMask)
        {
            var hitPoints = new List<Vector2>();

            var interval = endPoint - startPoint;
            var step = (endPoint - startPoint).magnitude / (rayCount - 1);
            interval.Normalize();

            for (var i = 0; i < rayCount; i++)
            {
                var rayOrigin = startPoint + i * step * interval;
                var hit = Physics2D.Raycast(rayOrigin + startPointCompensation, direction, distance, layerMask);
                Debug.DrawRay(rayOrigin + startPointCompensation, direction, Color.red);

                if (hit.collider != null)
                {
                    if (hit.normal.y > 0 && hit.normal.y > Mathf.Cos(angle * Mathf.Deg2Rad))
                    {
                        hitPoints.Add(hit.point);
                        Debug.DrawRay(hit.point, hit.normal, Color.green);
                    }
                }
            }

            return hitPoints;
        }

        public static List<Vector2> CastRaysBetweenPoints(Vector2 startPoint, Vector2 endPoint, int rayCount,
            Vector2 startPointCompensation,
            Vector2 direction, float distance, UnityEngine.LayerMask layerMask)
        {
            var hitPoints = new List<Vector2>();

            var interval = endPoint - startPoint;
            var step = (endPoint - startPoint).magnitude / (rayCount - 1);
            interval.Normalize();

            for (var i = 0; i < rayCount; i++)
            {
                var rayOrigin = startPoint + i * step * interval;
                var hit = Physics2D.Raycast(rayOrigin + startPointCompensation, direction, distance, layerMask);
                Debug.DrawRay(rayOrigin + startPointCompensation, direction, Color.red);

                if (hit.collider != null)
                {
                    if (hit.normal.y > 0)
                    {
                        hitPoints.Add(hit.point);
                        Debug.DrawRay(hit.point, hit.normal, Color.green);
                    }
                }
            }

            return hitPoints;
        }
    }
}