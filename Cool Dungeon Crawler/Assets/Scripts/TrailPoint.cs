using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPoint
{
    public int x;
    public int y;
    public Direction direction;
    public int tile;
    public bool isSuccess;

    public TrailPoint(int x, int y, Direction direction, int tile, bool isSuccess)
    {
        this.x = x;
        this.y = y;
        this.direction = direction;
        this.tile = tile;
        this.isSuccess = isSuccess;
    }

    public void Next()
    {
        if (direction.direction<=2)
        {
            direction.direction += 1;
        }
        else
        {
            direction.direction = 0;
        }
    }

}
