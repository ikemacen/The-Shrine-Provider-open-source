using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Shopinterface 
{
    void Boughtitem(Items.Itemtype itemtype);
    void AddSeed(string seedName, int amount);
    bool SpendCoins(int amount);
}
