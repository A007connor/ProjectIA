using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    [SerializeField] private Transform left;
    [SerializeField] private Transform right;

    [SerializeField] private float speed;

    [SerializeField] private Transform enemy;

    [SerializeField] private Vector3 initScale;
    private bool movingLeft;

    [SerializeField] private float idleDuration;
    private float idleTimer;

    [SerializeField] private Animator EnemyAnim;

    private void Awake()
    {
        initScale = enemy.localScale;
    }

    private void OnDisable()
    {
        EnemyAnim.SetBool("walking",false);
    }

    private void Update()
    {
        if(movingLeft)
        {
            if(enemy.position.x >= left.position.x)
            {
                MoveInDirection(-1);
            }
            else
            {
                DirectionChange();
            }
            
;       }
        else
        {
            if (enemy.position.x >= right.position.x)
            {
                MoveInDirection(1);
            }
            else
            {
                DirectionChange();
            }

        }
    }

    private void DirectionChange()
    {
        EnemyAnim.SetBool("walking", false);

        idleTimer += Time.deltaTime;

        if(idleTimer > idleDuration)
        {
            movingLeft = !movingLeft;
        }
        
    }

    private void MoveInDirection(int direction)
    {
        idleTimer = 0;

        EnemyAnim.SetBool("walking", true);

        enemy.localScale = new Vector3(Mathf.Abs(initScale.x) * direction,initScale.y ,initScale.z);

        enemy.position = new Vector3(enemy.position.x + Time.deltaTime * direction * speed, enemy.position.y, enemy.position.z);
    }
}
