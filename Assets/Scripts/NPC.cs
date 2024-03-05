using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPC : Humanoid
{
    public State state;

    public void SetState(State newState)
    {
        stateMachine.SetState(newState);
    }

    public State GetState()
    {
        return stateMachine.GetState();
    }

    public new void Start()
    {
        base.Start();
        //stateMachine = new StateMachine(new State(), (Humanoid)this);
        stateMachine = new StateMachine();
        stateMachine.Initialize(new State(), (Humanoid)this);
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        //stateMachine.GetState().StateTriggerEnter2D(other);
    }
}
