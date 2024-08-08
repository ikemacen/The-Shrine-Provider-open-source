using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    // FOR DEBUGGING
    [SerializeField] private KeyCode interactKey;

    [SerializeField] private GameObject currentTool;

    public void ChangeTool(GameObject newTool) {
        currentTool = newTool;
    }

    public GameObject GetCurrentTool() {
        return currentTool;
    }

    void Update()
    {
        // DEBUG: for testing player interaction
        if(Input.GetKeyDown(interactKey) == true) {
            currentTool.GetComponent<ToolClass>().InteractAction();
        }
    }
}
