using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class InputManager : MonoBehaviour
{
    public static InputManager Instance;

    GameObject inputObject;
    InputField inputField;
    TextMeshProUGUI placeholder;

    public TMP_InputValidator inputValidator;

    public string input { get; private set; }

    // Start is called before the first frame update
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
        inputObject = GameObject.FindGameObjectWithTag("InputField");
        inputField = inputObject.GetComponent<InputField>();
       // placeholder = inputObject.transform.GetChild(0).transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        placeholder = inputObject.GetComponentsInChildren<TextMeshProUGUI>()[0];
        
        inputObject.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.J))
        {
            DisplayInput();
        }
        if (Input.GetKeyDown(KeyCode.H))
        {
            SetPlaceholder("Hello!");
        }
    }

    public void DisplayInput()
    {
        inputObject.SetActive(true);
    }

    public void HideInput()
    {
        inputObject.SetActive(false);
    }

    public void SetValidation(string filepath)
    {
        
    }

    public void SetPlaceholder(string text)
    {
        placeholder.text = text;
    }

    public void ReadStringInput(string input)
    {
        this.input = input;
        print($"The input is {this.input}");
    }
}
