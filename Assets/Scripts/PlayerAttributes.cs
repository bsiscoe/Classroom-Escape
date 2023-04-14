using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public List<GameObject> collidingTriggers;
    private Rigidbody2D rb;
    public float speed = 5f;
    public bool hasFlashlight;
    public Animator flashlight;
    public bool isReading;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Creates a Vector3 value change with the x and y of the player's input
        Vector3 change = Vector3.zero;
        change.x = Input.GetAxisRaw("Horizontal");
        change.y = Input.GetAxisRaw("Vertical");
        // Normalizes the Vector3 so that it conveys just the direction, not distance
        change.Normalize();
        // Moves the player's position in the direction of the change, if they are not reading forced dialogue
        if (!isReading)
        {
            rb.MovePosition(this.transform.position + change * speed * Time.deltaTime);
            UpdateFlashlightDirection(change);
        }
    }

    void Update()
    {
        bool inRangeOfTrigger = collidingTriggers.Count > 0;
        if (Input.GetKeyDown("space") && inRangeOfTrigger && !isReading)
        {
            collidingTriggers[0].GetComponent<IInteractable>().Interact();
        }
        if (Input.GetKeyDown("f"))
        {

        }
    }

    void ResetFlashlightBool()
    {
        foreach (AnimatorControllerParameter parameter in flashlight.parameters)
        {
            flashlight.SetBool(parameter.name, false);
        }
    }

    void UpdateFlashlightDirection(Vector3 direction)
    {
        if (direction.y < 0)
        {
            ResetFlashlightBool();
            flashlight.SetBool("down", true);
        }
        else if (direction.y > 0)
        {
            ResetFlashlightBool();
            flashlight.SetBool("up", true);
        }
        else if (direction.x > 0)
        {
            ResetFlashlightBool();
            flashlight.SetBool("right", true);
        }
        else if (direction.x < 0)
        {
            ResetFlashlightBool();
            flashlight.SetBool("left", true);
        }
    }

    public void AddTrigger(GameObject trigger)
    {
        collidingTriggers.Add(trigger);
    }

    public void RemoveTrigger(GameObject trigger)
    {
        collidingTriggers.Remove(trigger);
    }
}

