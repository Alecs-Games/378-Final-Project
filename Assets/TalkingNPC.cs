using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TalkingNPC : NPC
{
    bool talking = false;
    public bool wandering;
    public TextMeshPro text;
    public string[] dialogueBank;
    int currDialogueIndex = 0;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        if (wandering)
        {
            SetState(new RoamingNPC_State(new Vector2(1f, 3f)));
        }
        else
        {
            Animate(0, true, 0.4f, true);
        }
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        base.OnTriggerEnter2D(other);
        if (other.gameObject.CompareTag("Player"))
        {
            //print("player Spotted");
            if (!talking)
            {
                StartCoroutine(Talk(dialogueBank[currDialogueIndex]));
                currDialogueIndex += 1;
                if (currDialogueIndex > dialogueBank.Length - 1)
                {
                    currDialogueIndex = 0;
                }
            }
        }
    }

    public IEnumerator Talk(string line)
    {
        talking = true;
        text.enabled = true;
        text.text = line;
        yield return new WaitForSeconds(3f);
        text.enabled = false;
        talking = false;
    }
}
