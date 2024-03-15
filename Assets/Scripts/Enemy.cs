using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : NPC
{
    public bool isLeader;
    public List<GameObject> group;
    public List<GameObject> drops;
    public int dropChance;

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
    public override IEnumerator Die()
    {
        if (Random.Range(0, 100) < dropChance)
        {
            print("instantiating coin");
            Instantiate(
                drops[Random.Range(0, drops.Count - 1)],
                transform.position,
                Quaternion.identity
            );
        }
        StartCoroutine(base.Die());
        yield return new WaitForSeconds(0.1f);
    }

    public virtual void OnPlayerSpotted(GameObject player) { }
}
