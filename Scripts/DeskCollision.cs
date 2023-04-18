using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskCollision : MonoBehaviour
{
    private PlayerAttributes player;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerAttributes>();
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        EnableCollisionWithPlayer(collision);
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        EnableCollisionWithPlayer(collision);
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        DisableCollisionWithPlayer(collision);
    }

    void EnableCollisionWithPlayer(Collision2D collision)
    {
        if (collision.gameObject.HasTag("Player"))
        {
            return;
        }
        bool collisionIsHorizontal = Mathf.Abs(collision.contacts[0].normal.x) > 0.5f;
        if (player.canMoveHorizontal && collisionIsHorizontal)
        {
            this.transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }
        else if (player.canMoveVertical && !collisionIsHorizontal)
        {
            this.transform.gameObject.layer = LayerMask.NameToLayer("Default");
        }
    }

    void DisableCollisionWithPlayer(Collision2D collision) {
        if (collision.gameObject.HasTag("Player"))
        {
            return;
        }
        this.transform.gameObject.layer = LayerMask.NameToLayer("Player Noncollision");
    }
}
