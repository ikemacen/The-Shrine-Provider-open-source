using UnityEngine;

public class CropGrowth : MonoBehaviour
{
    public GameObject[] growthStages; // Array of crop models for each growth stage
    public float timeBetweenStages = 10f; // Time in seconds between each growth stage

    private int currentStage = 0;
    private float growthTimer = 0f;
    private bool isGrowing = false;

    void Start()
    {
        // Initialize the crop with the first growth stage
        if (growthStages.Length > 0)
        {
            UpdateGrowthStage();
        }
        else
        {
            Debug.LogError("Growth stages array is empty.");
        }
    }

    void Update()
    {
        if (isGrowing)
        {
            // Increment the timer
            growthTimer += Time.deltaTime;

            // Check if it's time to progress to the next stage
            if (growthTimer >= timeBetweenStages && currentStage < growthStages.Length - 1)
            {
                currentStage++;
                UpdateGrowthStage();
                growthTimer = 0f; // Reset the timer
            }
        }
    }

    public void StartGrowing()
    {
        isGrowing = true;
    }

    void UpdateGrowthStage()
    {
        // Disable all growth stage models
        foreach (GameObject stage in growthStages)
        {
            if (stage != null)
            {
                stage.SetActive(false);
            }
        }

        // Enable the current stage model
        if (currentStage < growthStages.Length && growthStages[currentStage] != null)
        {
            growthStages[currentStage].SetActive(true);
        }
        else
        {
            Debug.LogError("Current stage GameObject is null or out of bounds.");
        }
    }
}
