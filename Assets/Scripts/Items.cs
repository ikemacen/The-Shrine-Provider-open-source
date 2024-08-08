using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum Itemtype //stores the vaules of the shop item names
    {
        none,
        Pitchfork
    }

    public static int GetCost(Itemtype itemtype) //stores the vaule of shop items prices
    {
        switch (itemtype)
        {
            default:
            case Itemtype.none: return 0;
            case Itemtype.Pitchfork: return 50;
        }
    }
}
