using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class StartScreen : MonoBehaviour
{
    public GameObject startScreen;
    private bool gameStarted = false;

    private void Awake()
    {
        startScreen.SetActive(true);
        Time.timeScale = 0.0f;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown && !gameStarted)
        {
            Debug.Log("hi");
            gameStarted = true;
            Time.timeScale = 1.0f;
            startScreen.SetActive(false);
        }
    }
}
