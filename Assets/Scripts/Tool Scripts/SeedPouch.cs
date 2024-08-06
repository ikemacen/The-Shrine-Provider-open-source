using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SeedPouch : MonoBehaviour
{
    private void OnTriggerEnter(Collider other) {
        print("Pick up seed bag!");
        ToolManager playerTool = other.gameObject.GetComponent<ToolManager>();
        playerTool.ChangeTool(this.gameObject);
    }
}
