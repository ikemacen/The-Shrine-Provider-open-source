using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private ToolClass[] test;
    [SerializeField] private GameObject currentTool;

    public void ChangeTool(GameObject newTool) {
        currentTool = newTool;
    }

    void Awake()
    {
        //activeToolList = new ToolClass[availableTools.Length];
        // check if keybinds are initialized
        if (interactKey == KeyCode.None) {
            interactKey = KeyCode.E;
        }
        // For each tool in our tool list
        //foreach(GameObject newTool in availableTools){
            // Create a component for that tool in our parent object (the player)
            //var temp = this.gameObject.AddComponent(newTool);
        //}
        if(currentTool == null) {
            //currentTool = availableTools[1].GetClass();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(interactKey))
        {
            //currentTool.InteractAction();
        }
    }
}
