using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrapAudioPlayer : AudioPlayer
{
    [SerializeField] AudioClip activationClip = null;

    public void PlayActivationClip()
    {
        PlayClipWithVariablePitch(activationClip);
    }
}
