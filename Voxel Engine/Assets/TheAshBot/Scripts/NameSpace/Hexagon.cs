using UnityEngine;

namespace TheAshBot
{
    public struct HexagonPointedTop
    {
        public float halfSize;
        public Vector2 centerPoint;
        public Vector2 upperRightCorner;
        public Vector2 upperLeftCorner;
        public Vector2 upperCorner;
        public Vector2 lowerCorner;
        public Vector2 lowerLeftCorner;
        public Vector2 lowerRightCorner;


        public HexagonPointedTop(Vector2 centerPoint, float halfSize)
        {
            this.centerPoint = centerPoint;
            this.halfSize = halfSize;

            upperCorner = centerPoint + new Vector2(0, +1) * halfSize;
            lowerCorner = centerPoint + new Vector2(0, -1) * halfSize;

            upperRightCorner = centerPoint + new Vector2(+1, +0.5f) * halfSize;
            upperLeftCorner = centerPoint + new Vector2(-1, +0.5f) * halfSize;
            lowerRightCorner = centerPoint + new Vector2(+1, -0.5f) * halfSize;
            lowerLeftCorner = centerPoint + new Vector2(-1, -0.5f) * halfSize;

        }
    }

    public struct HexagonFlatTop
    {
        public float halfSize;
        public Vector2 centerPoint;
        public Vector2 upperRightCorner;
        public Vector2 upperLeftCorner;
        public Vector2 rightCorner;
        public Vector2 leftCorner;
        public Vector2 lowerLeftCorner;
        public Vector2 lowerRightCorner;


        public HexagonFlatTop(Vector2 centerPoint, float halfSize)
        {
            this.centerPoint = centerPoint;
            this.halfSize = halfSize;

            rightCorner = centerPoint + new Vector2(+1, 0) * halfSize;
            leftCorner = centerPoint + new Vector2(-1, 0) * halfSize;

            upperRightCorner = centerPoint + new Vector2(0.5f, 1) * halfSize;
            upperLeftCorner = centerPoint + new Vector2(0.5f, -1) * halfSize;
            lowerRightCorner = centerPoint + new Vector2(-0.5f, 1) * halfSize;
            lowerLeftCorner = centerPoint + new Vector2(-0.5f, -1) * halfSize;

        }
    }
}
