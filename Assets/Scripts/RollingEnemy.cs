using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RollingEnemy : Enemy
{
    public GameObject target;
    GameObject damageBox;
    bool spinning;
    bool attacking;
    bool aggro;

    public new void Start()
    {
        base.Start();
        SetState(new Patrol());
        damageBox = transform.GetChild(0).gameObject;
        IdleAnimation();
    }

    void IdleAnimation()
    {
        Animate(0, true, 0.4f, true);
    }

    public new void Update()
    {
        base.Update();
        if (spinning)
        {
            transform.Rotate(0, 0, 10);
        }
    }

    public override void OnPlayerSpotted(GameObject player)
    {
        if (!aggro)
        {
            target = player;
            SetState(new State());
            StartCoroutine(Fight());
            aggro = true;
        }
    }

    IEnumerator Fight()
    {
        while (true)
        {
            bool justAttacked = false;
            if (attacking)
            {
                EndAttack();
                justAttacked = true;
            }
            if (target != null)
            {
                if (Vector2.Distance(transform.position, target.transform.position) > 5)
                {
                    MoveToward(
                        (Vector2)
                            target
                                .transform
                                .position /* +  new Vector2(Random.Range(-2,2),Random.Range(-2,2))*/
                        ,
                        0
                    );
                }
                else
                {
                    if (!justAttacked)
                    {
                        Attack(target.transform.position);
                    }
                }
            }
            justAttacked = false;
            yield return new WaitForSeconds(1.5f);
        }
    }

    void EndAttack()
    {
        damageBox.SetActive(false);
        transform.GetChild(0).gameObject.SetActive(false);
        spinning = false;
        attacking = false;
        Move(new Vector2(Random.Range(-1, 1), Random.Range(-1, 1)), 0);
        transform.rotation = Quaternion.identity;
        IdleAnimation();
    }

    void Attack(Vector2 pos)
    {
        damageBox.SetActive(true);
        transform.GetChild(0).gameObject.SetActive(true);
        MoveToward(pos, moveSpeed * 4);
        spinning = true;
        attacking = true;
        Animate(1, true, 1, true);
    }
}
