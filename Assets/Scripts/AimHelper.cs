using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class AimHelper
{
    public class AimAngle
    {
        public Enums.Angles.X x;
        public Enums.Angles.Y y;
    }

    public static AimAngle GetCurrentAimAngle(Vector2 mousePos, Vector2 playerPos)
    {
        Vector2 v = mousePos - playerPos;
        var angleRadians = Mathf.Atan2(v.y, v.x);
        var angleDegrees = angleRadians * Mathf.Rad2Deg;

        if (angleDegrees < 0)
            angleDegrees += 360;

        AimAngle currentAngle = new AimAngle();
        if (angleDegrees > 0 && angleDegrees <= 90)
        {
            currentAngle.x = Enums.Angles.X.Right;
            currentAngle.y = Enums.Angles.Y.Top;
        }
        else if (angleDegrees > 90 && angleDegrees <= 180)
        {
            currentAngle.x = Enums.Angles.X.Left;
            currentAngle.y = Enums.Angles.Y.Top;
        }
        else if (angleDegrees > 180 && angleDegrees <= 270)
        {
            currentAngle.x = Enums.Angles.X.Left;
            currentAngle.y = Enums.Angles.Y.Bottom;
        }
        else
        {
            currentAngle.x = Enums.Angles.X.Right;
            currentAngle.y = Enums.Angles.Y.Bottom;
        }

        return currentAngle;
    }
}