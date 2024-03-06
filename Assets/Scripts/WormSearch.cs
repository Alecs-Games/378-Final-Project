using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormSearch : MonoBehaviour
{
    void OnTriggerStay2D(Collider2D other)
    {
        if (
            other.gameObject.GetComponent<Humanoid>()
            || other.gameObject.GetComponent<Food>()
            || other.gameObject.CompareTag("Foliage")
        )
        {
            transform.parent.gameObject.SendMessage("FoodFound", other.gameObject);
        }
    }
}
