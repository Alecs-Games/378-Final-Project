using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Entrance : MonoBehaviour
{
    public string sceneName;
    public bool map;
    public Vector2 startLocation;

    // Start is called before the first frame update
    void Start() { }

    // Update is called once per frame
    void Update() { }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && other.gameObject.CompareTag("Player"))
        {
            GameManager.instance.LoadEntrance(this);
        }
    }
}
