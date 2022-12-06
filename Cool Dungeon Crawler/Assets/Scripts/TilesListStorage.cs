using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class TilesListStorage
{
    public TileBase[] tilesN { get; set; }
    public TileBase[] tilesE { get; set; }
    public TileBase[] tilesS { get; set; }
    public TileBase[] tilesW { get; set; }

    public TilesListStorage(TileBase[] tilesN, TileBase[] tilesE, TileBase[] tilesS, TileBase[] tilesW)
    {
        this.tilesN = tilesN;
        this.tilesE = tilesE;
        this.tilesS = tilesS;
        this.tilesW = tilesW;
    }

    public TileBase[] Get(int value)
    {
        int testvalue = value - 1;
        switch (testvalue)
        {
            case 0:
                return tilesN;
            case 1:
                return tilesE;
            case 2:
                return tilesS;
            case 3:
                return tilesW;
            case -1:
                break;
        }
        return null;
    }
}
