using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class End : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameManager.instance.player.gameObject.SetActive(false);
        foreach (GameObject p in GameManager.instance.pets)
        {
            Destroy(p);
        }
    }
}
