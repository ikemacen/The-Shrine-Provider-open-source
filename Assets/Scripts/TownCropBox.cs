using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TownCropBox : MonoBehaviour
{
    //--objects--
    public GameObject cropBox;
    public GameObject cropText;

    //--scripts--
    public Threshold threshold;

    //--variables--
    public float nutrition = 10f;
    bool InTrigger;
    private PlayerInventory playerInventory;
    private AudioManager audioManager;
    private void Start(){
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        audioManager = FindAnyObjectByType<AudioManager>();
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = true;
            cropText.SetActive(true);
            Debug.Log("Player entered Trigger Box.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = false;
            cropText.SetActive(false);
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
            audioManager.Play("Box1");
        }
    }
}
