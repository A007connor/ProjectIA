using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBall : MonoBehaviour
{
    private float speed;

    [SerializeField]
    private Animator fireAnim;

    private BoxCollider2D boxCollider;

    private float direction;

    [SerializeField]
    private string targetTag;

    private bool hit;
    private int damage;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        hit = true;
        boxCollider.enabled = false;
        if (collision.gameObject.CompareTag("Player"))
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
        if (hit) 
        {
            return;
        }
        float moveSpeed = speed * Time.deltaTime * direction;
        transform.Translate(moveSpeed, 0, 0);
    }

    public void SetDirection(float _direction)
    {
        direction = _direction;
        gameObject.SetActive(true);
        hit = false;
        boxCollider.enabled = true;

        float localScaleX = transform.localScale.x;
        if(Mathf.Sign(localScaleX) != direction)
        {
            localScaleX = -localScaleX;
        }
        transform.localScale = new Vector3(localScaleX, transform.localScale.y, transform.localScale.z);
    }

   private void Deactivate()
    {
        gameObject.SetActive(false);
    }

}
