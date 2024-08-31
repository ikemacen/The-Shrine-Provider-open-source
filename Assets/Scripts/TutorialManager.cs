using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialManager : MonoBehaviour
{
public GameObject[] panels; // Array to store your panels
    private int currentPanelIndex = 0;
    private void Awake()
    {
        Time.timeScale = 0;
        ShowCurrentPanel();
    }

    public void StartGame()
    {
        Time.timeScale = 1;
        HideAllPanels();
    }

    public void ShowNextPanel()
    {
        currentPanelIndex = Mathf.Min(currentPanelIndex + 1, panels.Length - 1);
        ShowCurrentPanel();
    }

    public void ShowPreviousPanel()
    {
        currentPanelIndex = Mathf.Max(currentPanelIndex - 1, 0);
        ShowCurrentPanel();
    }

    private void ShowCurrentPanel()
    {
        HideAllPanels();
        panels[currentPanelIndex].SetActive(true);
    }

    private void HideAllPanels()
    {
        foreach (GameObject panel in panels)
        {
            panel.SetActive(false);
        }
    }
}
