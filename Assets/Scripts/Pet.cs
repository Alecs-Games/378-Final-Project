using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class Pet : NPC
{
    public bool nearPlayer;
    public bool rescued;
    public bool isLizard;
    public bool isCat;
    public bool isDog;
    public GameObject rescuedTextPrefab;
    public string rescueText;

    // Start is called before the first frame update
    new void Start()
    {
        base.Start();
        SetState(new State());
        if (isLizard && GameManager.instance.lizardRescued)
        {
            Destroy(gameObject);
        }
        else if (isCat && GameManager.instance.catRescued)
        {
            Destroy(gameObject);
        }
        if (isLizard)
        {
            GameManager.instance.lizardRescued = true;
        }
        else if (isCat)
        {
            GameManager.instance.catRescued = true;
        }
        else if (isDog)
        {
            GameManager.instance.dogRescued = true;
        }
    }

    IEnumerator PlayerTeleport()
    {
        while (true)
        {
            yield return new WaitForSeconds(3f);
            if (
                Vector2.Distance(GameManager.instance.player.transform.position, transform.position)
                > 10f
            )
            {
                TeleportNearPlayer();
            }
        }
    }

    IEnumerator NearPlayerCheck()
    {
        while (true)
        {
            yield return new WaitForSeconds(5f);
            if (
                Vector2.Distance(GameManager.instance.player.transform.position, transform.position)
                > 3f
            )
            {
                nearPlayer = false;
            }
        }
    }

    void TeleportNearPlayer()
    {
        print("Friend Teleporting to Player");
        transform.position =
            GameManager.instance.player.transform.position
            + new Vector3(Random.Range(-1f, 1f), Random.Range(-1f, 1f), 0);
        nearPlayer = false;
    }

    protected override void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger)
        {
            if (other.gameObject.CompareTag("Player"))
                if (!rescued)
                {
                    Activate();
                }
            nearPlayer = true;
        }
        base.OnTriggerEnter2D(other);
        GameManager.instance.dogRescued = true;
    }

    void Activate()
    {
        GameObject textObj = Instantiate(
            rescuedTextPrefab,
            transform.position + new Vector3(6, 0, 0),
            Quaternion.identity
        );
        textObj.GetComponent<TextMeshPro>().text = rescueText;
        if (isLizard)
        {
            GameManager.instance.lizardRescued = true;
        }
        if (isLizard)
        {
            GameManager.instance.lizardRescued = true;
        }
        else if (isCat)
        {
            GameManager.instance.catRescued = true;
        }
        Destroy(gameObject);
        /*
        rescued = true;
        spr.color = Color.white;
        if (isLizard)
        {
            GameManager.instance.lizardRescued = true;
        }
        else if (isCat)
        {
            GameManager.instance.catRescued = true;
        }
        else if (isDog)
        {
            GameManager.instance.dogRescued = true;
        }
        SetState(new FollowPlayer());
        GameManager.instance.AddPet(this.gameObject);
        StartCoroutine(PlayerTeleport());
        StartCoroutine(NearPlayerCheck());
        */
    }

    public void OnSceneLoaded()
    {
        nearPlayer = false;
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") && !other.isTrigger)
            nearPlayer = false;
    }
}
