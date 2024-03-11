using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkingNPC : NPC
{
    bool talking = false;
    public bool wandering;
    public TextMeshPro text;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        if (wandering)
        {
            SetState(new RoamingNPC_State(new Vector2(1f, 3f)));
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            print("player Spotted");
            if (!talking)
            {
                StartCoroutine(Talk());
            }
        }
    }

    public IEnumerator Talk()
    {
        talking = true;
        text.enabled = true;
        yield return new WaitForSeconds(3f);
        text.enabled = false;
        talking = false;
    }
}
