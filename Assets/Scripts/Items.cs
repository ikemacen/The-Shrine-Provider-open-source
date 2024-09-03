using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Items : MonoBehaviour
{
    public enum Itemtype //stores the vaules of the shop item names
    {
        none,
        CarrotCrop,
        TomatoCrop,
        WheatCrop,
        CornCrop
    }

    public static int GetCost(Itemtype itemtype) //stores the vaule of shop items prices
    {
        switch (itemtype)
        {
            default:
            case Itemtype.none: return 0;
            case Itemtype.CarrotCrop: return 25;
            case Itemtype.TomatoCrop: return 5;
            case Itemtype.WheatCrop: return 10;
            case Itemtype.CornCrop: return 30;
        }
    }
}
