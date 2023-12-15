using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MovePosition : ActionNode
{
    DataEnemies dataEnemies;
    public float stoppingDistance = 0.1f;
    public float tolerance = 1.0f;
    protected override void OnStart() {
        dataEnemies = new DataEnemies();
        context.agent.stoppingDistance = stoppingDistance;
        context.agent.speed = dataEnemies.getSpeed();
        context.agent.destination = blackboard.moveToPosition;
        Debug.Log(blackboard.moveToPosition);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if (context.agent.remainingDistance < tolerance)
        {
            return State.Success;
        }

        return State.Running;
    }
}
