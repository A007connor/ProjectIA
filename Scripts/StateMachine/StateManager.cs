using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateManager : MonoBehaviour
{
    [SerializeField] State currentState;
    // Update is called once per frame
    void Update()
    {
        RunStateMachine();
    }

    private void RunStateMachine()
    {
        State nextState =  currentState?.RunCurrentState();
        if (nextState != null) 
        {
            SwitchNextState(nextState);
        }
    }
    public State GetState()
    {
        return currentState;
    }

    public void SwitchNextState(State nextState)
    {
        currentState = nextState;
    }
}
