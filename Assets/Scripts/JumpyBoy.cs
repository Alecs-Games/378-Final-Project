using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JumpyBoy : NPC
{
    public GameObject hitbox;
    bool inAir;
    public SpriteRenderer shadow;
    Player player;
    public Vector3 shadowModifier;
    public GameObject attack;
    public BoxCollider2D coll;
    SpriteRenderer spr;

    public new void Start()
    {
        spr = GetComponent<SpriteRenderer>();
        base.Start();
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && other.isTrigger)
        {
            WakeUp();
            shadow.gameObject.SetActive(true);
        }
    }

    void WakeUp()
    {
        player = GameManager.instance.player;
        StartJump();
    }

    IEnumerator Jump(Vector3 target)
    {
        //gameObject.layer = 10;
        //hitbox.layer = 10;
        spr.sortingOrder = 3;

        Vector3 groundPosition = transform.position;
        float verticalChange = 0.6f;
        float verticalChangeModifier = -0.02f;
        float height = 0f;
        yield return new WaitForSeconds(0.25f);
        coll.enabled = false;
        for (int i = 0; i < 60; i++)
        { //gameObject.layer = 10;
            //hitbox.layer = 10;
            groundPosition = Vector3.MoveTowards(groundPosition, target, 0.07f);
            shadow.transform.position = groundPosition - shadowModifier;
            Vector3 ToMove = groundPosition;
            verticalChange += verticalChangeModifier;
            height += verticalChange;
            ToMove.y += height;
            transform.position = ToMove;
            if (i > 55)
            {
                attack.SetActive(true);
                coll.enabled = true;
                spr.sortingOrder = 1;
            }
            yield return new WaitForSeconds(0.03f);
        }
        attack.SetActive(false);
        shadow.transform.position = transform.position - shadowModifier;
        StartCoroutine(AfterJump());
    }

    void StartJump()
    {
        if (!inAir)
        {
            StartCoroutine(
                Jump(
                    player.transform.position
                        + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0)
                )
            );
            Animate(0, false, 0.1f, true);
            inAir = true;
        }
    }

    IEnumerator AfterJump()
    {
        Animate(1, true, 0.3f, true);
        inAir = false;
        yield return new WaitForSeconds(Random.Range(1f, 2f));
        StartJump();
    }
}
