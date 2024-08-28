using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ClockTimer : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject GoodEnding;
    public GameObject BadEnding; 
    public Threshold threshold; 
    [SerializeField] TextMeshProUGUI timerText;
    [SerializeField] float remainingTime;

    void Update()
    {
        if (remainingTime > 0)
        {
            remainingTime -= Time.deltaTime;
        }
        else if (remainingTime < 0)
        {
            remainingTime = 0;
            gameOver.SetActive(true);
            if(threshold.IsFed is true)
            {
                GoodEnding.SetActive(true);
            }
            else
            {
                BadEnding.SetActive(true);
            }
            Time.timeScale = 0f;
        }
        
        int minutes = Mathf.FloorToInt (remainingTime / 60);
        int seconds = Mathf.FloorToInt (remainingTime % 60);
        timerText.text = string.Format("{0:00}:{1:00}", minutes, seconds);
    }
}