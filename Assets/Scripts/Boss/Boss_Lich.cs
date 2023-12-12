using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss_Lich : MonoBehaviour
{
    [SerializeField] StateManager currentState;
    [SerializeField] IdleState idleState;
    [SerializeField] AttackState attackState;
    [SerializeField] float maxCouldown;
    [SerializeField] float currentCouldown;
    [SerializeField] LayerMask invocationLayer;
    [SerializeField] float maxHp;
    [SerializeField] float currentHp;
    void Start()
    {
        currentCouldown = maxCouldown;
        currentHp = maxHp;
    }

    private void Update()
    {
        ChangeState();
        if (currentState.GetState() is AttackState)
        {

        }
        if(currentHp <= 0) Death();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (((1 << collision.gameObject.layer) & invocationLayer) != 0)
        {
            currentHp -= 1;
            
        }
    }

    void ChangeState()
    {
        if (currentCouldown == maxCouldown)
        {
            currentCouldown = 0;
            currentState.SwitchNextState(attackState);
        }
        if (currentCouldown != maxCouldown)
        {
            currentCouldown++;
            currentState.SwitchNextState(idleState);
        }
    }

    void Death()
    {
        Destroy(gameObject);
    }
}
