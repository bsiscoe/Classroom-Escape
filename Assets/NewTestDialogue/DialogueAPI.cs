using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.IO;
using UnityEngine.UI;
using System.Linq;
using System;

public class DialogueAPI : Interactable
{
    [SerializeField] TextAsset dialogueText;
    [SerializeField] bool forceAllDialogue;
    GameObject textBox;
    TextMeshProUGUI textInBox;
    bool nextLine = false;
    public List<string> getBatches;
    public List<string[]> dialougeBatches = new List<string[]>();
    public int currentSet = 0;
    GameObject player;

    public void Start()
    {
        Debug.Log("Going");
        textBox = GameObject.FindGameObjectWithTag("DialogueBox");
        textInBox = GameObject.FindGameObjectWithTag("DialogueText").GetComponent<TextMeshProUGUI>();
        player = GameObject.FindGameObjectWithTag("Player");
        getBatches = dialogueText.text.Split("*--*", StringSplitOptions.RemoveEmptyEntries).ToList();
        foreach (string batch in getBatches) 
        {
            dialougeBatches.Add(batch.Split('\n'));
        }
        StartCoroutine(PlayDialogue());
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
        textBox.GetComponent<RawImage>().enabled = true;
        textInBox.enabled = true;
        player.GetComponent<PlayerAttributes>().currentState = PlayerState.reading;
        foreach (string line in dialougeBatches[currentSet]) 
        {
            textInBox.text = line;
            yield return WaitForInput();
        }
        textBox.GetComponent<RawImage>().enabled = false;
        textInBox.enabled = false;
        player.GetComponent<PlayerAttributes>().currentState = PlayerState.idle;
    }

    private void PlayUnforced()
    {
        
    }
}