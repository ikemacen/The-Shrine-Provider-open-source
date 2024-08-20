using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum Itemtype //stores the vaules of the shop item names
    {
        none,
        Pitchfork,
        TomatoCrop,
        WheatCrop
    }

    public static int GetCost(Itemtype itemtype) //stores the vaule of shop items prices
    {
        switch (itemtype)
        {
            default:
            case Itemtype.none: return 0;
            case Itemtype.Pitchfork: return 50;
            case Itemtype.TomatoCrop: return 1;
            case Itemtype.WheatCrop: return 2;
        }
    }
}
