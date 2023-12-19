using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class RangedEnemy : MonoBehaviour
{
    [Header("Attacks Parameters")]
    [SerializeField] private float attack;
    [SerializeField] private float range;
    [SerializeField] private int damage;

    [SerializeField] private Transform firePoint;
    [SerializeField] private GameObject[] fireBalls;

    [Header ("Collider Parameters")]
    [SerializeField] private float colliderDistance;
    [SerializeField] private BoxCollider2D boxCollider;

    [Header("Player Layer")]
    [SerializeField] private LayerMask playerLayer;
    private float cooldownTimer = Mathf.Infinity;

    private Animator enemyAnim;

    private EnemyPatrol enemyPatrol;

    // Start is called before the first frame update
    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyPatrol = GetComponent<EnemyPatrol>();
    }

    // Update is called once per frame
    private void Update()
    {
        cooldownTimer = Time.time;
        if (PlayerInSight())
        {
            if (cooldownTimer >= attack)
            {
                cooldownTimer = 0;
                enemyAnim.SetTrigger("RangedAttack");
            }
        }
        if (enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }

    private void RangedAttack()
    {
        cooldownTimer = 0;
        fireBalls[FindFireBall()].transform.position = firePoint.position;
        fireBalls[FindFireBall()].GetComponent<FireBallScript>().ActivateFire();
    }

    private int FindFireBall()
    {
        for(int i = 0; i < fireBalls.Length;i++)
        {
            if (fireBalls[i].activeInHierarchy)
            {
                return i;
            }
        }
        return 0;
    }

    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right * range * transform.localScale.x,
            new Vector3(boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z)
            , 0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }
}
