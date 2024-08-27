using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCropBox : MonoBehaviour
{
    //--objects--
    public GameObject cropBox;

    //--scripts--
    public Threshold threshold;

    //--variables--
    public float nutrition = 10f;
    bool InTrigger;
    private PlayerInventory playerInventory;
    private void Start(){
        playerInventory = FindAnyObjectByType<PlayerInventory>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = true;
            Debug.Log("Player entered Trigger Box.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = false;
            Debug.Log("Player exited Trigger Box.");
        }
    }

    void Update()
    {
        if (InTrigger && Input.GetKeyDown(KeyCode.E)&&playerInventory.foodAmount>=nutrition)
        {
            threshold.amountThreshold += nutrition;
            Debug.Log(threshold.startThreshold.maxValue);
            playerInventory.RemoveFood((int)nutrition);
        }
    }
}
