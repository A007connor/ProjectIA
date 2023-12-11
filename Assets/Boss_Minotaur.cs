using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Minotaur : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] int maxhp = 8;
    [SerializeField] int currentHp;
    [SerializeField] StateManager currentState;
    [SerializeField] Transform playerTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] ChaseState chaseState;
    [SerializeField] AttackState attackState;


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
        }
        if(currentState.GetState() is AttackState)
        {
            //transform.Translate(playerTransform.transform.position* 3*Time.deltaTime);
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
        if (distanceToPlayer <= 5f) currentState.SwitchNextState(chaseState);
        if (distanceToPlayer <= 3f) currentState.SwitchNextState(attackState);
    }

}
