using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoundingListStorage
{
    public BoundingList boundingN { get; set; }
    public BoundingList boundingE { get; set; }
    public BoundingList boundingS { get; set; }
    public BoundingList boundingW { get; set; }

    public BoundingListStorage(BoundingList boundingN, BoundingList boundingE, BoundingList boundingS, BoundingList boundingW)
    {
        this.boundingN = boundingN;
        this.boundingE = boundingE;
        this.boundingS = boundingS;
        this.boundingW = boundingW;
    }
}
