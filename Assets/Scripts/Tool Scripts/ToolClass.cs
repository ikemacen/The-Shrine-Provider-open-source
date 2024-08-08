using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToolClass : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        ToolManager playerTool = other.gameObject.GetComponent<ToolManager>();
        playerTool.ChangeTool(this.gameObject);
        this.PickupAction();
    }
    public void PickupAction() {
        print("Despawn Item");
        this.gameObject.SetActive(false);
    }

    public virtual void InteractAction() {
        print("Perform Default Interaction");
    }
}
