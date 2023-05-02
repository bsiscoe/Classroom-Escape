using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DialogueAPI))]
public class StartDialogue : Interactable, IInteractable
{
    DialogueAPI dialogue;
    void Start()
    {
        dialogue = GetComponent<DialogueAPI>();
    }
    public void Interact()
    {
        dialogue.PlayDialogue();
    }
}
