using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering.Universal;
using UnityEngine.UIElements;

public enum PlayerState
{
    transition,
    forcedReading,
    unforcedReading,
    idle,
    walking,
}

public enum PlayerDirection
{
    up,
    down,
    left,
    right
}

public class PlayerAttributes : MonoBehaviour
{
    public List<GameObject> collidingTriggers;
    Rigidbody2D rb;
    Animator anim;
    public float walkingSpeed = 5f;
    private float speed;
    public bool hasFlashlight;
    public Flashlight flashlight;


    public bool canMoveHorizontal = true;
    public bool canMoveVertical = true;

    public bool canMoveLeft = true;
    public bool canMoveRight = true;
    public bool canMoveUp = true;
    public bool canMoveDown = true;
    public PlayerState currentState;
    public PlayerDirection currentDirection;

    public bool updateFacing = true;

    // Start is called before the first frame update
    void Start()
    {
        speed = walkingSpeed;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        flashlight = GameObject.FindGameObjectWithTag("Flashlight").GetComponent<Flashlight>();
        currentState = PlayerState.idle;
        currentDirection = PlayerDirection.down;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (currentState == PlayerState.transition)
        {
            UpdateAnimation(Vector3.zero);
            return;
        }
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
        if (updateFacing)
        {
            UpdateDirection(change);
        }
        UpdateAnimation(change);
        // Moves the player's position in the direction of the change
        if (currentState != PlayerState.forcedReading)
        {
            Vector3 newPosition = this.transform.position + (speed * Time.deltaTime * change);
            rb.MovePosition(newPosition);
            if (updateFacing)
            {
                flashlight.UpdateFlashlightDirection(change);
            }
        }
    }

    void Update()
    {
        if (currentState == PlayerState.transition)
        {
            return;
        }
        bool inRangeOfTrigger = collidingTriggers.Count > 0;
        bool isReading = currentState == PlayerState.unforcedReading || currentState == PlayerState.forcedReading;
        if (Input.GetKeyDown(KeyCode.Space) && inRangeOfTrigger && !isReading)
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

    void UpdateAnimation(Vector3 change)
    {
        if (change != Vector3.zero)
        {
            anim.SetBool("Walking", true);

        }
        else
        {
            anim.SetBool("Walking", false);
        }

        if (currentDirection == PlayerDirection.left)
        {
            anim.SetFloat("MoveX", -1f);
            anim.SetFloat("MoveY", 0f);
        }
        else if (currentDirection == PlayerDirection.right)
        {
            anim.SetFloat("MoveX", 1f);
            anim.SetFloat("MoveY", 0f);
        }
        else if (currentDirection == PlayerDirection.up)
        {
            anim.SetFloat("MoveY", 1f);
            anim.SetFloat("MoveX", 0f);
        }
        else if (currentDirection == PlayerDirection.down)
        {
            anim.SetFloat("MoveY", -1f);
            anim.SetFloat("MoveX", 0f);
        }
    }

    void UpdateDirection(Vector3 change)
    {
        if (change.x < 0)
        {
            currentDirection = PlayerDirection.left;
        }
        else if (change.x > 0)
        {
            currentDirection = PlayerDirection.right;
        }
        else if (change.y < 0)
        {
            currentDirection = PlayerDirection.down;
        }
        else if (change.y > 0)
        {
            currentDirection = PlayerDirection.up;
        }
    }

    public void SetDirection(PlayerDirection direction)
    {
        currentDirection = direction;
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

