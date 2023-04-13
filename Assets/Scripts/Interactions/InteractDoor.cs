using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : InteractableDialogue
{
    public Animator anim;

    void Start()
    {
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerInRange)
        {
            return;
        }
        if (Input.GetKeyDown("space"))
        {
            playDialogue();
        }
    }

    public void openDoor()
    {
        anim.SetBool("isOpen", true);
    }

    public void closeDoor()
    {

    }
}
