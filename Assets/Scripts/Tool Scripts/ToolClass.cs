using UnityEngine;

public class ToolClass : MonoBehaviour
{
    private bool canSwap = true;
    private AudioManager audioManager;
    private void Start(){
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    private void OnTriggerEnter(Collider other) {
        if (canSwap) {
            ToolManager playerTool = other.gameObject.GetComponent<ToolManager>();
            if (playerTool != null) {
                if (playerTool.GetCurrentTool() != null) {
                    GameObject oldTool = playerTool.GetCurrentTool();
                    oldTool.SetActive(true);
                    oldTool.transform.position = this.transform.position;
                }
                playerTool.ChangeTool(this.gameObject);
                this.PickupAction();
                this.gameObject.SetActive(false);
                canSwap = false;
            }
        }
    }

    private void OnTriggerExit(Collider other) {
        canSwap = true;
    }

    public virtual void PickupAction() {
        Debug.Log("Picked up tool: " + this.gameObject.name);
        if(this.gameObject.name == "Watering Can"){
            audioManager.Play("Bucket");
        }
        if(this.gameObject.name == "Seed Bag"){
            audioManager.Play("Bag");
        }
    }

    public virtual void InteractAction() {
        Debug.Log("Interacting with tool: " + this.gameObject.name);
    }
}
