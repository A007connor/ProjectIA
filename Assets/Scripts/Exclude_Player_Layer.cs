using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Exclude_Player_Layer : MonoBehaviour
{
    private void Start()
    {
        // Ignore la collision avec tous les autres layers, sauf le layer actuel du joueur
        int playerLayer = gameObject.layer;
        for (int i = 0; i < 32; i++)
        {
            if (i != playerLayer)
            {
                Physics.IgnoreLayerCollision(playerLayer, i);
            }
        }
    }

    // Vous pouvez appeler cette fonction chaque fois que le layer du joueur change
    public void UpdatePlayerLayer()
    {
        int playerLayer = gameObject.layer;

        // Réactive toutes les collisions
        for (int i = 0; i < 32; i++)
        {
            if (i != playerLayer)
            {
                Physics.IgnoreLayerCollision(playerLayer, i, false);
            }
        }

        // Ignore la collision avec tous les autres layers, sauf le layer actuel du joueur
        for (int i = 0; i < 32; i++)
        {
            if (i != playerLayer)
            {
                Physics.IgnoreLayerCollision(playerLayer, i);
            }
        }
    }
}

