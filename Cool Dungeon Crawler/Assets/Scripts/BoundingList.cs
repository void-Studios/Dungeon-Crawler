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
}
