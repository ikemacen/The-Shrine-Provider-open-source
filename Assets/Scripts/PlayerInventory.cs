using UnityEngine;
using System.Collections.Generic;
using TMPro;

public class PlayerInventory : MonoBehaviour
{
    public List<string> heldSeeds = new List<string>();
    public int selectedSeedIndex = 0;
    public int coinBalance = 100;
    public int foodAmount = 0; // New field for food amount
    
    [SerializeField] private TextMeshProUGUI seedDisplay;
    //[SerializeField] private TextMeshProUGUI toolDisplay;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    [SerializeField] private TextMeshProUGUI foodDisplay; // New UI element for food display
    [SerializeField] private ToolManager toolManager;

    void Start()
    {
        UpdateInventoryDisplay();
    }

    void Update()
    {
        for (int i = 0; i < heldSeeds.Count; i++)
        {
            if (Input.GetKeyDown((KeyCode)(KeyCode.Alpha1 + i)))
            {
                selectedSeedIndex = i;
                UpdateSeedDisplay();
                break;
            }
        }
    }

    public string GetCurrentSeed()
    {
        return (heldSeeds.Count > 0 && selectedSeedIndex < heldSeeds.Count) 
            ? heldSeeds[selectedSeedIndex] 
            : "No seeds selected";
    }

    public void AddSeed(string seedName)
    {
        heldSeeds.Add(seedName);
        selectedSeedIndex = Mathf.Clamp(selectedSeedIndex, 0, heldSeeds.Count - 1);
        UpdateSeedDisplay();
    }

    public void AddCoins(int amount)
    {
        coinBalance += amount;
        UpdateCoinDisplay();
    }

    public bool SpendCoins(int amount)
    {
        if (coinBalance >= amount)
        {
            coinBalance -= amount;
            UpdateCoinDisplay();
            return true;
        }
        return false;
    }

    public void AddFood(int amount)
    {
        foodAmount += amount;
        UpdateFoodDisplay();
    }

    private void UpdateInventoryDisplay()
    {
        UpdateSeedDisplay();
        //UpdateToolDisplay();
        UpdateCoinDisplay();
        UpdateFoodDisplay(); // Update food display
    }

    private void UpdateSeedDisplay()
    {
        if (seedDisplay != null)
        {
            seedDisplay.SetText("Seed: " + GetCurrentSeed());
        }
    }

    /*private void UpdateToolDisplay()
    {
        if (toolManager != null && toolDisplay != null)
        {
            string toolName = toolManager.GetCurrentToolName() ?? "No tool equipped";
            toolDisplay.SetText("Tool: " + toolName);
        }
        else if (toolDisplay != null)
        {
            toolDisplay.SetText("Tool: No tool equipped");
        }
    }*/

    private void UpdateCoinDisplay()
    {
        if (coinDisplay != null)
        {
            coinDisplay.SetText("Coins: " + coinBalance);
        }
    }

    private void UpdateFoodDisplay()
    {
        if (foodDisplay != null)
        {
            foodDisplay.SetText("Food: " + foodAmount);
        }
    }

    public void ClearSeeds()
    {
        heldSeeds.Clear();
        selectedSeedIndex = 0;
        UpdateSeedDisplay();
    }
}
