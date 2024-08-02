using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class ToolManager : MonoBehaviour
{
    // What tools do we need?
    // Let's define a tool first

    [SerializeField] private MonoScript[] toolList;

    void Awake()
    {
        foreach(MonoScript newTool in toolList){
            print(newTool.GetClass());
            this.gameObject.AddComponent(newTool.GetClass());
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
