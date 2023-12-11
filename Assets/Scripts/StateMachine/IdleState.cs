using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IdleState : State
{
    [SerializeField] ChaseState chaseState;
    [SerializeField] bool canSeePlayer;

    public override State RunCurrentState()
    {
        if (canSeePlayer)
        {
            Debug.Log("I SEE YOU");
            return chaseState;
        }
        else
        {
            Debug.Log("ZZZ");
            return this;
        }
    }
}
