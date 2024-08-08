using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    [SerializeField] private KeyCode interactKey;
    [SerializeField] private GameObject currentTool;

    public void ChangeTool(GameObject newTool) {
        currentTool = newTool;
    }

    void Awake()
    {
        // check if keybinds are initialized
        //if (interactKey == KeyCode.None) {
            //interactKey = KeyCode.E;
        //}
        if(currentTool == null) {
            //currentTool = availableTools[1].GetClass();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(interactKey) == true) {
            currentTool.GetComponent<ToolClass>().InteractAction();
        }
    }
}
