using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThrowableAudio : AudioPlayer
{
    [SerializeField] private AudioClip throwSFX = null;
    [SerializeField] private AudioClip explosionSFX = null;

    public void PlayThrowClip()
    {
        PlayClip(throwSFX);
    }

    public void PlayExplosionClip()
    {
        PlayClip(explosionSFX);
    }
}
