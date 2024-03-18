using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Upgrade : MonoBehaviour
{
    public int cost;
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        if (
            (type == "shoe" && !GameManager.instance.shoeUpgradeAvailable)
            || (type == "sword") && !GameManager.instance.swordUpgradeAvailable
        )
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Player"))
        {
            if (GameManager.instance.player.coins >= cost)
            {
                GameManager.instance.player.AddCoins(-cost);
                GameManager.instance.player.ApplyUpgrade(type);
                if (type == "shoe")
                {
                    GameManager.instance.shoeUpgradeAvailable = false;
                }
                else if (type == "sword")
                {
                    GameManager.instance.swordUpgradeAvailable = false;
                }
                Destroy(gameObject);
            }
        }
    }
}
