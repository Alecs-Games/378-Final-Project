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
            "So far there are three caves to visit\nand the available portion of map has expanded from the last demo.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "In addition, you can finally \nsprint by holding left shift!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "You might notice the place where a \nshop will later be, as we plan to include a currency system to upgrade your attacks.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text = "Use the arrow keys to move and Z to attack.";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        text.text =
            "by Alec Evans, Emma Sheffo, and David Quach\nFont used - m6x11 by Daniel Linssen\nGood luck!";
        yield return StartCoroutine(WaitForKey(KeyCode.Z));
        SceneManager.LoadScene("map");
    }
}
