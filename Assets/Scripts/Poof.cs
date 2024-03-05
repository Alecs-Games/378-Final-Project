using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Poof : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(Disappear());
    }

    // Update is called once per frame
    public IEnumerator Disappear()
    {
        yield return new WaitForSeconds(0.3f);
        Destroy(gameObject);
    }
}
