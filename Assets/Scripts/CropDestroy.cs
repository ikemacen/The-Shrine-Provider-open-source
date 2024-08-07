using UnityEngine;
using TMPro;

public class CropDestroy : MonoBehaviour
{
    public CropTrigger cropTrigger; // Reference to the CropTrigger script
    public TextMeshPro countdownText; // Reference to the TextMeshPro component to clear text
    bool InTrigger;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = true;
            Debug.Log("Player entered trigger box.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = false;
            Debug.Log("Player exited trigger box.");
        }
    }

    void Update()
    {
        if (InTrigger && Input.GetKeyDown(KeyCode.X))
        {
            if (cropTrigger != null)
            {
                cropTrigger.currentCrop = null;
            }
            Destroy(gameObject);
            Debug.Log("Crop destroyed. Replanting is allowed.");
        }
    }

    private void OnDestroy()
    {
        // Clear the countdown text when the crop is destroyed
        if (countdownText != null)
        {
            countdownText.text = string.Empty; // Clear the countdown text
        }
        if (cropTrigger != null)
        {
            cropTrigger.OnCropDestroyed(); // Notify CropTrigger that the crop is destroyed
        }
    }
}
