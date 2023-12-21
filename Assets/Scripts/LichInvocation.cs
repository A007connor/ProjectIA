using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LichInvocation : MonoBehaviour
{
    [SerializeField] LayerMask lichLayer;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Destroy(gameObject);
    }
}
