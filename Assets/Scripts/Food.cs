using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Food : MonoBehaviour
{
    public int healValue;
    public AudioClip sound;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<Player>().UpdateHealth(healValue);
            GameManager.instance.audi.PlayOneShot(sound);
            Destroy(gameObject);
        }
    }
}
