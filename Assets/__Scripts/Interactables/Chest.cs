using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Interactable
{
    private void Start() 
    {
        OpenChest();    
    }

    private void OpenChest()
    {
        PlayChestOpenAnim();
    }

    private void PlayChestOpenAnim()
    {
        animator.SetBool("isOpened", true);
    }
}
