using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SceneManager : MonoBehaviour
{
    public static bool isGamePaused = false;
    public SettingsMenu settingsMenu;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("H");
            TogglePause();
        }
    }

    void TogglePause()
    {
        Debug.Log("A");
        isGamePaused = !isGamePaused;

        if (settingsMenu != null) //c'est là dedans qu'on peut pas rentrer
        {
            Debug.Log("B");
            settingsMenu.ToggleMenu(isGamePaused);
        }
        Time.timeScale = isGamePaused ? 0 : 1;
    }
}
