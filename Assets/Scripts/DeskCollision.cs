using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeskCollision : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        print("test");
        if (collision.gameObject.HasTag("Player"))
        {
            return;
        }
        this.transform.gameObject.layer = LayerMask.NameToLayer("Default");
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        if (collision.gameObject.HasTag("Player"))
        {
            return;
        }
        this.transform.gameObject.layer = LayerMask.NameToLayer("Player Noncollision");
    }
}
