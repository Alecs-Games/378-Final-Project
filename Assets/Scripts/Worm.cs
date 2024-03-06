using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Worm : Enemy
{
    bool searching;
    Coroutine c;

    [SerializeField]
    CircleCollider2D MouthHole;

    [SerializeField]
    Collider2D bounds;
    bool outOfBounds;

    new void Start()
    {
        base.Start();
        StartCoroutine(Patrol());
    }

    void FoodFound(GameObject g)
    {
        if (c == null)
        {
            if (g.GetComponent<Humanoid>())
            {
                c = StartCoroutine(Chase(g.transform));
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        print(other);
        if (other == bounds)
        {
            print("Left bounds");
            outOfBounds = true;
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other == bounds)
        {
            outOfBounds = false;
        }
    }

    IEnumerator Chase(Transform target)
    {
        Animate(0, true, 0.2f, true);
        print("Worm Chasing " + target);
        while (Vector2.Distance(transform.position, target.position) > 0.5f)
        {
            WormMove(target.position - transform.position, 0f);
            yield return new WaitForSeconds(0.3f);
        }
        WormMove(Vector2.zero, 0.5f);
        yield return new WaitForSeconds(1f);
        Animate(1, false, 0.2f, true);
        yield return new WaitForSeconds(0.2f);
        MouthHole.enabled = true;
        yield return new WaitForSeconds(0.5f);
        MouthHole.enabled = false;
        yield return new WaitForSeconds(1.0f);
        Animate(0, true, 0.25f, true);
        c = null;
        StartCoroutine(Patrol());
    }

    IEnumerator Patrol()
    {
        while (c == null)
        {
            WormMove(new Vector2(one(), one()), 1f);
            yield return new WaitForSeconds(0.5f);
        }
    }

    void WormMove(Vector2 dir, float speed)
    {
        if (outOfBounds)
        {
            SetMovement(bounds.transform.position - transform.position, 0f);
        }
        else
        {
            SetMovement(dir, speed);
        }
    }

    int one()
    {
        return Random.Range(0, 2) == 0 ? -1 : 1;
    }
}
