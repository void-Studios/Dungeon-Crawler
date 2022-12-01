using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class TileBoundings
{
    public TileBoundings(int boundingN, int boundingS, int boundingE, int boundingW)
    {
        BoundingN = boundingN;
        BoundingS = boundingS;
        BoundingE = boundingE;
        BoundingW = boundingW;
    }

    public int BoundingN { get; set; }
    public int BoundingS { get; set; }
    public int BoundingE { get; set; }
    public int BoundingW { get; set; }
}
