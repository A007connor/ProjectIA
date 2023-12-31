using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskPatrol : Node
{
    private Transform _transform;
    private Animator EnemyAnim;
    private Transform[] _waypoints;

    private int currentWaypointIndex = 0;

    private float waitTime = 1f;
    private float waitCounter = 0f;
    private bool waiting = false;

    public TaskPatrol(Transform transform, Transform[] waypoints)
    {
        _transform = transform;
        EnemyAnim = transform.GetComponent<Animator>();
        _waypoints = waypoints;

    }

    public override NodeState Evaluate()
    {
       if(waiting)
        {
            waitCounter += Time.deltaTime;
            if(waitCounter >= waitTime)
            {
                waiting = false;
                EnemyAnim.SetBool("Walking", true);
            }
        }
       else
        {
            Transform wp = _waypoints[currentWaypointIndex];
            if (Vector3.Distance(_transform.position,wp.position) < 0.01f)
            {
                _transform.position = wp.position;
                waitCounter = 0f;
                waiting = true;

                currentWaypointIndex = (currentWaypointIndex + 1) % _waypoints.Length;
                EnemyAnim.SetBool("Walking", false);
            }
            else
            {
                _transform.position = Vector3.MoveTowards(_transform.position, wp.position, GuardBT.speed * Time.deltaTime);
                _transform.LookAt(wp.position);
            }
            
        }
        state = NodeState.RUNNING;
        return state;
    }

}
