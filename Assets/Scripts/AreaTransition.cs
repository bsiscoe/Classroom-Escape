using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AreaTransition : MonoBehaviour
{
    public bool hasFadeEffect;
    public Vector2 newCamMinPos;
    public Vector2 newCamMaxPos;
    public Vector3 newPlayerPosition;
    GameObject player;
    CameraController cam;
    Animator fade;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        cam = Camera.main.GetComponent<CameraController>();
        fade = GameObject.FindGameObjectWithTag("Fade").GetComponent<Animator>();
    }
    private void OnTriggerEnter2D(Collider2D other)
    {
        StartCoroutine(StartTransition());
    }

    IEnumerator StartTransition()
    {
        PlayerAttributes attributes = player.GetComponent<PlayerAttributes>();
        attributes.currentState = PlayerState.transition;

        if (hasFadeEffect)
        {
            fade.SetTrigger("Start");
        }
        yield return new WaitForSeconds(1f);
        fade.ResetTrigger("Start");

        player.transform.position = newPlayerPosition;
        cam.minPosition = newCamMinPos;
        cam.maxPosition = newCamMaxPos;

        if (hasFadeEffect) 
        {
            fade.SetTrigger("End");
        }
        yield return new WaitForSeconds(1f);

        attributes.currentState = PlayerState.idle;
    }
}
