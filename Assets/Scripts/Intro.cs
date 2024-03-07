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
        text.text = "Welcome to the early build of Princess Aminta's\nEven Grander Adventure!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "The Evil Blobs are back, and they've brought two\nscary new enemy types with them, with more to come.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "They've captured Princess Penelope and her pets,\nso Princess Aminta must travel to a new kingdom to rescue her beloved!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "This is an extremely early beta version with two locations to visit.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "Use the arrow keys to move and Z to attack.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "by Alec Evans, Emma Sheffo, and David Quach\nFont used - m6x11 by Daniel Linssen\nGood luck!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        SceneManager.LoadScene("map");
    }
}
