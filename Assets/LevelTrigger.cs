using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelTrigger : MonoBehaviour
{
    public GameObject player;

    public GameObject doorOpened;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision == player)
        {
            if (doorOpened.name == "LevelChangeTrigger1")
            {
                SceneManager.LoadScene("2");
            }else if (doorOpened.name == "LevelChangeTrigger2")
            {
                SceneManager.LoadScene("3");
            }

        }
    }
}
