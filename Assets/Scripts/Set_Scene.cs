using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Set_Scene : MonoBehaviour
{
    public string sceneName; // Nom de la scène vers laquelle vous souhaitez changer

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player")) // Vérifie si le joueur entre dans la zone
        {
            SceneManager.LoadScene(sceneName); // Charge la nouvelle scène
        }
    }
}