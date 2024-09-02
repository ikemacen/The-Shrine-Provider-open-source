using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Threshold : MonoBehaviour
{
    public Slider startThreshold;
    public float amountThreshold = 0f;
    public bool IsFed = false;
    public float maxVal = 100f;

    void Start()
    {
        startThreshold.maxValue = maxVal;
        startThreshold.value = amountThreshold;
    }

    void Update()
    {
        if (startThreshold.maxValue <= amountThreshold)
        {
            IsFed = true;
            //startThreshold.maxValue *= 2f;
            //amountThreshold = 0f;
            amountThreshold = startThreshold.maxValue;
        }
        /*else 
        {
            startThreshold.value = amountThreshold;
        }*/
        startThreshold.value = amountThreshold;
    }
}
