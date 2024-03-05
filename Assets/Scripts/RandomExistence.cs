using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomExistence : MonoBehaviour
{
    public float disappearchance = 50;
    public Object obj;
    public bool doneIt = false;

    // Use this for initialization
    void Awake()
    {
        if (obj == null)
            obj = this.gameObject;
        DoIt();
    }

    void DoIt()
    {
        if (!doneIt && Random.Range(1, 100) < disappearchance)
        {
            Debug.Log("Despawned " + gameObject.name);
            Destroy(obj);
        }
        else
            doneIt = true;
    }
}
