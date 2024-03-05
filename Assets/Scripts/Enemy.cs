using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    public bool isLeader;
    public List<GameObject> group;

    public new void Start()
    {
        base.Start();
    }

    #region Group Management
    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            UpdateGroup(true, other.gameObject);
        }
        else if (other.gameObject.CompareTag("Player"))
        {
            OnPlayerSpotted(other.gameObject);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.GetComponent<Enemy>())
        {
            UpdateGroup(false, other.gameObject);
        }
    }

    // Update is called once per frame
    void UpdateGroup(bool add, GameObject o)
    {
        if (add)
        {
            group.Add(o);
        }
        else
        {
            group.Remove(o);
        }
    }
    #endregion

    public virtual void OnPlayerSpotted(GameObject player) { }
}
