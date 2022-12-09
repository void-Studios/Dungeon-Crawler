using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileBoundings
{
    
    public TileBoundings(int boundingN, int boundingE, int boundingS, int boundingW)
    {
        BoundingN = boundingN;
        BoundingE = boundingE;
        BoundingS = boundingS;
        BoundingW = boundingW;
    }

    public int BoundingN { get; set; }
    public int BoundingS { get; set; }
    public int BoundingE { get; set; }
    public int BoundingW { get; set; }

    public int Get(int value)
    {
        switch (value)
        {
            case 0:
                return BoundingN;
            case 1:
                return BoundingE;
            case 2:
                return BoundingS;
            case 3:
                return BoundingW;
            case -1:
                break;
        }
        return -1;
    }



}
