using System.Collections;
using System.Collections.Generic;
using UnityEditor.Searcher;
using UnityEngine;
using UnityEngine.Rendering.Universal;

public class GlobalLightSwitch : MonoBehaviour, IInteractable
{
    private Light2D globalLight;

    private Light2D[] playerLights; // Contains all Light2D objects in all player children (flashlight/playerlight)

    private Light2D playerLight;
    // Start is called before the first frame update
    void Start()
    {
        globalLight = GameObject.FindGameObjectWithTag("GlobalLight").GetComponent<Light2D>();
        playerLights = GameObject.FindGameObjectWithTag("Player").GetComponentsInChildren<Light2D>();
        foreach (Light2D light in playerLights)
        {
            if (light.name.CompareTo("Player Light") == 0)
            {
                playerLight = light;
            }
        }
    }

    // Toggles global lightswitch
    public void Interact()
    {
        globalLight.enabled = !globalLight.enabled;
        playerLight.enabled = !playerLight.enabled;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
