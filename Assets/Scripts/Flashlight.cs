using System.Collections;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UI;

public class Flashlight : MonoBehaviour
{

    private Slider chargeDisplay;
    private Light2D flashlight;
    private Animator anim;
    [HideInInspector] public bool isBatteryInfinite;
    public bool hasFlashlight;
    public int currentBatteryCharge;

    void Start()
    {
        chargeDisplay = GameObject.FindGameObjectWithTag("FlashlightCharge").GetComponent<Slider>();
        flashlight = GetComponent<Light2D>();
        anim = GetComponent<Animator>();
        isBatteryInfinite = false;
        hasFlashlight = false;
        currentBatteryCharge = 100;
        if (flashlight.enabled && !isBatteryInfinite)
        {
            InvokeRepeating("DrainBattery", 0, 5f);
        }

    }   

    void Update()
    {
        UpdateBatteryUI();
        if (!HasCharge())
        {
            flashlight.enabled = false;
        }
    }

    public void ToggleFlashLight()
    {
        if (!flashlight.enabled && HasCharge() && hasFlashlight)
        {
            flashlight.enabled = true;
            if (!isBatteryInfinite)
            {
                InvokeRepeating("DrainBattery", 0, 5f);
            }
        }
        else
        {
            flashlight.enabled = false;
            CancelInvoke("DrainBattery");
        }
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
    void DrainBattery()
    { 
        currentBatteryCharge--;
    }

    void ReloadBattery()
    {
        currentBatteryCharge = 100;
    }

    void UpdateBatteryUI()
    { 
        int batteryLevelInDisplay = (currentBatteryCharge + 19) / 20;
        chargeDisplay.value = batteryLevelInDisplay;
    }

    bool HasCharge()
    {
        return currentBatteryCharge > 0;
    }

    public void DisableInfiniteBattery()
    {
        isBatteryInfinite = false;
        if (flashlight.enabled)
        {
            InvokeRepeating("DrainBattery", 0, 5f);
        }
    }

    public void EnableInfiniteBattery()
    {
        isBatteryInfinite = true;
        CancelInvoke("DrainBattery");
    }
}