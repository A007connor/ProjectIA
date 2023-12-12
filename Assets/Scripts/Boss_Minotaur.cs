using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Minotaur : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] LayerMask playerLayer;
    [SerializeField] int maxhp = 8;
    [SerializeField] int currentHp;
    [SerializeField] StateManager currentState;
    [SerializeField] Transform playerTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float chargeSpeed;
    [SerializeField] ChaseState chaseState;
    [SerializeField] AttackState attackState;
    Vector3 chargeDirection;


    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxhp;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        Debug.Log(currentState.GetState());
        if (currentState.GetState() is ChaseState)
        {
            transform.position = Vector2.MoveTowards(this.transform.position, playerTransform.position, moveSpeed * Time.deltaTime);
            chargeDirection = Vector3.zero;
        }
        if(currentState.GetState() is AttackState)
        {
            if(chargeDirection == Vector3.zero)
            {
                chargeDirection =(playerTransform.position - transform.position).normalized;
                
            }
            transform.position = Vector2.MoveTowards(transform.position,transform.position + chargeDirection, chargeSpeed * Time.deltaTime);
        }
        if (currentHp == 0)
        {
            Death();
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & wallLayer) != 0)
        {
            currentHp -= 1;
            Debug.Log("hurt");
        }

    }
    void Death()
    {
        Destroy(gameObject);
    }
    void ChangeState()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        Debug.Log(distanceToPlayer);
        if (distanceToPlayer <= 6f) currentState.SwitchNextState(chaseState);
        if (distanceToPlayer <= 4f) currentState.SwitchNextState(attackState);
    }

}
