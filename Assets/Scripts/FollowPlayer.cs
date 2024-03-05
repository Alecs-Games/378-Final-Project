using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//attach to friend
public class FollowPlayer : State
{
    Coroutine c;

    public override void Update(float deltaTime)
    {
        if (((Pet)parent).nearPlayer && parent.isMoving)
        {
            parent.Stop();
        }
    }

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
            if (!((Pet)parent).nearPlayer)
            {
                parent.MoveToward((Vector2)GameManager.instance.player.transform.position, 0f);
            }
            else
            {
                parent.SetMovement(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), 0);
            }
            yield return new WaitForSeconds(Random.Range(0.25f, 0.5f));
        }
    }
}
