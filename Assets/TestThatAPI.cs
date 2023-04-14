using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThatAPI : DialogueAPI, IInteractable
{
    // Start is called before the first frame update
    void Start()
    {
        InitialzieText();
       // DebugTestOutput();
        StartCoroutine(PlayDialogue());
    }

    public void Interact()
    {

    }
}
