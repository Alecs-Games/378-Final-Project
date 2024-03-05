using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Patrol : State
{
    Enemy leaderToFollow = null;
    IEnumerator e;
    int lastGroupCount = 0;

    public override void Update(float deltaTime)
    {
        if (parent is Enemy && ((Enemy)parent).group.Count > lastGroupCount)
        {
            foreach (GameObject g in ((Enemy)parent).group)
            {
                if (g == null)
                    ((Enemy)parent).group.Remove(g);
                if (g.GetComponent<Enemy>().isLeader)
                {
                    onLeaderFound(g);
                }
            }
            lastGroupCount = ((Enemy)parent).group.Count;
        }
    }

    protected virtual void onLeaderFound(GameObject g)
    {
        ((NPC)parent).SetState(new FollowLeader(g));
        //^should be new FollowLeader(g);
    }

    public override void Enter()
    {
        e = PatrolBehaviour();
        parent.StartCoroutine(e);
    }

    public override void Exit()
    {
        parent.StopCoroutine(e);
    }

    public void OnGroupJoined(Enemy newGuy)
    {
        if (newGuy.isLeader)
        {
            leaderToFollow = newGuy;
        }
        else if (newGuy.GetState() is FollowLeader)
        {
            //leaderToFollow = ((FollowLeader)newGuy.GetState()).leader;
        }
    }

    IEnumerator PatrolBehaviour()
    {
        while (true)
        {
            Vector2 movement = new Vector2((int)Random.Range(-1, 2), (int)Random.Range(-1, 2));
            parent.Move(movement, 0);
            yield return new WaitForSeconds(2f);
        }
    }
}
