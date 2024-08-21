using UnityEngine;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class SeedType
{
    public string seedName;
    public int seedAmount;
}

public class PlayerInventory : MonoBehaviour
{
    public List<SeedType> seedTypes = new List<SeedType>(); // List to hold seed types and amounts
    public int selectedSeedIndex = 0;
    public int coinBalance = 100;
    public int foodAmount = 0; 
    
    [SerializeField] private TextMeshProUGUI seedDisplay;
    [SerializeField] private TextMeshProUGUI coinDisplay;
    [SerializeField] private TextMeshProUGUI foodDisplay; 
    [SerializeField] private ToolManager toolManager;

    private Dictionary<string, int> seedCounts = new Dictionary<string, int>(); // Internal dictionary to track seed counts
    private List<string> seedNames = new List<string>();

    void Start()
    {
        InitializeSeedCounts(); // Initialize seed counts from the serialized list
        UpdateInventoryDisplay();
    }

    void Update()
    {
        for (int i = 0; i < seedNames.Count; i++)
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
        return (seedNames.Count > 0 && selectedSeedIndex < seedNames.Count) 
            ? seedNames[selectedSeedIndex] 
            : "No seeds selected";
    }

    public int GetCurrentSeedCount()
    {
        string currentSeed = GetCurrentSeed();
        return seedCounts.ContainsKey(currentSeed) ? seedCounts[currentSeed] : 0;
    }

    public void AddSeed(string seedName, int amount = 1)
    {
        if (seedCounts.ContainsKey(seedName))
        {
            seedCounts[seedName] += amount;
        }
        else
        {
            seedCounts.Add(seedName, amount);
            seedNames.Add(seedName);
        }
        selectedSeedIndex = Mathf.Clamp(selectedSeedIndex, 0, seedNames.Count - 1);
        UpdateSeedDisplay();
    }

    public void RemoveSeed(string seedName, int amount = 1)
    {
        if (seedCounts.ContainsKey(seedName))
        {
            seedCounts[seedName] -= amount;
            if (seedCounts[seedName] <= 0)
            {
                seedCounts.Remove(seedName);
                seedNames.Remove(seedName);
            }
            selectedSeedIndex = Mathf.Clamp(selectedSeedIndex, 0, seedNames.Count - 1);
            UpdateSeedDisplay();
        }
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
        UpdateCoinDisplay();
        UpdateFoodDisplay(); 
    }

    private void UpdateSeedDisplay()
    {
        if (seedDisplay != null)
        {
            string currentSeed = GetCurrentSeed();
            int currentSeedCount = GetCurrentSeedCount();
            seedDisplay.SetText($"Seed: {currentSeed} ({currentSeedCount})");
        }
    }

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
        seedCounts.Clear();
        seedNames.Clear();
        selectedSeedIndex = 0;
        UpdateSeedDisplay();
    }

    private void InitializeSeedCounts()
    {
        foreach (SeedType seedType in seedTypes)
        {
            if (!seedCounts.ContainsKey(seedType.seedName))
            {
                seedCounts.Add(seedType.seedName, seedType.seedAmount);
                seedNames.Add(seedType.seedName);
            }
        }
    }
}
