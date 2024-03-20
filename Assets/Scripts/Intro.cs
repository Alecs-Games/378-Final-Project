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
        text.text =
            "On her grand adventure in the kingdom of Pelesmos,\n Princess Aminta thwarted the Evil Blobs and rescued her pets.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "Now, our princess is travelling to the magic kingdom \nof Kelia to embark on the grandest adventure of all... Love!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "Upon arriving at the castle, she learns that her betrothed, \nPrincess Penelope, has been kidnapped!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "That's right- the Evil Blobs are back, and they've captured\n Princess Penelope and her pets.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "If you can rescue Penelope's magical pets, they can cast a \nspell to help free their Princess Penelope.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "Princess Aminta must use the arrow keys to move, hold shift\n to run, and press Z to attack the terrible monsters.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "Time to save the Kingdom of Kelia!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "by Alec Evans, Emma Sheffo, and David Quach\nFont used - m6x11 by Daniel Linssen\nGood luck!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        SceneManager.LoadScene("castle");
    }
}
