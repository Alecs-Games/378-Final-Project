using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hearts : MonoBehaviour
{
    public SpriteRenderer[] hearts;

    // Start is called before the first frame update
    void Start()
    {
        UpdateHearts();
    }

    // Update is called once per frame
    public void UpdateHearts()
    {
        for (int i = 0; i < GameManager.instance.player.health; i++)
        {
            hearts[i].enabled = true;
        }
        for (int i = GameManager.instance.player.health; i < 10; i++)
        {
            hearts[i].enabled = false;
        }
    }
}
