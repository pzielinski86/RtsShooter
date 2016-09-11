﻿using UnityEngine;

namespace Game.Core.Math
{
    public class MathEx
    {
        public static float AngleBetweenVector3(Vector3 a, Vector3 b)
        {
            // angle in [0,180]
            float angle = Vector3.Angle(a, b);
            float sign = System.Math.Sign(Vector3.Dot(Vector3.up, Vector3.Cross(a, b)));

            // angle in [-179,180]
            float signed_angle = angle * sign;

            return signed_angle;
        }

        public static float Clamp(float value, float min, float max)
        {
            if (value > max)
                return max;
            if (value < min)
                return min;

            return value;
        }
    }
}