using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WormDeathTrigger : MonoBehaviour
{
    bool eating;

    void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.isTrigger && !eating)
        {
            StartCoroutine(Consume(other.gameObject));
        }
    }

    IEnumerator Consume(GameObject target)
    {
        eating = true;
        for (int i = 0; i < 3; i++)
        {
            target.transform.position = transform.position;
            foreach (Component c in target.GetComponents<Component>())
            {
                if (!(c is SpriteRenderer || c is Transform))
                {
                    Destroy(c);
                }
            }
            target.transform.localScale = target.transform.localScale * 0.7f;
            yield return new WaitForSeconds(0.2f);
        }
        Destroy(target);
        eating = false;
    }
}
