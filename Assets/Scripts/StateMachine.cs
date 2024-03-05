using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StateMachine
{
    //Testing changes ffrom unity
    State currentState;
    Humanoid parent;

    //-------------------
    public State GetState()
    {
        return currentState;
    }

    public void Initialize(State startState, Humanoid parent)
    {
        this.parent = parent;
        this.currentState = startState;
        currentState.Enter();
    }

    public void SetState(State newState)
    {
        if (currentState != null)
            currentState.Exit();
        currentState = newState;
        currentState.parent = this.parent;
        currentState.Enter();
    }

    public void Update()
    {
        currentState.Update(Time.deltaTime);
    }

    public void FixedUpdate()
    {
        currentState.FixedUpdate();
    }

    void StateTriggerEnter2D(Collider2D other) { }
}
