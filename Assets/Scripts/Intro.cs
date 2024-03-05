using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Intro : MonoBehaviour
{
    TextMesh text;

    void Start()
    {
        text = GetComponent<TextMesh>();
        StartCoroutine(IntroText());
    }

    IEnumerator WaitForKey(KeyCode key)
    {
        bool done = false;
        while (!done)
        {
            if (Input.GetKeyDown(key))
            {
                done = true;
            }
            yield return null;
        }
    }

    IEnumerator IntroText()
    {
        text.text = "Princess Aminta's Grand Adventure";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "The Kingdom of Pelesmos was once a peaceful land.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "Princess Aminta ruled over the land with her three pets,\nTina the cat, Glomper the lizard, and Wuff the dog.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "But one day, the Evil Blobs invaded the land!\n They broke into the palace and kidnapped the princess’s pets!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "Determined to save her pets, Princess Aminta sets off\n on a journey across the kingdom.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "She must brave the forests and explore her kingdom to find\n the three caves where her beloved pets are held captive (Move using arrow keys).";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "She must battle the Evil Blobs by slashing them with her sword (Z).";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "Game by Alec Evans and Emma Sheffo\nFont used - m6x11 by Daniel linssen\nGood luck!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        SceneManager.LoadScene("map");
    }
}
