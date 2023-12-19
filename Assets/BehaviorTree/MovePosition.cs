using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;
using Unity.VisualScripting;

public class MovePosition : ActionNode
{
    DataEnemies dataEnemies;

    protected override void OnStart() {
        dataEnemies = context.gameObject.GetComponent<DataEnemies>();
        dataEnemies.setDestination(dataEnemies.getTarget());
        
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(dataEnemies.getCurrentPosition() == dataEnemies.getDestination() && !dataEnemies.getInRange())
        {
            return State.Success;
        }
        if (dataEnemies.getInRange())
        {
            return State.Failure;
        }

        return State.Running;
    }
}
