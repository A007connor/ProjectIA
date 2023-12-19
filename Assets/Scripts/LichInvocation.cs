using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichInvocation : MonoBehaviour
{
    [SerializeField] LayerMask playerLayer;
    [SerializeField] LayerMask lichLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & playerLayer) != 0)
        {
            Destroy(gameObject);

        }
        if (((1 << collision.gameObject.layer) & lichLayer) != 0)
        {
            Destroy(gameObject);
        }
    }
}
