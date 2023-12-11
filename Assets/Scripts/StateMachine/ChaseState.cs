using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    [SerializeField] AttackState attackState;
    [SerializeField] bool inRange;
    public override State RunCurrentState()
    {
        if(inRange) 
        {
            Debug.Log("I can Attack you");
            return attackState;
        }
        else
        {
            Debug.Log("i chase");
            return this;
        }
    }
}
