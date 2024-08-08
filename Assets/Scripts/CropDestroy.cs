using UnityEngine;

public class CropDestroy : MonoBehaviour
{
    public CropTrigger cropTrigger; // Reference to the CropTrigger script
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
        if (Input.GetKeyDown(KeyCode.X) && InTrigger)
        {
            cropTrigger.currentCrop = null;
            Destroy(gameObject);
            Debug.Log("Crop destroyed. Replanting is allowed.");
        }
    }
}
