using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractDoor : Interactable
{
    public Animator anim;
    private bool isOpen;

    void Start()
    {
        anim = this.transform.parent.GetComponent<Animator>();
    }

    void Update()
    {
        if (!playerInRange)
        {
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            if (!isOpen)
            {
                OpenDoor();
            }
            else
            {
                CloseDoor();
            }
        }
    }

    public void OpenDoor()
    {
        anim.SetBool("isOpen", true);
        isOpen = true;
    }

    public void CloseDoor()
    {
        anim.SetBool("isOpen", false);
        isOpen = false;
    }
}
