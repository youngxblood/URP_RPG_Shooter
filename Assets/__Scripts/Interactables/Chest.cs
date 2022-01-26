using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private void OpenChest()
    {
        PlayChestOpenAnim();
        interactableSounds.PlayInteractSound();
    }

    private void PlayChestOpenAnim()
    {
        animator.SetBool("isOpened", true);
    }

    public override void Interact()
    {
        OpenChest(); 
    }
}
