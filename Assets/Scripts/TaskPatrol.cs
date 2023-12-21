using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using BehaviorTree; 

public class TaskPatrol : Node
{
    [SerializeField] private Transform _transform;
    private Animator EnemyAnim;
    private Transform[] _waypoints;

    [SerializeField] private Transform enemy;
    [SerializeField] private float speed;
    private Vector3 initScale;
    private bool movingLeft;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void Update()
    {
        if (movingLeft)
        {
            MoveInDirection(-1);
            
        }
        else
        {
            MoveInDirection(1);

        }
        
    }

    private void MoveInDirection(int direction)
    {
        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction, 
            initScale.y * direction, initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * speed,enemy.position.y,enemy.position.z);
    }

    

}
