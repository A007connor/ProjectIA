using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChaseState : State
{
    [SerializeField] IdleState idleState;
    [SerializeField] AttackState attackState;
    [SerializeField] bool inRange;
    [SerializeField] bool outRange;
    public override State RunCurrentState()
    {
            
        return this;
        
    }
}
