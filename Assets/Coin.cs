using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : MonoBehaviour
{
    GameObject player = null;
    public float moveTowardsSpeed;

    // Start is called before the first frame update
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (!other.isTrigger)
            {
                other.gameObject.SendMessage("GetCoin");
                Destroy(gameObject);
            }
            else
            {
                print("player near coin");
                //uncomment to enable following
                //player = other.gameObject;
            }
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            transform.position = Vector2.MoveTowards(
                transform.position,
                player.transform.position,
                moveTowardsSpeed * Time.deltaTime
            );
        }
    }
}
