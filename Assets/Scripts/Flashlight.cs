using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Rendering.Universal;
using UnityEngine.Rendering.Universal;

public class Flashlight : MonoBehaviour
{
    Light2D flashlight;
    private Animator anim;
    public int batteryCount;
    public int currentBatteryCharge;

    private void Start()
    {
        flashlight = GetComponent<Light2D>();
        anim = GetComponent<Animator>();
    }

    public void ToggleFlashLight()
    {
        flashlight.enabled = !flashlight.enabled;
    }

    private void ResetFlashlightBool()
    {
        foreach (AnimatorControllerParameter parameter in anim.parameters)
        {
            anim.SetBool(parameter.name, false);
        }
    }

    public void UpdateFlashlightDirection(Vector3 direction)
    {
        if (direction.y < 0)
        {
            ResetFlashlightBool();
            anim.SetBool("down", true);
        }
        else if (direction.y > 0)
        {
            ResetFlashlightBool();
            anim.SetBool("up", true);
        }
        else if (direction.x > 0)
        {
            ResetFlashlightBool();
            anim.SetBool("right", true);
        }
        else if (direction.x < 0)
        {
            ResetFlashlightBool();
            anim.SetBool("left", true);
        }
    }
}
