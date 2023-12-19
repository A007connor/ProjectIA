using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class MovePlayer : ActionNode
{
    DataEnemies dataEnemies;

    protected override void OnStart()
    {
        dataEnemies = context.gameObject.GetComponent<DataEnemies>();

    }

    protected override void OnStop()
    {
    }

    protected override State OnUpdate()
    {
        dataEnemies.target = dataEnemies._player.transform.position;
        dataEnemies.setDestination(dataEnemies.getTarget());

        if (!dataEnemies.getInRange())
        {
            return State.Failure;
        }

        return State.Running;
    }
}

