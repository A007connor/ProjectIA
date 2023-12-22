using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichInvocation : MonoBehaviour
{
    [SerializeField] LayerMask lichLayer;
    [SerializeField] LayerMask invocationLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & invocationLayer) != 0)
        {    

        }
        else
        {
            Destroy(gameObject);
        }

    }
}
