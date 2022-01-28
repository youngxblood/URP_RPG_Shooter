using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Door : Interactable
{
    [SerializeField] private GameObject door;

    public override void Interact()
    {
        ToggleDoor();
    }

    private void ToggleDoor()
    {
        if (door.activeSelf)
            door.SetActive(false);
        else
            door.SetActive(true);

        interactableSounds.PlayInteractSound();
    }
}
