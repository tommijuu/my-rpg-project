using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    //This script is a base class for interactable objects (items) and NPCs in the game

    public Transform player;
    public float interactionDistance = 5f; //some range where the interactable is interactable with

    //(note to myself) Subclass methods can be override methods because of virtual

    //The range where an interactable object or NPC can be interacted with, will be limited
    public virtual void CheckDistance(Transform player)
    {
        this.player = player;

        if (Vector3.Distance(player.position, transform.position) > interactionDistance)
            Interact();
    }

    public virtual void Interact()
    {
        Debug.Log("Interacting with base interaction class.");
    }

    //public virtual void MoveToInteraction()
    //{

    //}
}
