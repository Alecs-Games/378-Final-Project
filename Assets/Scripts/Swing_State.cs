using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//movementEnabled must be read from specific classes
public class Swing_State : State
{
    public override void Update(float deltaTime) { }

    public override void FixedUpdate() { }

    public override void StateTriggerEnter2D(Collider2D other) { }

    public override void Enter()
    {
        parent.AttackAnimation(0f);
    }

    public override void Exit() { }
}
