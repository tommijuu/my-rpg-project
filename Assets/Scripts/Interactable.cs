using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //subclass methods can be override methods because of virtual

    //public virtual void MoveToInteraction()
    //{

    //}

    public Transform player;
    public float interactionDistance = 5f;

    public virtual void CheckDistance(Transform player)
    {
        this.player = player;

        if (Vector3.Distance(player.position, transform.position) > interactionDistance)
            Interact();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with base class.");
    }
}
