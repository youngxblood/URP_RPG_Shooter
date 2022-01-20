using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleSounds : AudioPlayer
{
    [SerializeField] private AudioClip pickupClip = null;

    public void PlayPickupSound()
    {
        PlayClip(pickupClip);
    }
}
