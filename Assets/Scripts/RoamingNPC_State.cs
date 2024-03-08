using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Include a trigger to get OnHumanoidNearby() calls when a humanoid enters
//Override OnHumanoidNearby() for state transitions

public class RoamingNPC_State : State
{
    Coroutine e;
    Vector2 turnAroundFrequencyRange;

    public RoamingNPC_State(Vector2 turnAroundFrequencyRange)
    {
        //returns upper and lower bounds for NPC turning around
        //town NPC's use 2.0f
        this.turnAroundFrequencyRange = turnAroundFrequencyRange;
    }

    public override void Update(float deltaTime) { }

    public override void FixedUpdate() { }

    public override void StateTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Humanoid>())
        {
            OnHumanoidNearby(other.gameObject);
        }
    }

    public virtual void OnHumanoidNearby(GameObject other) { }

    public override void Enter()
    {
        e = parent.StartCoroutine(Chill());
    }

    public override void Exit()
    {
        parent.StopCoroutine(e);
        parent.Stop();
    }

    public IEnumerator Chill()
    {
        while (true)
        {
            Vector2 movement;
            if (Random.Range(0, 2) == 1)
            {
                movement = Vector2.zero;
            }
            else
            {
                movement = new Vector2((int)Random.Range(-1, 2), (int)Random.Range(-1, 2));
            }
            parent.Move(movement, 0.75f);
            yield return new WaitForSeconds(
                Random.Range(turnAroundFrequencyRange.x, turnAroundFrequencyRange.y)
            );
        }
    }
}
