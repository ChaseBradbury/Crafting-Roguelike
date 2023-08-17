using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Utils
{
    public static SlotDirection FindDirectionQuadrant(Vector2 point, Vector2 center)
    {
        float xDist = point.x - center.x;
        float yDist = point.y - center.y;
        if (xDist > yDist)
        {
            // In SE
            if (xDist + yDist > 0)
            {
                return SlotDirection.East;
            }
            else
            {
                return SlotDirection.South;
            }
        }
        else
        {
            // In NW
            if (xDist + yDist > 0)
            {
                return SlotDirection.North;
            }
            else
            {
                return SlotDirection.West;
            }
        }
    }
}
