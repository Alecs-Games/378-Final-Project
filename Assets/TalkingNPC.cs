using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TalkingNPC : NPC
{
    // Start is called before the first frame update
    void Start()
    {
        base.Start();
        SetState(new RoamingNPC_State(new Vector2(1f, 3f)));
    }
}
