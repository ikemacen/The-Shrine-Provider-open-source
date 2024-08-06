using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class ToolRack : MonoBehaviour
{
    [SerializeField] private GameObject[] availableTools;
    [SerializeField] private GameObject toolMenu;
    private void Awake()
    {
        ;
    }

    // Update is called once per frame
    void Update()
    {
    }

    void OnTriggerEnter(Collider other)
    {
        print("Select a tool!");
    }

    void OnTriggerExit(Collider other)
    {
        print("No more tools for you!");
    }
}
