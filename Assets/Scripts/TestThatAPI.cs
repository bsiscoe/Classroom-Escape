using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestThatAPI : DialogueAPI, IInteractable
{

    public void Interact()
    {
        StartCoroutine(PlayDialogue());
    }
}
