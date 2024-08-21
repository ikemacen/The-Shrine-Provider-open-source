using UnityEngine;

public class CropTrigger : MonoBehaviour
{
    public GameObject[] cropPrefabs; // Array of crop prefabs to instantiate
    public Transform spawnPoint; // The location where the crop will spawn
    public KeyCode plantKey = KeyCode.E; // The key used to plant the crop
    public GameObject gameOb; // UI or indicator object to activate when the player is in the trigger

    private bool playerInTrigger = false; // Flag to check if player is in the trigger box
    private GameObject player; // Reference to the player object
    public bool canPlant = true; // Flag to check if planting is allowed
    public GameObject currentCrop; // Reference to the currently planted crop

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = true;
            player = other.gameObject;
            Debug.Log("Player entered trigger box. Press 'E' to plant.");
            gameOb.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            playerInTrigger = false;
            player = null;
            Debug.Log("Player exited trigger box.");
            gameOb.SetActive(false);
        }
    }

    private void Update()
    {
        if (playerInTrigger && canPlant && Input.GetKeyDown(plantKey))
        {
            PlantCrop();
        }

        // Allow replanting if the crop has been destroyed
        if (currentCrop == null)
        {
            canPlant = true;
            Debug.Log("Current crop is null. Replanting is allowed.");
        }
    }

    private void PlantCrop()
    {
        if (currentCrop == null)
        {
            // Get the player's held seed
            PlayerInventory playerInventory = player.GetComponent<PlayerInventory>();
            if (playerInventory == null)
            {
                Debug.LogWarning("PlayerInventory script not found.");
                return;
            }

            string selectedSeed = playerInventory.GetCurrentSeed();
            if (string.IsNullOrEmpty(selectedSeed))
            {
                Debug.LogWarning("Player is not holding any seed.");
                return;
            }

            // Find the corresponding crop prefab based on the selected seed
            GameObject selectedCropPrefab = null;
            foreach (GameObject cropPrefab in cropPrefabs)
            {
                if (cropPrefab.name == selectedSeed)
                {
                    selectedCropPrefab = cropPrefab;
                    break;
                }
            }

            if (selectedCropPrefab != null)
            {
                // Check if the player has enough seeds
                if (playerInventory.GetCurrentSeedCount() > 0)
                {
                    // Reduce the seed count by 1
                    playerInventory.RemoveSeed(selectedSeed, 1);

                    // Instantiate the crop
                    currentCrop = Instantiate(selectedCropPrefab, spawnPoint.position, spawnPoint.rotation);
                    CropGrowth cropGrowth = currentCrop.GetComponent<CropGrowth>();

                    if (cropGrowth != null)
                    {
                        cropGrowth.StartGrowing();
                        canPlant = false; // Disable planting until crop is destroyed
                        Debug.Log("Crop planted. Planting disabled until crop is destroyed.");
                    }
                    else
                    {
                        Debug.LogError("CropGrowth script not found on instantiated crop.");
                    }
                }
                else
                {
                    Debug.LogWarning("Not enough seeds to plant.");
                }
            }
            else
            {
                Debug.LogWarning("No crop prefab found for the held seed: " + selectedSeed);
            }
        }
    }

    // Public method to be called by CropDestroy
    public void OnCropDestroyed()
    {
        currentCrop = null;
        canPlant = true;
        Debug.Log("Crop destroyed. Replanting is allowed.");
    }
}
