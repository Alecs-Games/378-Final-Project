using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PrincessCage : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (
            (
                GameManager.instance.catRescued
                && GameManager.instance.dogRescued
                && GameManager.instance.lizardRescued
            )
        )
        {
            print("Opening princess cage");
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() { }
}
