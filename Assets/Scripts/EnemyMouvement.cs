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

    private bool destination;

    [SerializeField]
    private float walkSpeed;

    [SerializeField]
    private Animator EnemyAnim;

    [SerializeField]
    private float wanderRingWaitTimeMin;

    [SerializeField]
    private float wanderRingWaitTimeMax;

    [SerializeField]
    private float wanderRingDistanceMin;

    [SerializeField]
    private float wanderRingDistanceMax;

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
        else
        {

        }
    }

    IEnumerator GetNewDestination()
    {
        destination = true;
        yield return new WaitForSeconds(Random.Range(wanderRingWaitTimeMin, wanderRingWaitTimeMax));

        Vector3 nextDestination = player.position;
        nextDestination += new Vector3(Random.Range(wanderRingDistanceMin, wanderRingDistanceMax), 0f, Random.Range(-1f, 1)).normalized;

        NavMeshHit hit;
        if(NavMesh.SamplePosition(nextDestination,out hit,wanderRingDistanceMax,NavMesh.AllAreas))
        {
            enemy.SetDestination(hit.position);
        }
        destination = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawSphere(transform.position, lookRadius);
    }
}
