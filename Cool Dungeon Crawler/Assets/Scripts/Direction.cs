using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Direction
{
    public int direction;

    public Direction(int direction)
    {
        this.direction = direction;
    }

    public int GetDirection()
    {
        return direction;
    }
    public int GetIncrement()
    {
        switch (direction)
        {
            case 0:
                return 1;
            case 1:
                return 1;
            case 2:
                return -1;
            case 3:
                return -1;
            default:
                break;
        }
        return 0;
    }

    public string GetAxis()
    {
        if (direction == 0 || direction == 2)
        {
            return "y";
        }
        else
        {
            return "x";
        }
        

    }

}
