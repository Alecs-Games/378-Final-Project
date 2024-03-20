using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThankYouText : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        if (
            !(
                GameManager.instance.catRescued
                && GameManager.instance.dogRescued
                && GameManager.instance.lizardRescued
            )
        )
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() { }
}
