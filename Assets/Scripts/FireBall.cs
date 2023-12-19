using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;

    [SerializeField]
    private Animator fireAnim;

    private BoxCollider boxCollider;



    private bool hit;

    public int damage = 20;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            //PlayerHealth playerHealth = collision.gameObject.GetComponent<PlayerHealth>();
            //if (playerHealth != null)
            //{
            //    playerHealth.RecevoirDegats(damage);
            //}

            
            Destroy(gameObject);
        }
        fireAnim.SetTrigger("Explode");
    }

    private void Update()
    {
        if (hit) return;
        float movementSpeed = Time.deltaTime;
        boxCollider.enabled = false;
        transform.Translate(movementSpeed, 0, 0);
    }

}
