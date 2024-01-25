using UnityEngine;

namespace Struct
{
    /// <summary>
    ///     Store a flat triangle.
    /// </summary>
    public struct TrianglePoints
    {
        /// <summary>
        ///     The position of one point of the triangle
        /// </summary>
        public Vector2 PointX;

        /// <inheritdoc cref="PointX" />
        public Vector2 PointY;

        /// <inheritdoc cref="PointX" />
        public Vector2 PointZ;

        /// <summary>
        ///     Initialize the struct.
        /// </summary>
        /// <param name="pointX">The position of one point of the triangle.</param>
        /// <param name="pointY">The position of one point of the triangle.</param>
        /// <param name="pointZ">The position of one point of the triangle.</param>
        public TrianglePoints(Vector2 pointX, Vector2 pointY, Vector2 pointZ)
        {
            PointX = pointX;
            PointY = pointY;
            PointZ = pointZ;
        }

        /// <summary>
        ///     Determines whether exist a point which position is equal another.
        /// </summary>
        /// <returns>Returns true when three points cannot form a triangle</returns>
        public bool Judge()
        {
            return PointX == PointY || PointX == PointZ || PointY == PointZ;
        }

        /// <inheritdoc />
        public override bool Equals(object obj)
        {
            //TODO:Use value tuples
            if (obj is TrianglePoints other)
                return (PointX == other.PointX && PointY == other.PointY && PointZ == other.PointZ) ||
                       (PointX == other.PointY && PointY == other.PointX && PointZ == other.PointZ) ||
                       (PointX == other.PointZ && PointY == other.PointY && PointZ == other.PointX) ||
                       (PointX == other.PointX && PointY == other.PointZ && PointZ == other.PointY) ||
                       (PointX == other.PointY && PointY == other.PointZ && PointZ == other.PointX) ||
                       (PointX == other.PointZ && PointY == other.PointX && PointZ == other.PointY);

            return false;
        }

        /// <inheritdoc />
        public override int GetHashCode()
        {
            return PointX.GetHashCode() ^ PointY.GetHashCode() ^ PointZ.GetHashCode();
        }
    }
}