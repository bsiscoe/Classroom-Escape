using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Flashlight : InteractableDialogue, IInteractable
{    public void Interact()
    {
        playDialogue();
        if (done)
        {
            
            Destroy(this.transform.parent.gameObject);
        }
    }
}
