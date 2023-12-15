using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AttackState : State
{
    [SerializeField] ChaseState chaseState;
    [SerializeField] bool outrange;
    public override State RunCurrentState()
    {  
        return this;

    }
}
