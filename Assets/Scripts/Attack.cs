using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Attack : MonoBehaviour
{
    public int damage;
    public int knockback;
    public GameObject origin;

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            Hit(other.gameObject);
        }
    }

    protected virtual void Hit(GameObject target)
    {
        if (target.GetComponent<Humanoid>())
        {
            target.GetComponent<Humanoid>().Hit(damage, knockback, origin);
        }
        else
        {
            //target.SendMessage("Hit");
        }
    }

    public virtual void Initialize(int damage, int knockback, Direction direction)
    {
        this.damage = damage;
        this.knockback = knockback;
        switch (direction)
        {
            case Direction.down:
                transform.Rotate(0, 180, 180);
                break;
            case Direction.left:
                transform.Rotate(180, 0, 90);
                break;
            case Direction.right:
                transform.Rotate(0, 0, -90);
                break;
            case Direction.up:
                transform.Rotate(0, 180, 0);
                break;
        }
        this.origin = transform.parent.gameObject;
    }
}
