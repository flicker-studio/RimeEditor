using UnityEngine;

namespace Sample
{
    public class DrawFrustumCorner : MonoBehaviour
    {
        public float baseHeight;

        public GameObject leftUp;
        public GameObject leftDown;
        public GameObject rightUp;
        public GameObject rightDown;

        private void Update()
        {
            var planes = GeometryUtility.CalculateFrustumPlanes(Camera.main);

            var leftUpPosition = GetPlaneIntersention(planes[0], planes[3]);
            var rightUpPosition = GetPlaneIntersention(planes[1], planes[3]);
            var leftDownPosition = GetPlaneIntersention(planes[0], planes[2]);
            var rightDownPosition = GetPlaneIntersention(planes[1], planes[2]);

            leftUp.transform.position = new Vector3(leftUpPosition.x, baseHeight, leftUpPosition.y);
            rightUp.transform.position = new Vector3(rightUpPosition.x, baseHeight, rightUpPosition.y);
            leftDown.transform.position = new Vector3(leftDownPosition.x, baseHeight, leftDownPosition.y);
            rightDown.transform.position = new Vector3(rightDownPosition.x, baseHeight, rightDownPosition.y);
        }

        private Vector2 GetPlaneIntersention(Plane plane0, Plane plane1)
        {
            var e = new Vector3(plane0.normal.y * plane1.normal.z - plane0.normal.z * plane1.normal.y,
                plane0.normal.z * plane1.normal.x - plane0.normal.x * plane1.normal.z,
                plane0.normal.x * plane1.normal.y - plane0.normal.y * plane1.normal.x);
            var d0 =
                -Mathf.Pow(
                    plane0.normal.x * plane0.normal.x + plane0.normal.y * plane0.normal.y +
                    plane0.normal.z * plane0.normal.z, 0.5f) * plane0.distance;
            var d1 =
                -Mathf.Pow(
                    plane1.normal.x * plane1.normal.x + plane1.normal.y * plane1.normal.y +
                    plane1.normal.z * plane1.normal.z, 0.5f) * plane1.distance;

            Vector3 A;
            if (e.z != 0)
            {
                A = new Vector3((d0 * plane1.normal.y - d1 * plane0.normal.y) / e.z,
                    (d0 * plane1.normal.x - d1 * plane0.normal.x) / (-e.z), 0);
            }
            else if (e.y != 0)
            {
                A = new Vector3((d0 * plane1.normal.z - d1 * plane0.normal.z) / (-e.y), 0,
                    (d0 * plane1.normal.x - d1 * plane0.normal.x) / e.y);
            }
            else if (e.x != 0)
            {
                A = new Vector3(0, (d0 * plane1.normal.z - d1 * plane0.normal.z) / e.x,
                    (d0 * plane1.normal.y - d1 * plane0.normal.y) / (-e.x));
            }
            else
            {
                A = Vector3.positiveInfinity;
            }

            var t = (baseHeight - A.y) / e.y;

            return new Vector2(A.x + t * e.x, A.z + t * e.z);
        }
    }
}
