using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class ChaseState : State
{
    [SerializeField] IdleState idleState;
    [SerializeField] AttackState attackState;
    [SerializeField] bool inRange;
    [SerializeField] bool outRange;

    [SerializeField] NavMeshAgent navMeshAgent;
    [SerializeField] Transform player; // R�f�rence au joueur
    [SerializeField] float detectionRange = 10f; // Port�e de d�tection du joueur
    [SerializeField] float verticalSpeed = 2f; // Vitesse de d�placement vertical
    [SerializeField] GameObject fireballPrefab; // Pr�fabriqu� de boule de feu
    [SerializeField] Transform firePoint; // Point de spawn des boules de feu

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    public override State RunCurrentState()
    {
        float distanceToPlayer = Vector3.Distance(transform.position, player.position);

        // Si le joueur est dans la zone de d�tection
        if (distanceToPlayer <= detectionRange)
        {
            // D�placer le dragon vers le joueur (horizontal)
            navMeshAgent.SetDestination(player.position);

            // D�placer le dragon vers le haut ou le bas (vertical)
            float verticalMovement = Mathf.Sin(Time.time) * verticalSpeed;

            // Utiliser le NavMeshAgent pour le mouvement vertical
            navMeshAgent.Move(Vector3.up * verticalMovement * Time.deltaTime);

            // Lancer une boule de feu
            LaunchFireball();
        }
        else
        {
            // Si le joueur n'est pas dans la zone, passer � l'�tat Idle
            return idleState;
        }

        return this;
    }

    private void LaunchFireball()
    {
        // Instancier une boule de feu au point de spawn
        Instantiate(fireballPrefab, firePoint.position, firePoint.rotation);
    }
}
