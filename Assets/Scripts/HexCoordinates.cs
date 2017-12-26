using System;
using UnityEngine;

namespace DefaultNamespace
{
    [Serializable]
    public struct HexCoordinates
    {
        [SerializeField]
        private int _x;

        [SerializeField]
        private int _z;

        public int X
        {
            get { return _x; }
        }

        public int Z
        {
            get { return _z; }
        }

        public int Y
        {
            get { return -X - Z; }
        }

        public HexCoordinates(int x, int z) : this()
        {
            _x = x;
            _z = z;
        }

        public static HexCoordinates FromOffsetCoordinates(int x, int z)
        {
            return new HexCoordinates(x - z / 2, z);
        }

        public override string ToString()
        {
            return "(" + X + ", " + Y + ", " + Z + ")";
        }

        public string ToStringOnSeparateLines()
        {
            return X + "\n" + Y + "\n" + Z;
        }

        public static HexCoordinates FromPosition(Vector3 position)
        {
            float x = position.x / (HexMetrics.InnerRadius * 2F);
            float offset = position.z / (HexMetrics.OuterRadius * 3F);
            float y = -x;
            x -= offset;
            y -= offset;

            int ix = Mathf.RoundToInt(x);
            int iy = Mathf.RoundToInt(y);
            int iz = Mathf.RoundToInt(-x -y);

            // Rounding errors are possible to nearer we get to an edge
            if (ix + iy + iz != 0)
            {
                float dX = Mathf.Abs(x - ix);
                float dY = Mathf.Abs(y - iy);
                float dZ = Mathf.Abs(-x -y - iz);

                // find the "most rounded" coordinate and solve rounding issue by deriving it from the other 
                // two values
                if (dX > dY && dX > dZ) {
                    ix = -iy - iz;
                }
                else if (dZ > dY) {
                    iz = -ix - iy;
                }
            }
            return new HexCoordinates(ix, iz);
        }
    }
}