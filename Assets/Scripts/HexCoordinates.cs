﻿using System;
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
    }
}