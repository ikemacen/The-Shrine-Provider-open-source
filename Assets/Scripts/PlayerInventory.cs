using UnityEngine;
using System.Collections.Generic;
using TMPro;

[System.Serializable]
public class SeedType
{
    public string seedName;
    public int seedAmount;
    public KeyCode keyBinding; // Key to select this seed type
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
    private Dictionary<KeyCode, string> keyToSeedMapping = new Dictionary<KeyCode, string>(); // Map keys to seed names

    void Start()
    {
        InitializeSeedCounts(); // Initialize seed counts from the serialized list
        UpdateInventoryDisplay();
    }

    void Update()
    {
        // Check if any key bound to a seed is pressed
        foreach (var entry in keyToSeedMapping)
        {
            if (Input.GetKeyDown(entry.Key))
            {
                string seedName = entry.Value;
                selectedSeedIndex = seedTypes.FindIndex(seed => seed.seedName == seedName);
                UpdateSeedDisplay();
                break;
            }
        }
        if (GetCurrentSeedCount() <= 0)
        {
            SwitchToNextAvailableSeed();
        }
    }

    public string GetCurrentSeed()
    {
        return (seedTypes.Count > 0 && selectedSeedIndex < seedTypes.Count)
            ? seedTypes[selectedSeedIndex].seedName
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
        }
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
            }
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

    public void RemoveFood(int amount)
    {
        if (foodAmount > 0)
        {
            foodAmount -= amount;
            UpdateFoodDisplay();
        }
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
                keyToSeedMapping.Add(seedType.keyBinding, seedType.seedName); // Map key to seed name
            }
        }
    }
     private void SwitchToNextAvailableSeed()
    {
        // Find the next seed with a positive count
        for (int i = 0; i < seedTypes.Count; i++)
        {
            int index = (selectedSeedIndex + i + 1) % seedTypes.Count;
            string seedName = seedTypes[index].seedName;
            if (seedCounts.ContainsKey(seedName) && seedCounts[seedName] > 0)
            {
                selectedSeedIndex = index;
                UpdateSeedDisplay();
                return;
            }
        }
    }
}
