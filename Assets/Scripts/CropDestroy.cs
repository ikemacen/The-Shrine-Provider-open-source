using UnityEngine;
using TMPro;

public class CropDestroy : MonoBehaviour
{
    public TextMeshPro countdownText; // Reference to the TextMeshPro component to clear text
    public int coinReward = 10;
    public int foodReward = 10;
    private bool inTrigger;
    private CropTrigger cropTrigger;
    private PlayerInventory playerInventory;
    public GameObject textObj;
    private AudioManager audioManager;
    private void Start(){
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = true;
            textObj.SetActive(true);

            // Find the CropTrigger in the parent or children of the trigger box, or in nearby objects
            CropTrigger[] cropTriggers = FindObjectsOfType<CropTrigger>();

            foreach (CropTrigger trigger in cropTriggers)
            {
                // Adjust the distance threshold as needed
                if (Vector3.Distance(trigger.transform.position, transform.position) < 2f)
                {
                    cropTrigger = trigger;
                    break;
                }
            }

            if (cropTrigger == null)
            {
                Debug.LogWarning("No nearby CropTrigger script found.");
            }

            Debug.Log("Player entered trigger box.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            inTrigger = false;
            textObj.SetActive(false);
            Debug.Log("Player exited trigger box.");
        }
    }

    private void Update()
    {
        if (inTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if (cropTrigger != null && cropTrigger.currentCrop != null)
            {
                Destroy(cropTrigger.currentCrop);
                cropTrigger.currentCrop = null;
                Debug.Log("Crop destroyed. Replanting is allowed.");
                if(playerInventory!= null){
                    playerInventory.AddCoins(coinReward);
                    playerInventory.AddFood(foodReward);
                }
                audioManager.Play("Harvest");
            }
            else
            {
                Debug.LogWarning("No crop to destroy.");
            }
        }
    }

    private void OnDestroy()
    {
        // Clear the countdown text when the crop is destroyed
        if (countdownText != null)
        {
            countdownText.text = string.Empty;
        }

        if (cropTrigger != null)
        {
            cropTrigger.OnCropDestroyed(); // Notify CropTrigger that the crop is destroyed
        }
    }
}
