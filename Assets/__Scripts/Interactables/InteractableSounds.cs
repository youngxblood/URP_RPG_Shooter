using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableSounds : AudioPlayer
{
    [SerializeField] private AudioClip pickupClip = null;

    public void PlayInteractSound()
    {
        PlayClip(pickupClip);
    }
}
