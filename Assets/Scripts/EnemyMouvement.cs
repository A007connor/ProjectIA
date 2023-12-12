using System.Collections;
using System.Collections.Generic;
using TheKiwiCoder;
using UnityEngine;
using UnityEngine.AI;

public class EnemyMouvement : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    [SerializeField]
    public NavMeshAgent Orc;

    [SerializeField]
    private float lookRadius = 10f;

    [SerializeField]
    private float walk;

    [SerializeField]
    private float Chase;

    private bool hasDestination;

    [SerializeField] private float wanderinWaitTimeMin;
    [SerializeField] private float wanderinWaitTimeMax;

    [SerializeField] private Animator EnemyAnim;

    void Start()
    {

        EnemyAnim = GetComponent<Animator>();
        Orc = GetComponent<NavMeshAgent>();
       
    }

    void Update()
    {
        if(Vector3.Distance(transform.position,player.position) < lookRadius)
        {
            Orc.SetDestination(player.position);

        }
        else
        {
            if(Orc.remainingDistance < 0.75f && !hasDestination)
            {
                StartCoroutine(GetNewDestination());
            }
        }
        EnemyAnim.SetFloat("Speed", Orc.velocity.magnitude);
    }

    IEnumerator GetNewDestination()
    {
        hasDestination = true;
        yield return new WaitForSeconds(Random.Range(wanderinWaitTimeMin, wanderinWaitTimeMax));

        Vector3 nextDestination = transform.position;
        nextDestination += Random.Range(wanderinWaitTimeMin,wanderinWaitTimeMax) * new Vector3(Random.Range(-1f, 1), 0f, Random.Range(-1f, 1f)).normalized;

        NavMeshHit hit;
        if (NavMesh.SamplePosition(nextDestination,out hit,wanderinWaitTimeMax,NavMesh.AllAreas))
        {
            Orc.SetDestination(hit.position);
        }
        hasDestination = false;
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lookRadius);


    }
}
