using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CastlePet : MonoBehaviour
{
    public string type;

    // Start is called before the first frame update
    void Start()
    {
        if (
            (!GameManager.instance.catRescued && type == "cat")
            || (!GameManager.instance.dogRescued && type == "dog")
            || (!GameManager.instance.lizardRescued && type == "lizard")
        )
        {
            Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update() { }
}
