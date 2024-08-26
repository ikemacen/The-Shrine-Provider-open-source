using UnityEngine;

public class TriggerActivator : MonoBehaviour
{
    private ToolManager toolManager; // Reference to the ToolManager

    private void Start()
    {
        toolManager = FindObjectOfType<ToolManager>();
        if (toolManager == null)
        {
            Debug.LogError("ToolManager not found in the scene.");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Check if the player is carrying the WaterCan
            GameObject currentTool = toolManager.GetCurrentTool();
            if (currentTool != null && currentTool.GetComponent<WaterCan>() != null)
            {
                ActivateParentCropTrigger();
            }
        }
    }

    private void ActivateParentCropTrigger()
    {
        // Access the parent CropTrigger component
        CropTrigger cropTrigger = GetComponentInParent<CropTrigger>();
        if (cropTrigger != null)
        {
            cropTrigger.EnableTrigger();
            Debug.Log("Enabled CropTrigger on parent object.");
        }
        else
        {
            Debug.LogError("Parent CropTrigger component not found.");
        }
    }
}
