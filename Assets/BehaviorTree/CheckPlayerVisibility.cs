using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class CheckPlayerVisibility : DecoratorNode
{
    DataEnemies dataEnemies;
    protected override void OnStart() {
        dataEnemies = new DataEnemies();
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        if(dataEnemies.getInRange() == true) return State.Success;
        if(dataEnemies.getInRange() == false) return State.Failure;
        return State.Failure;
    }
}
