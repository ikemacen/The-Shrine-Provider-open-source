using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Shopinterface
{
    int Goldamount;
    public void Boughtitem(Items.Itemtype itemtype)//allows the player to buy have the tag to buy the item
    {
        Debug.Log("Bought item" + itemtype);
    }
}
