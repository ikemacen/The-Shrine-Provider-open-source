using UnityEngine;
using System.Collections.Generic;

public class PlayerInventory : MonoBehaviour
{
    public List<string> heldSeeds = new List<string>(); // List of crop seeds the player is holding
    public int selectedSeedIndex = 0; // Index of the currently selected seed

    void Update()
    {
        // Switch seeds using number keys (1, 2, 3, ...)
        for (int i = 0; i < heldSeeds.Count; i++)
        {
            if (Input.GetKeyDown((KeyCode) (KeyCode.Alpha1 + i)))
            {
                selectedSeedIndex = i;
                Debug.Log("Selected seed: " + GetCurrentSeed());
                break;
            }
        }
    }

    // Method to get the currently selected seed
    public string GetCurrentSeed()
    {
        if (heldSeeds.Count > 0 && selectedSeedIndex < heldSeeds.Count)
        {
            return heldSeeds[selectedSeedIndex];
        }
        return string.Empty;
    }

    // Method to add a new seed to the inventory
    public void AddSeed(string seedName)
    {
        heldSeeds.Add(seedName);
    }

    // Method to clear the held seeds (optional)
    public void ClearSeeds()
    {
        heldSeeds.Clear();
        selectedSeedIndex = 0;
    }
}
