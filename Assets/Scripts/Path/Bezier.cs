﻿using UnityEngine;

namespace Scripts.Path
{
    public static class Bezier
    {
        public static float CalculateLength(BezierSpline spline, int steps = 1000)
        {
            float totalSplineLength = 0f;

            for (int i = 0; i < steps - 1; i++)
            {
                var p1 = spline.GetPoint(i / (float)steps);
                var p2 = spline.GetPoint((i + 1) / (float)steps);

                totalSplineLength += Vector3.Distance(p1, p2);
            }

            return totalSplineLength;
        }

        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return oneMinusT * oneMinusT * p0 + 2f * oneMinusT * t * p1 + t * t * p2;
        }

        public static Vector3 GetFirstDerivative(Vector3 p0, Vector3 p1, Vector3 p2, float t)
        {
            return 2f * (1f - t) * (p1 - p0) + 2f * t * (p2 - p1);
        }

        public static Vector3 GetPoint(Vector3 p0, Vector3 p1, Vector3 p2, Vector3 p3, float t)
        {
            t = Mathf.Clamp01(t);
            float OneMinusT = 1f - t;
            return OneMinusT * OneMinusT * OneMinusT * p0
                + 3f * OneMinusT * OneMinusT * t * p1
                + 3f * OneMinusT * t * t * p2
                + t * t * t * p3;
        }

        public static Vector3 GetFirstDerivative(
            Vector3 p0,
            Vector3 p1,
            Vector3 p2,
            Vector3 p3,
            float t
        )
        {
            t = Mathf.Clamp01(t);
            float oneMinusT = 1f - t;
            return 3f * oneMinusT * oneMinusT * (p1 - p0)
                + 6f * oneMinusT * t * (p2 - p1)
                + 3f * t * t * (p3 - p2);
        }
    }
}
