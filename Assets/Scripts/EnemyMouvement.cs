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
    public GameObject fireballPrefab;

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
    public float damage = 10;

    private bool hasDestination;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        EnemyAnim = GetComponent<Animator>();
        enemy = GetComponent<NavMeshAgent>();

    }

    void Update(float v)
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        if (distanceToPlayer < walkSpeed && distanceToPlayer > attackRange)
        {
            Chase();
        }
        if (distanceToPlayer < attackRange)
        {
            attack();

        }
    }

    void Chase()
    {
        EnemyAnim.SetBool("Walking", true);
        enemy.destination = player.position;
    }

    void attack()
    {
        enemy.destination = player.position;

        if (Time.time > attackTime)
        {
            EnemyAnim.SetBool("Attack", true);
            //playerHealth.playerHurt(10);
            attackTime = Time.time + attackRepeat;
            Instantiate(fireballPrefab, fire.position, Quaternion.identity);
        }
    }

    private void FireBall()
    {
        GameObject fireball = Instantiate(fireballPrefab, transform.position, Quaternion.identity);

        FireBall fireballScript = fireball.GetComponent<FireBall>();
        if (fireballScript != null)
        {
            // Vous pouvez configurer d'autres propriétés de la boule de feu ici, si nécessaire
            fireballScript.damage = 30; // Par exemple, définir des dégâts différents pour cette boule de feu
        }

        // Calculer la direction vers le joueur
        Vector3 directionToPlayer = (player.position - transform.position).normalized;
        Rigidbody2D rbFireball = fireball.GetComponent<Rigidbody2D>();
        rbFireball.AddForce(directionToPlayer * 500f);
        Destroy(fireball, 3f);
    }

    void Dead()
    {
        EnemyAnim.SetTrigger("isDead");
        Destroy(gameObject, 1.5f);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);

    }
}