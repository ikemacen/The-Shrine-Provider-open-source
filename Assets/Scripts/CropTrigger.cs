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
    private Collider cropCollider;
    private ToolManager toolManager;
    private AudioManager audioManager;
    private void Start()
    {
        cropCollider = GetComponent<Collider>();
        if (cropCollider != null)
        {
            cropCollider.enabled = false; // Disable the collider at the start
        }
        toolManager = FindObjectOfType<ToolManager>();
        if (toolManager == null)
        {
            Debug.LogError("ToolManager not found in the scene.");
        }
        audioManager = FindAnyObjectByType<AudioManager>();
        if (audioManager == null){
            Debug.LogError("AudioManager Not Here");
        }
    }
    public void EnableTrigger()
    {
        if (cropCollider != null)
        {
            cropCollider.enabled = true;
            Debug.Log("CropTrigger enabled at: " + transform.position);
        }
    }
    public void DisableTrigger()
    {
        if (cropCollider != null)
        {
            cropCollider.enabled = false;
            Debug.Log("CropTrigger disabled at: " + transform.position);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        GameObject currentTool = toolManager.GetCurrentTool();
        if (other.CompareTag("Player") && currentTool.GetComponent<SeedPouch>() != null)
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
        GameObject currentTool = toolManager.GetCurrentTool();
        if (playerInTrigger && canPlant && Input.GetKeyDown(plantKey))
        {
            if(currentTool.GetComponent<SeedPouch>() != null){
                PlantCrop();
                audioManager.Play("Plant");
            }
        }

        // Allow replanting if the crop has been destroyed
        if (currentCrop == null)
        {
            canPlant = true;
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
        DisableTrigger();
        currentCrop = null;
        canPlant = true;
        player = null;
        playerInTrigger = false;
        gameOb.SetActive(false);
        Debug.Log("Crop destroyed. Replanting is allowed.");
    }
}
