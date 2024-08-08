using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolClass : MonoBehaviour
{
    // this "flag" is to prevent the tools from infinitely swapping while the player stands on top of one
    private bool canSwap = true;
    private void OnTriggerEnter(Collider other) {
        // only perform swap if player has not swapped tools recently
        if (canSwap) {
            ToolManager playerTool = other.gameObject.GetComponent<ToolManager>();
            // if the player has a tool
            if(playerTool.GetCurrentTool() != null) {
                // replace the new tool in the world with the old tool
                GameObject oldTool = playerTool.GetCurrentTool();
                oldTool.SetActive(true);
                oldTool.transform.position = this.transform.position;
            }
            // have the player "pickup" the new tool
            playerTool.ChangeTool(this.gameObject);
            this.PickupAction();
            this.gameObject.SetActive(false);
            canSwap = false;
        }
    }
    // reset the swap flag when the player leaves an object trigger
    private void OnTriggerExit(Collider other) {
        canSwap = true;
    }
    // virtual pickup function in case you want something special to happen on pickup
    public virtual void PickupAction() {
        Debug.Log("Virtual Pickup Function Success");
    }
    // virtual interaction function to allow for custom interaction behavior
    public virtual void InteractAction() {
        Debug.Log("Virtual Interact Function Success");
    }
}
