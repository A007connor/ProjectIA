using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class GameUI : MonoBehaviour
{
    public float timer;
    public bool timeIsRunning;
    public TMP_Text timerText;
    public TMP_Text deathCount;


    private void Start()
    {
        timeIsRunning = true;
    }
    // Update is called once per frame
    void Update()
    {
        if (timeIsRunning)
        {
            timer += Time.deltaTime;
            DisplayTime(timer);
        }
    }

    void DisplayTime(float timeToDisplay)
    {
        timeToDisplay += 1;
        float minutes = Mathf.FloorToInt(timeToDisplay / 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);
        timerText.text = string.Format("{0:00} : {1:00}", minutes, seconds);
    }
}
