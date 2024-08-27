using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersonalCropBox : MonoBehaviour
{
    //--objects--
    public GameObject cropBox;
    public GameObject personalText;
    //--scripts--
    public TimerScript timer;

    //--variables--
    public float nutrition = 10f;
    bool InTrigger;
    private PlayerInventory playerInventory;
    private void Start(){
        playerInventory = FindAnyObjectByType<PlayerInventory>();
        if (playerInventory == null){
            Debug.LogError("Player Inventory not found");
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = true;
            personalText.SetActive(true);
            Debug.Log("Player entered Trigger Box.");
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            InTrigger = false;
            personalText.SetActive(false);
            Debug.Log("Player exited Trigger Box.");
        }
    }

    void Update()
    {
        if (InTrigger && Input.GetKeyDown(KeyCode.E))
        {
            if ((timer.startTimer.maxValue - timer.amountTimer) > nutrition)
            {
                timer.amountTimer += nutrition;
                Debug.Log(timer.startTimer.maxValue);
                playerInventory.RemoveFood((int)nutrition);
            } 
        }
    }
}
