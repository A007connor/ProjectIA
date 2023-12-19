<<<<<<< HEAD:Assets/Scripts/Enemy/FireBallScript.cs
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    [SerializeField] private float speed;
    [SerializeField] private float resetTime;
    private float direction;
    private bool hit;

    private float lifeTime;

    private BoxCollider2D boxCollider;
    private Animator FireAnim;

    // Start is called before the first frame update
    private void Awake()
    {
        FireAnim = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    public void ActivateFire()
    {
        hit = false;
        lifeTime = 0;
        gameObject.SetActive(false);
        boxCollider.enabled = true;
    }

    // Update is called once per frame
    private void Update()
    {
        if (hit) return;
        float moveSpeed = speed * Time.deltaTime;
        transform.Translate(moveSpeed, 0, 0);

        lifeTime += Time.deltaTime;
        if(lifeTime > resetTime)
        {
            gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        hit = true;
        //base.OnTriggerEnter2D(collision);

        boxCollider.enabled = true;

        if (FireAnim != null)
        {
            FireAnim.SetTrigger("Explode");
        }
        else
        {
            gameObject.SetActive(false);
        }
        
    }

    private void Desactivate()
    {
        gameObject.SetActive(false);
    }


}
=======
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBallScript : MonoBehaviour
{
    [SerializeField]
    private float speed;

    private bool hit;

    private BoxCollider2D boxCollider;
    private Animator AnimFire;

    private void Awake()
    {
        AnimFire = GetComponent<Animator>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
>>>>>>> 9f1dd67f328b175882acfed9372aeaf7852236f4:Scripts/Enemy/FireBallScript.cs
