using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss_Minotaur : MonoBehaviour
{
    [SerializeField] LayerMask wallLayer;
    [SerializeField] int maxhp = 8;
    [SerializeField] int currentHp;
    [SerializeField] StateManager currentState;
    [SerializeField] Transform playerTransform;
    [SerializeField] float moveSpeed;
    [SerializeField] float maxMoveSpeed;
    [SerializeField] float chargeSpeed;
    [SerializeField] float maxChargeSpeed;
    [SerializeField] float maxCooldown;
    [SerializeField] float currentcooldown;
    [SerializeField] ChaseState chaseState;
    [SerializeField] AttackState attackState;
    [SerializeField] IdleState idleState;
    TestSpawer testSpawer;
    
    Vector3 chargeDirection;
    


    // Start is called before the first frame update
    void Start()
    {
        currentHp = maxhp;
        moveSpeed = maxMoveSpeed;
        chargeSpeed = maxMoveSpeed;
        currentcooldown = maxCooldown;
    }

    // Update is called once per frame
    void Update()
    {
        ChangeState();
        
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
        if(currentcooldown < maxCooldown)
        {
            moveSpeed = 0;
            chargeSpeed = 0;
            currentcooldown+= 1*Time.deltaTime;
        }
        else
        {
            moveSpeed = maxMoveSpeed;
            chargeSpeed = maxChargeSpeed;
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
            currentcooldown = 0;
        }

    }
    void Death()
    {
        Destroy(gameObject);
        //testSpawer.killedBoss[1] = true;
        //testSpawer.SaveGame();

    }
    void ChangeState()
    {
        float distanceToPlayer = Vector2.Distance(transform.position, playerTransform.position);
        if (distanceToPlayer > 10f) currentState.SwitchNextState(idleState);
        if (distanceToPlayer <= 6f) currentState.SwitchNextState(chaseState);
        if (distanceToPlayer <= 4f) currentState.SwitchNextState(attackState);
    }
    public void setPlayerTransform (Transform PlayerTransform) { playerTransform = PlayerTransform; }
}
