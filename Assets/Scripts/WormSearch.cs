using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSearch : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (!other.isTrigger)
            transform.parent.gameObject.SendMessage("FoodFound", other.gameObject);
    }
}
