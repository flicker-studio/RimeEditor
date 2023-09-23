using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Struct
{
    public struct TrianglePoints
    {
        public Vector2 PointX;
        public Vector2 PointY;
        public Vector2 PointZ;

        public TrianglePoints(Vector2 pointX,Vector2 pointY,Vector2 pointZ)
        {
            PointX = pointX;
            PointY = pointY;
            PointZ = pointZ;
        }
        public override bool Equals(object obj)
        {
            if (obj is TrianglePoints)
            {
                TrianglePoints other = (TrianglePoints)obj;
                return PointX == other.PointX && PointY == other.PointY && PointZ == other.PointZ ||
                       PointX == other.PointY && PointY == other.PointX && PointZ == other.PointZ ||
                       PointX == other.PointZ && PointY == other.PointY && PointZ == other.PointX ||
                       PointX == other.PointX && PointY == other.PointZ && PointZ == other.PointY ||
                       PointX == other.PointY && PointY == other.PointZ && PointZ == other.PointX ||
                       PointX == other.PointZ && PointY == other.PointX && PointZ == other.PointY;
            }
            return false;
        }

        public bool Judge()
        {
            return PointX == PointY || PointX == PointZ || PointY == PointZ;
        }

        public override int GetHashCode()
        {
            return PointX.GetHashCode() ^ PointY.GetHashCode() ^ PointZ.GetHashCode();
        }
    }
}
