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

    public BoundingList Get(int value)
    {
        int testvalue = value - 1;
        switch (testvalue)
        {
            case 0:
                return boundingN;
            case 1:
                return boundingE;
            case 2:
                return boundingS;
            case 3:
                return boundingW;
            case -1:
                break;
        }
        return null;
    }


}
