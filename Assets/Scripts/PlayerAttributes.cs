using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAttributes : MonoBehaviour
{
    private Rigidbody2D rb;
    public float speed = 5f;
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

    }
}
