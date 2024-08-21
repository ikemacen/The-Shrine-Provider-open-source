using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using TMPro;

public class ToolManager : MonoBehaviour
{
    // FOR DEBUGGING
    [SerializeField] private KeyCode interactKey;

    [SerializeField] private GameObject currentTool;
    [SerializeField] private TextMeshProUGUI toolDisplay;

    public void ChangeTool(GameObject newTool)
    {
        currentTool = newTool;
        string toolName = newTool != null ? newTool.name : "None";
        Debug.Log("Tool changed to: " + toolName);
        UpdateToolDisplay(toolName); // Update the text display
    }

    private void UpdateToolDisplay(string toolName)
    {
        if (toolDisplay != null)
        {
            toolDisplay.SetText("Tool: " + toolName);
        }
    }

    public GameObject GetCurrentTool()
    {
        return currentTool;
    }

    public string GetCurrentToolName()
    {
        return currentTool != null ? currentTool.name : "No tool";
    }

    void Update()
    {
        // DEBUG: for testing player interaction
        if(Input.GetKeyDown(interactKey) == true) {
            currentTool.GetComponent<ToolClass>().InteractAction();
        }
    }
}
