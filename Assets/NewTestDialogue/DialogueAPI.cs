using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;

public class DialogueAPI : Interactable
{
    [SerializeField] TextAsset dialogueText;
    [SerializeField] bool forceAllDialogue;
    [SerializeField] GameObject textBox;
    [SerializeField] TextMeshProUGUI textInBox;
    bool nextLine = false;
    public string[] lines;
    GameObject player;

    public void InitialzieText()
    {
        Debug.Log("Going");
        player = GameObject.FindGameObjectWithTag("Player");
        lines = dialogueText.text.Split('\n');
    }
    public void DebugTestOutput()
    {
        print(dialogueText.text);
    }

    public IEnumerator PlayDialogue()
    {
        if (forceAllDialogue) 
        {
            StartCoroutine(PlayForced());
        } else
        {
            PlayUnforced();
        }
        yield return null;
    }

    IEnumerator WaitForInput()
    {
        while (!nextLine)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                nextLine = true;
            }
            yield return null;
        }
        nextLine = false;
    }

    IEnumerator PlayForced()
    {
        textBox.SetActive(true);
        player.GetComponent<PlayerAttributes>().isReading = true;
        foreach (string line in lines) 
        {
            textInBox.text = line;
            yield return WaitForInput();
        }
        textBox.SetActive(false);
        player.GetComponent<PlayerAttributes>().isReading = false;
    }

    private void PlayUnforced()
    {
        
    }
}