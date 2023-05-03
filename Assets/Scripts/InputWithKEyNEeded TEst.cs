using System.Collections;
using System.Collections.Generic;
using UnityEditor.Rendering.Universal;
using UnityEngine;

public class InputWithKEyNEededTEst : Interactable, IInteractable
{
    InputManager inputManager;
    // Start is called before the first frame update
    void Start()
    {
        inputManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<InputManager>();  
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Interact()
    {
        InputManager.Instance.DisplayInput();
        
    }
}
