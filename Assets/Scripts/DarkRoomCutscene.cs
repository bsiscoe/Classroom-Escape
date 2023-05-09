using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;
using UnityEngine.Playables;

public class DarkRoomCutscene : MonoBehaviour
{
    PlayableDirector cutscene;
    PlayerAttributes playerAttributes;
    DialogueAPI dialogue;
    Animator playerAnim;
    bool firstTime;
    public AudioSource cutsceneAudio; 

    private void Awake()
    {
        cutscene = GetComponent<PlayableDirector>();
        dialogue = GetComponent<DialogueAPI>();
        playerAttributes = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
        playerAnim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
        firstTime = true;
    }

    IEnumerator PlayCustscene()
    {
        playerAttributes.ChangeState(PlayerState.forcedReading);
        cutsceneAudio.Play();
        cutscene.Play();
        while (cutscene.state == PlayState.Playing)
        {
            yield return null;
        }
        dialogue.PlayDialogue();
        while (!dialogue.DialogueOver())
        {
            yield return null;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!firstTime)
        {
            return;
        }
        else if (collision.gameObject.HasTag("Player") && !collision.isTrigger)
        {
            firstTime = false;
            StartCoroutine(PlayCustscene());
        }
    }
}
