using UnityEngine;
using TMPro;

public class CropGrowth : MonoBehaviour
{
    public GameObject[] growthStages; // Array of crop models for each growth stage
    public float timeBetweenStages = 10f; // Time in seconds between each growth stage
    public TextMeshPro countdownText; // Reference to the TextMeshPro component

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

            // Update the countdown display
            UpdateCountdownDisplay();

            // Check if it's time to progress to the next stage
            if (growthTimer >= timeBetweenStages)
            {
                if (currentStage == 0)
                {
                    currentStage++;
                    UpdateGrowthStage();
                    growthTimer = 0f; // Reset the timer
                }
                if (currentStage == 1)
                {
                    // Stop growing when the last stage is reached
                    isGrowing = false;
                    countdownText.text = "Matured"; // Optional: Display a message when fully grown
                }
            }
        }
    }

    public void StartGrowing()
    {
        if (currentStage == 0)
        {
            isGrowing = true;
            UpdateCountdownDisplay(); // Initial update to set the timer display
        }
        else
        {
            Debug.LogWarning("Crop is already fully grown.");
        }
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

    void UpdateCountdownDisplay()
    {
        if (countdownText != null && isGrowing)
        {
            float remainingTime = timeBetweenStages - growthTimer;
            int minutes = Mathf.FloorToInt(remainingTime / 60f);
            int seconds = Mathf.FloorToInt(remainingTime % 60);
            countdownText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
        }
        else if (countdownText != null && !isGrowing)
        {
            countdownText.text = "Matured2"; // Display "Matured" or another message when fully grown
        }
    }
}
