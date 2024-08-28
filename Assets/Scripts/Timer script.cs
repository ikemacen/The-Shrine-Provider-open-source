using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TimerScript : MonoBehaviour
{
    public GameObject gameOver;
    public GameObject gameOverText;
    public Slider startTimer;
    public float amountTimer;
    public bool stopTimer = false;

    // Start is called before the first frame update
    void Start()
    {
        startTimer.maxValue = amountTimer;
        startTimer.value = amountTimer;
        StartTimer();
    }

    public void StartTimer()
    {

        StartCoroutine(StartTheTimer());

    }
    IEnumerator StartTheTimer()
    {

        while (stopTimer == false)
        {
            amountTimer -= Time.deltaTime;
            yield return new WaitForSeconds(0.001f);

            if (amountTimer <= 0)
            {
                stopTimer = true;
                gameOver.SetActive(true);
                gameOverText.SetActive(true);
                Time.timeScale = 0f;
            }
            if (stopTimer == false)
            {
                startTimer.value = amountTimer;
            }

        }
        //Add logic here

    }
    public void StopTimer()
    {
        stopTimer = true;
    }



}
