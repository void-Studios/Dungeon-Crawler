using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class BoundingList 
{
    public TileBoundings one { get; set; }
    public TileBoundings two { get; set; }
    public TileBoundings three { get; set; }
    public TileBoundings four { get; set; }
    public TileBoundings five { get; set; }
    public TileBoundings six{ get; set; }
    public TileBoundings seven { get; set; }

    public BoundingList(TileBoundings one, TileBoundings two, TileBoundings three, TileBoundings four, TileBoundings five, TileBoundings six, TileBoundings seven)
    {
        this.one = one;
        this.two = two;
        this.three = three;
        this.four = four;
        this.five = five;
        this.six = six;
        this.seven = seven;
    }

    public TileBoundings Get(int value)
    {
        int testvalue = value - 1;
        switch (testvalue)
        {
            case 0:
                return one;
            case 1:
                return two;
            case 2:
                return three;
            case 3:
                return four;
            case 4:
                return five;
            case 5:
                return six;
            case 6:
                return seven;
            case -1:
                break;
        }
        return null;
    }

}
