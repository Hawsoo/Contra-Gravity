using UnityEngine;
using System.Collections;

public class Angle
{
    // Optimizes angle in degrees
    public static float OptimizeAngle(float angle)
    {
        while (angle < 0) angle += 360;
        while (angle >= 360) angle -= 360;
        return angle;
    }
}
