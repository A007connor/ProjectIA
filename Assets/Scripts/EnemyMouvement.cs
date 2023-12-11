using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMouvement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    private NavMeshAgent enemy;

    [SerializeField]
    private float lookRadius;

    

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private Animator EnemyAnim;

    [SerializeField]
    private float wanderRingWaitTime;

    // Start is called before the first frame update
    void Start()
    {
        enemy = GetComponent<NavMeshAgent>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Vector3.Distance(player.position,transform.position) < lookRadius)
        {
            enemy.SetDestination(player.position);
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }
}
