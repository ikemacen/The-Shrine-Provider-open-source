using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour, Shopinterface
{
    private PlayerInventory inventory; // Reference to PlayerInventory to manage seeds and coins

    private void Start()
    {
        // Assuming PlayerInventory is attached to the same GameObject or can be found
        inventory = GetComponent<PlayerInventory>();

        if (inventory == null)
        {
            Debug.LogError("PlayerInventory component not found!");
        }
    }

    public void Boughtitem(Items.Itemtype itemtype)
    {
        Debug.Log("Bought item: " + itemtype);
        
        // Handle buying item logic here
        // For example, you might want to update player stats or abilities
    }

    public void AddSeed(string seedName, int amount)
    {
        if (inventory != null)
        {
            inventory.AddSeed(seedName, amount);
        }
        else
        {
            Debug.LogError("PlayerInventory not initialized.");
        }
    }

    public bool SpendCoins(int amount)
    {
        if (inventory != null)
        {
            return inventory.SpendCoins(amount);
        }
        else
        {
            Debug.LogError("PlayerInventory not initialized.");
            return false;
        }
    }
}

