using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TheKiwiCoder;

public class TargetPosition : ActionNode
{
    DataEnemies dataEnemies;
    Vector3 min = Vector3.one;
    Vector3 max = Vector3.one;
    protected override void OnStart() {
        dataEnemies = context.gameObject.GetComponent<DataEnemies>();
        dataEnemies.target.x = Random.Range(min.x, max.x);
        dataEnemies.target.y = Random.Range(-min.y, max.y);
    }

    protected override void OnStop() {
    }

    protected override State OnUpdate() {
        
        return State.Success;
    }
}
