using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PushableDesk : MonoBehaviour, IInteractable
{
    private GameObject player;
    public float pushSpeed = 1f;
    public bool currentlyPushing;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public void Interact()
    {
        if (!currentlyPushing) {
            EnablePushPullState();
        }
        else
        {
            DisablePushPullState();
        }
    }
    
    bool IsRelativePositionHorizontal(Vector3 currentObject, Vector3 other)
    {
        float yDistance = currentObject.y - other.y;
        float xDistance = currentObject.x - other.x;
        bool isHorizontal = Mathf.Abs(yDistance) < Mathf.Abs(xDistance);
        return isHorizontal;
    }

    float GetHorizontalDistance(Vector3 currentObject, Vector3 other)
    {
        return currentObject.x - other.x;
    }

    float GetVerticalDistance(Vector3 currentObject, Vector3 other)
    {
        return currentObject.y - other.y;
    }

    IEnumerator MoveDesk(bool isHorizontal)
    {
        Rigidbody2D desk = this.transform.parent.GetComponent<Rigidbody2D>();
        float horizontalDistance = GetHorizontalDistance(player.transform.position, this.transform.position);
        float verticalDistance = GetVerticalDistance(player.transform.position, this.transform.position);
        while (currentlyPushing)
        {
            if (isHorizontal)
            {
                Vector3 newPosition = player.transform.position - new Vector3(horizontalDistance, 0.2f, 0);
                Debug.Log(newPosition.x);
                desk.MovePosition(newPosition);
            }
            else
            {
                Vector3 newPosition = player.transform.position - new Vector3(0, verticalDistance, 0);
                desk.MovePosition(newPosition);
            }
            yield return null;
        }
    }

    void EnablePushPullState()
    {
        currentlyPushing = true;
        this.transform.parent.gameObject.layer = LayerMask.NameToLayer("Player Noncollision");
        this.transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        PlayerAttributes attributes = player.GetComponent<PlayerAttributes>();
        bool isHorizontal = IsRelativePositionHorizontal(player.transform.position, this.transform.position);
        if (isHorizontal)
        {
            attributes.canMoveVertical = false;
        }
        else
        {
            attributes.canMoveHorizontal = false;
        }
        attributes.SetSpeed(pushSpeed);
        StartCoroutine(MoveDesk(isHorizontal));
    }

    void DisablePushPullState()
    {
        currentlyPushing = false;
        this.transform.parent.gameObject.layer = LayerMask.NameToLayer("Default");
        this.transform.parent.gameObject.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Kinematic;
        PlayerAttributes attributes = player.GetComponent<PlayerAttributes>();
        attributes.canMoveHorizontal = true;
        attributes.canMoveVertical = true;
        attributes.SetSpeed(attributes.walkingSpeed);
    }
}
