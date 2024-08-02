using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    // Array for holding each tool we need
    // Could simply use an arbitrary number of GameObject variables, 
    // but doing it this way prevents the need for tool prefabs
    // and only requires the tool script
    [SerializeField] private MonoScript[] toolList;

    void Awake()
    {
        // For each tool in our tool list
        foreach(MonoScript newTool in toolList){
            // Create a component for that tool in our parent object (the player)
            this.gameObject.AddComponent(newTool.GetClass());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
