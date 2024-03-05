using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class State
{
    public Humanoid parent;
    
    public virtual void Update(float deltaTime){}
    public virtual void FixedUpdate(){}
    public virtual void StateTriggerEnter2D(Collider2D other){}
    public virtual void Enter(){
    }
    public virtual void Exit(){}
}
