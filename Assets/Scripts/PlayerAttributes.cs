using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;

<<<<<<< Updated upstream
=======
public enum PlayerState
{
    reading,
    idle,
    walking
}

>>>>>>> Stashed changes
public class PlayerAttributes : MonoBehaviour
{
    public List<GameObject> collidingTriggers;
    private Rigidbody2D rb;
    public float walkingSpeed = 5f;
    private float speed;
    public bool hasFlashlight;
    public Flashlight flashlight;
<<<<<<< Updated upstream
=======
	public PlayerState currentState;
>>>>>>> Stashed changes

    public bool canMoveHorizontal = true;
    public bool canMoveVertical = true;

    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool canMoveUp = true;
    public bool canMoveDown = true;

    // Start is called before the first frame update
    void Start()
    {
<<<<<<< Updated upstream
=======
		currentState = PlayerState.idle
>>>>>>> Stashed changes
        speed = walkingSpeed;
        rb = GetComponent<Rigidbody2D>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Flashlight>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        // Creates a Vector3 value change with the x and y of the player's input
        Vector3 change = Vector3.zero;
        if (canMoveHorizontal)
        {
            change.x = Input.GetAxisRaw("Horizontal");
        }
        if (canMoveVertical)
        {
            change.y = Input.GetAxisRaw("Vertical");
        }
        change = RestrictMovement(change);
        // Normalizes the Vector3 so that it conveys just the direction, not distance
        change.Normalize();
<<<<<<< Updated upstream
        // Moves the player's position in the direction of the change
        Vector3 newPosition = this.transform.position + (speed * Time.deltaTime * change);
        rb.MovePosition(newPosition);
        flashlight.UpdateFlashlightDirection(change);
    }
=======
        // Moves the player's position in the direction of the change, if they are not reading forced dialogue
        if (currentState != PlayerState.reading)
        {
			Vector3 newPosition = this.transform.position + (speed * Time.deltaTime * change);
			rb.MovePosition(newPosition);
			flashlight.UpdateFlashlightDirection(change);
		}
	}
>>>>>>> Stashed changes

    void Update()
    {
        bool inRangeOfTrigger = collidingTriggers.Count > 0;
<<<<<<< Updated upstream
        if (Input.GetKeyDown(KeyCode.Space) && inRangeOfTrigger)
=======
        if (Input.GetKeyDown(KeyCode.Space) && inRangeOfTrigger && currentState != PlayerState.reading)
>>>>>>> Stashed changes
        {
            collidingTriggers[0].GetComponent<IInteractable>().Interact();
        }
        if (Input.GetKeyDown(KeyCode.F))
        {
            flashlight.ToggleFlashLight();
        }
    }

    public void SetSpeed(float speed)
    {
        this.speed = speed;
    }

    Vector3 RestrictMovement(Vector3 change)
    {
        if (!canMoveLeft)
        {
            if (change.x < 0)
            {
                change.x = 0;
            }
        }
        if (!canMoveRight)
        {
            if (change.x > 0)
            {
                change.x = 0;
            }
        }
        if (!canMoveUp)
        {
            if (change.y > 0)
            {
                change.y = 0;
            }
        }
        if (!canMoveDown)
        {
            if (change.y > 0)
            {
                change.y = 0;
            }
        }
        return change;
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

