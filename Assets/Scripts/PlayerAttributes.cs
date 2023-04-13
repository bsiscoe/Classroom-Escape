using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    public List<GameObject> collidingTriggers;
    private Rigidbody2D rb;
    public float speed = 5f;
    public bool hasFlashlight;
    public Transform flashlight;
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
        // Moves the player's position in the direction of the change
        rb.MovePosition(this.transform.position + change * speed * Time.deltaTime);
        UpdateFlashlightDirection(change);
    }

    void Update()
    {
        bool inRangeOfTrigger = collidingTriggers.Count > 0;
        if (Input.GetKeyDown("space") && inRangeOfTrigger)
        {
            collidingTriggers[0].GetComponent<IInteractable>().Interact();
        }
        if (Input.GetKeyDown("f"))
        {

        }
    }

    void UpdateFlashlightDirection(Vector3 direction)
    {
        if (direction.y < 0)
        {
            flashlight.rotation = Quaternion.Euler(0, 0, 0);
        }
        else if (direction.y > 0)
        {
            flashlight.rotation = Quaternion.Euler(0, 0, 180);
        }
        else if (direction.x > 0)
        {
            flashlight.rotation = Quaternion.Euler(0, 0, 90);
        }
        else if (direction.x < 0)
        {
            flashlight.rotation = Quaternion.Euler(0, 0, 270);
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

