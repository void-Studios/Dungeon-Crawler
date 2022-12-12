using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrailPoint
{
    public Vector2Int trailPosition;
    public Direction direction;
    public int tile;
    public bool isSuccess;
    public TileBoundings boundings;

    public TrailPoint(Vector2Int trailPosition, Direction direction, int tile, bool isSuccess, TileBoundings boundings)
    {
        this.trailPosition = trailPosition;
        this.direction = direction;
        this.tile = tile;
        this.isSuccess = isSuccess;
        this.boundings = boundings;
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
