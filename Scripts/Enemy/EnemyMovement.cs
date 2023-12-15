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
