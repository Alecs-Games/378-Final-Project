using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class FollowLeader : State
{
    Coroutine c;
    GameObject leader;

    public FollowLeader(GameObject leader)
    {
        this.leader = leader;
    }

    public override void Update(float deltaTime) { }

    public override void FixedUpdate() { }

    public override void StateTriggerEnter2D(Collider2D other) { }

    public override void Enter()
    {
        //MonoBehaviour.print(parent);
        c = parent.StartCoroutine(Follow());
    }

    public override void Exit()
    {
        parent.StopCoroutine(c);
    }

    public IEnumerator Follow()
    {
        while (true)
        {
            if (Vector2.Distance(parent.transform.position, leader.transform.position) > 2)
            {
                parent.MoveToward((Vector2)leader.transform.position, 0f);
            }
            else
                parent.Stop();
            yield return new WaitForSeconds(Random.Range(0.5f, 2f));
        }
    }
}
