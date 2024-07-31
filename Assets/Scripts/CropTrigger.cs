using UnityEngine;

public class CropTrigger : MonoBehaviour
{
    public GameObject cropPrefab; // The crop prefab to instantiate
    public Transform spawnPoint; // The location where the crop will spawn

    private void OnTriggerEnter(Collider other)
    {
        // Check if the object entering the trigger is the player
        if (other.CompareTag("Player"))
        {
            // Instantiate the crop at the spawn point
            GameObject cropInstance = Instantiate(cropPrefab, spawnPoint.position, spawnPoint.rotation);

            // Get the CropGrowth component from the instantiated crop
            CropGrowth cropGrowth = cropInstance.GetComponent<CropGrowth>();

            // Start the growth process
            if (cropGrowth != null)
            {
                cropGrowth.StartGrowing();
            }
            else
            {
                Debug.LogError("CropGrowth script not found on instantiated crop.");
            }

            // Optional: Disable the trigger after activation if you want it to be a one-time event
            gameObject.SetActive(false);
        }
    }
}
