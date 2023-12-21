<<<<<<< HEAD
<<<<<<< HEAD:Assets/Scripts/Enemy/EnemyMovement.cs
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    private Animator enemyAnim;

    private EnemyPatrol enemyPatrol;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer = Time.time;
        if(PlayerInSight())
        {
            if(cooldownTimer >= attack)
            {
                cooldownTimer = 0;
                enemyAnim.SetTrigger("Attack");
            }
        }
        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right* range * transform.localScale.x,
            new Vector3( boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z )
            ,0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }   
}
=======
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.GraphicsBuffer;

public class EnemyMovement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    public GameObject fireBall;

    [SerializeField]
    public Transform fire;

    [SerializeField]
    private NavMeshAgent enemy;

    [SerializeField]
    private Rigidbody2D rb;

    [SerializeField]
    private float lookRadius;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private Animator EnemyAnim;

    public float attackRange = 2.2f;

    public float attackRepeat = 1;
    private float attackTime = 2;

    private bool hasDestination;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();

    }

    void Update(float v)
    {
        float distanceTarget = Vector2.Distance(transform.position, player.transform.position);

        if (distanceTarget < walkSpeed && distanceTarget > attackRange)
        {
            Chase();
        }
        if (distanceTarget < attackRange)
        {
            attack();

        }
    }

    void Chase()
    {
        EnemyAnim.SetBool("Walking", true);
        enemy.destination = Target.position;
    }

    void attack()
    {
        enemy.destination = Target.position;

        if (Time.time > attackTime)
        {
            EnemyAnim.SetBool("Attack", true);
            //playerHealth.playerHurt(10);
            attackTime = Time.time + attackRepeat;
            Instantiate(fireBall, fire.position, Quaternion.identity);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}
>>>>>>> 9f1dd67f328b175882acfed9372aeaf7852236f4:Scripts/Enemy/EnemyMovement.cs
=======
using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;


public class EnemyMovement : MonoBehaviour
{
    [SerializeField] private float attack;
    [SerializeField] private float range;
    [SerializeField] private float colliderDistance;
    [SerializeField] private int damage;
    private Animator enemyAnim;

    private EnemyPatrol enemyPatrol;

    [SerializeField]
    private BoxCollider2D boxCollider;

    [SerializeField]
    private LayerMask playerLayer;

    private float cooldownTimer = Mathf.Infinity;

    private void Awake()
    {
        enemyAnim = GetComponent<Animator>();
        enemyPatrol = GetComponentInParent<EnemyPatrol>();
    }

    private void Update()
    {
        cooldownTimer = Time.time;
        if(PlayerInSight())
        {
            if(cooldownTimer >= attack)
            {
                cooldownTimer = 0;
                enemyAnim.SetTrigger("Attack");
            }
        }
        if(enemyPatrol != null)
        {
            enemyPatrol.enabled = !PlayerInSight();
        }
    }
    private bool PlayerInSight()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center + transform.right* range * transform.localScale.x,
            new Vector3( boxCollider.bounds.size.x * range, boxCollider.bounds.size.y, boxCollider.bounds.size.z )
            ,0, Vector2.left, 0, playerLayer);
        return hit.collider != null;
    }   
}
>>>>>>> 20b55a1fa3e350377f96424e821737b68b351a5a
